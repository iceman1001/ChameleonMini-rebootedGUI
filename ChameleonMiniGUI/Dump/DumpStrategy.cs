using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Dump
{
    public abstract class DumpStrategy
    {
        public string FileName { get; set; }

        public abstract byte[] Read();

        public abstract void Save(byte[] data);

        public abstract string Extension { get; }
    }
}
