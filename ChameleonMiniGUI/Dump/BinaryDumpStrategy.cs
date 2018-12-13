using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Dump
{
    public class BinaryDumpStrategy : DumpStrategy
    {
        public BinaryDumpStrategy(string fileName)
        {
            FileName = fileName;
        }

        public override string Extension
        {
            get
            { return ".bin"; }
        }

        public override byte[] Read() =>
            File.ReadAllBytes(FileName);

        public override void Save(byte[] data) =>
            File.WriteAllBytes(FileName, data);
    }
}
