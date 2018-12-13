using ChameleonMiniGUI.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Dump
{
    public class JsonDumpStrategy : DumpStrategy
    {
        private readonly DataContractJsonSerializer Serializer;

        public JsonDumpStrategy(string fileName)
        {
            FileName = fileName;
            var settings = new DataContractJsonSerializerSettings();
            settings.DataContractSurrogate = new BlockSurrogate();
            settings.KnownTypes = new List<Type> { typeof(Dictionary<string, string>) };
            settings.UseSimpleDictionaryFormat = true;
            Serializer = new DataContractJsonSerializer(typeof(MifareClassicModel), settings);
        }

        public override string Extension
        {
            get
            { return ".json"; }
        }

        public override byte[] Read()
        {
            using (var fs = File.OpenRead(FileName))
            {
                var mfc = Serializer.ReadObject(fs) as MifareClassicModel;
                return mfc.ToByteArray();
            }
        }

        public override void Save(byte[] data)
        {
            var mfc = new MifareClassicModel()
            {
                Created = "ChameleonMiniGUI",
                FileType = "mfcard",
                Blocks = MifareClassicModel.ToNestedByteArray(data)
            };
            using (var fs = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true, "  "))
            {
                var settings = new DataContractJsonSerializerSettings();
                settings.DataContractSurrogate = new BlockSurrogate();
                settings.KnownTypes = new List<Type> { typeof(Dictionary<string, string>) };
                settings.UseSimpleDictionaryFormat = true;
                var ser = new DataContractJsonSerializer(typeof(MifareClassicModel), settings);
                ser.WriteObject(writer, mfc);
                writer.Flush();
            }
        }
    }
}
