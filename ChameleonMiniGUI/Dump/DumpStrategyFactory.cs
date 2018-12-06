using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Dump
{
    public static class DumpStrategyFactory
    {
        public static DumpStrategy Create(DumpType type, string fileName = null)
        {
            switch (type)
            {
                case DumpType.Bin:
                    return new BinaryDumpStrategy(fileName);
                case DumpType.Json:
                    return new JsonDumpStrategy(fileName);
                case DumpType.Eml:
                    return new EmlDumpStrategy(fileName);
                default:
                    return new BinaryDumpStrategy(fileName);
            }
        }

        public static DumpStrategy Create(string fileName)
        {
            switch (Path.GetExtension(fileName).ToLower())
            {
                case ".bin":
                case ".dump":
                case ".mfd":
                case ".hex":
                    return new BinaryDumpStrategy(fileName);
                case ".json":
                    return new JsonDumpStrategy(fileName);
                case ".eml":
                    return new EmlDumpStrategy(fileName);
                default:
                    return new BinaryDumpStrategy(fileName);
            }
        }
    }
}
