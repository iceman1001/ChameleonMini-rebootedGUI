using ChameleonMiniGUI.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ChameleonMiniGUI.Dump
{
    public class JsonDumpStrategy : DumpStrategy
    {
        private static readonly DataContractJsonSerializerSettings Settings = new DataContractJsonSerializerSettings
        {
            DataContractSurrogate = new BlockSurrogate(),
            KnownTypes = new List<Type>
            {
                typeof(Dictionary<string, string>)
            },
            UseSimpleDictionaryFormat = true,
            EmitTypeInformation = EmitTypeInformation.Never,
            SerializeReadOnlyTypes = false
        };

        public JsonDumpStrategy(string fileName)
        {
            FileName = fileName;
        }

        public override string Extension
        {
            get
            { return ".json"; }
        }

        public override byte[] Read()
        {
            using (var fs = File.OpenRead(FileName))
            using (var reader = JsonReaderWriterFactory.CreateJsonReader(fs, new XmlDictionaryReaderQuotas()))
            {
                var root = XElement.Load(reader);
                var fileType = root.Element("FileType").Value;
                var type = typeof(MifareClassicModel);
                if (fileType == "mfu")
                    type = typeof(MifareUltralightModel);
                var ser = new DataContractJsonSerializer(type, Settings);
                using (var reader2 = root.CreateReader())
                {
                    var mf = ser.ReadObject(reader2) as MifareModel;
                    return mf.ToByteArray();
                }
            }
        }

        public override void Save(byte[] data)
        {
            MifareModel mf = null;
            if (data.Length < 1024 && data.Length != 320)
            {
                mf = MifareUltralightModel.Parse(data);
            }
            else
            {
                mf = new MifareClassicModel()
                {
                    Created = "ChameleonMiniGUI",
                    Blocks = MifareClassicModel.ToNestedByteArray(data)
                };
            }
            using (var fs = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true, "  "))
            {
                var ser = new DataContractJsonSerializer(mf.GetType(), Settings);
                ser.WriteObject(writer, mf);
                writer.Flush();
            }
        }
    }
}
