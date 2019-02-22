using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Dump
{
    public class DumpFileByteProvider : IByteProvider
    {
        byte[] data;
        bool changed;
        DumpStrategy dumpStrategy;

        public DumpFileByteProvider(string fileName)
        {
            dumpStrategy = DumpStrategyFactory.Create(fileName);
            data = dumpStrategy.Read();
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
            dumpStrategy.Save(data);
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
