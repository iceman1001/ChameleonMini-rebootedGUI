using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    public class JsonFileByteProvider : IByteProvider
    {
        byte[] data;
        string fileName;
        bool changed;
        MifareClassicModel mfc;

        public JsonFileByteProvider(string fileName)
        {
            this.fileName = fileName;
            using (var fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var settings = new DataContractJsonSerializerSettings
                {
                    DataContractSurrogate = new BlockSurrogate(),
                    KnownTypes = new List<Type> {typeof (Dictionary<string, string>)},
                    UseSimpleDictionaryFormat = true
                };
                var ser = new DataContractJsonSerializer(typeof(MifareClassicModel), settings);
                mfc = ser.ReadObject(fs) as MifareClassicModel;
                data = mfc.ToByteArray();
            }
        }
        
        public long Length => data.LongLength;

        public event EventHandler LengthChanged;
        public event EventHandler Changed;
        public event EventHandler WriteFinished;

        void OnChanged(EventArgs e)
            => Changed?.Invoke(this, e);

        void OnWriteFinished(EventArgs e)
            => WriteFinished?.Invoke(this, e);

        public void ApplyChanges()
        {
            mfc.Blocks = MifareClassicModel.ToNestedByteArray(data);
            using (var fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true, "  "))
            {
                var settings = new DataContractJsonSerializerSettings
                {
                    DataContractSurrogate = new BlockSurrogate(),
                    KnownTypes = new List<Type> {typeof (Dictionary<string, string>)},
                    UseSimpleDictionaryFormat = true
                };
                var ser = new DataContractJsonSerializer(typeof(MifareClassicModel), settings);
                ser.WriteObject(writer, mfc);
                writer.Flush();
            }
            changed = false;
        }

        public void DeleteBytes(long index, long length)
        {
            throw new NotImplementedException();
        }

        public bool HasChanges()
        {
            return changed;
        }

        public void InsertBytes(long index, byte[] bs)
        {
            throw new NotImplementedException();
        }

        public byte ReadByte(long index)
        {
            return data[index];
        }

        public bool SupportsDeleteBytes()
        {
            return false;
        }

        public bool SupportsInsertBytes()
        {
            return false;
        }

        public bool SupportsWriteByte()
        {
            return true;
        }

        public void WriteByte(long index, byte value)
        {
            data[index] = value;
            changed = true;
            OnChanged(EventArgs.Empty);
            OnWriteFinished(EventArgs.Empty);
        }
    }
}
