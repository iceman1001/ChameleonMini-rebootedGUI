using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{

    [DataContract]
    public class MifareClassicModel
    {
        [DataMember(Order = 0)]
        public string Created { get; set; }

        [DataMember(Order = 1)]
        public string FileType { get; set; }

        [DataMember(Name = "blocks", Order = 2)]
        public byte[][] Blocks { get; set; }

        [DataMember(Order = 3)]
        public MifareClassicCardInfo Card
        {
            get { return new MifareClassicCardInfo(this); }
            set { }
        }

        [DataMember(Order = 4)]
        public Dictionary<string, MifareClassicSectorKey> SectorKeys
        {
            get
            {
                return Enumerable
                    .Range(0, Blocks.Length / 4)
                    .ToDictionary(i => i.ToString(), i => new MifareClassicSectorKey(this, i));
            }
            set { }
        }

        public static byte[] StringToByteArray(string hex)
            => Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();

        public static string ByteArrayToString(IEnumerable<byte> bytes)
            => string.Join("", bytes.Select(b => b.ToString("X2")));

        public static byte[][] ToNestedByteArray(byte[] data)
        {
            return Enumerable.Range(0, data.Length / 16)
                .Select(i => data.Skip(i * 16).Take(16).ToArray())
                .ToArray();
        }

        public byte[] ToByteArray()
            => Blocks.SelectMany(bytes => bytes).ToArray();
    }
}
