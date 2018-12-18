using ChameleonMiniGUI.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Dump
{
    public class MctDumpStrategy : DumpStrategy
    {
        public MctDumpStrategy(string fileName)
        {
            FileName = fileName;
        }

        public override string Extension
        {
            get
            { return ".mct"; }
        }

        public override byte[] Read()
        {
            var lines = File.ReadLines(FileName)
                .Where(line => line.Length == 32)
                .Select(line => line.Replace('-', 'F')); // fill unknown data
            return MifareClassicModel.StringToByteArray(string.Join("", lines));
        }

        public override void Save(byte[] data)
        {
            var mfc = new MifareClassicModel();
            mfc.Blocks = MifareClassicModel.ToNestedByteArray(data);
            var list = new List<string>();
            foreach (var sc in mfc.SectorKeys.Values)
            {
                list.Add($"+Sector: {sc.SectorNumber}");
                for (int i = 0; i < sc.BlockCount; i++)
                    list.Add(MifareClassicModel.ByteArrayToString(mfc.Blocks[sc.FirstBlockNumber + i]));
            }
            File.WriteAllText(FileName, string.Join("\n", list), Encoding.ASCII); // UNIX NewLine
        }
    }
}
