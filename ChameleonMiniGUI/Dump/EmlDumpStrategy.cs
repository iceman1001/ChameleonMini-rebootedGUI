using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Dump
{
    public class EmlDumpStrategy : DumpStrategy
    {
        public EmlDumpStrategy(string fileName)
        {
            FileName = fileName;
        }

        public override string Extension
        {
            get
            { return ".eml"; }
        }

        public override byte[] Read()
        {
            var rows = File.ReadAllLines(FileName);
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var row in rows)
                {
                    var clean = row.Replace(":", "").Replace(" ", "");

                    if (string.IsNullOrWhiteSpace(clean))
                        continue;

                    int pos = 0;
                    var bytes = new byte[clean.Length >> 1];
                    for (int i = 0; i < clean.Length; i += 2)
                    {
                        var b = Convert.ToByte(clean.Substring(i, 2), 16);
                        bytes[pos++] = b;
                    }

                    bw.Write(bytes);
                }
                bw.Flush();
                return ms.ToArray();
            }
        }

        public override void Save(byte[] data)
        {
            // read bytes & convert to ascii
            var bytesleft = data.Length;
            var pos = 0;
            var rows = new List<string>();
            while (bytesleft > 0)
            {
                var len = Math.Min(bytesleft, 16);
                rows.Add(BitConverter.ToString(data, pos, len).Replace("-", " "));
                pos += len;
                bytesleft -= len;
            }

            // save text file
            File.WriteAllLines(FileName, rows, Encoding.ASCII);
        }
    }
}
