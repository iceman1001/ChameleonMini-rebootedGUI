using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    [DataContract]
    public class MifareUltralightModel : MifareModel
    {
        private MifareUltralightCardInfo info;

        [DataMember(Order = 1)]
        public override string FileType
        {
            get { return "mfu"; }
            set { }
        }

        [DataMember(Order = 2)]
        public MifareUltralightCardInfo Card
        {
            get { return info; }
            set
            {
                info = value;
                if (info != null)
                    info.Mifare = this;
            }
        }

        [DataMember(Name = "blocks", Order = 3)]
        public byte[][] Blocks { get; set; }

        public override byte[] ToByteArray()
        {
            var stba = new Func<string, byte[]>(MifareClassicModel.StringToByteArray);
            if (info.Counter0 != null && info.Counter1 != null && info.Counter2 != null) {
                // Save as new header dump format with counters
                byte[] pages = { (byte) (Blocks.Length - 1) };
                return stba(info.Version)
                    .Concat(stba(info.TBO_0))
                    .Concat(stba(info.TBO_1))
                    .Concat(pages)
                    .Concat(stba(info.Signature))
                    .Concat(stba(info.Counter0)).Concat(stba(info.Tearing0))
                    .Concat(stba(info.Counter1)).Concat(stba(info.Tearing1))
                    .Concat(stba(info.Counter2)).Concat(stba(info.Tearing2))
                    .Concat(Blocks.SelectMany(bytes => bytes))
                    .ToArray();
            }
            else if (info.Tearing0 != null && info.Tearing1 != null && info.Tearing2 != null)
                // Save as old header dump format with tearing, but without counters
                return stba(info.Version)
                    .Concat(stba(info.TBO_0))
                    .Concat(stba(info.Tearing0))
                    .Concat(stba(info.Tearing1))
                    .Concat(stba(info.Tearing2))
                    .Concat(stba(info.Pack))
                    .Concat(stba(info.TBO_1))
                    .Concat(stba(info.Signature))
                    .Concat(Blocks.SelectMany(bytes => bytes))
                    .ToArray();
            else
                // Save as simple dump format
                return Blocks.SelectMany(bytes => bytes)
                    .ToArray();
        }

        public static bool HasUltralightNewHeader(IReadOnlyList<byte> bytes)
        {
            if (bytes.Count % 4 != 0 || bytes.Count <= MifareUltralightCardInfo.NewPrefixLength)
                return false;

            // tbo should be ZERO
            if (bytes[8] != 0x00 || bytes[9] != 0x00)
                return false;

            // tbo1 should be ZERO
            if (bytes[10] != 0x00)
                return false;

            // pages count must be equals to pages in header
            int maxPage = (bytes.Count - MifareUltralightCardInfo.NewPrefixLength) / 4 - 1;
            return maxPage == bytes[11];
        }

        public static bool HasUltralightHeader(IReadOnlyList<byte> bytes)
        {
            // empty header
            var empty_header = new byte[]
            {
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
            };

            if (bytes.SequenceEqual(empty_header))
            {
                return true;
            }

            // detect mfu header. If probability of magic values is more than 50%, assume file has header.
            var probability = 0d;
            // first two bytes of version should be 0x00, 0x04
            if (bytes[0] == 0x00 && bytes[1] == 0x04)
                probability += 0.25;

            // tbo should be ZERO
            if (bytes[8] == 0x00 && bytes[9] == 0x00)
                probability += 0.15;

            // tbo1 should be ZERO
            if (bytes[15] == 0x00)
                probability += 0.15;

            // tearing is normally 0xBD
            if (bytes[10] == 0xBD || bytes[11] == 0xBD || bytes[12] == 0xBD)
                probability += 0.35;

            return (probability >= 0.50);
        }

        public static MifareUltralightModel Parse(byte[] data)
        {
            bool isNewHeaderFormat = HasUltralightNewHeader(data);
            bool isOldHeaderFormat = !isNewHeaderFormat && HasUltralightHeader(data);
            int headerLength = isNewHeaderFormat ? MifareUltralightCardInfo.NewPrefixLength : isOldHeaderFormat ? MifareUltralightCardInfo.PrefixLength : 0;
            int pages = (data.Length - headerLength) / 4;
            return new MifareUltralightModel()
            {
                Created = "ChameleonMiniGUI",
                Card = isNewHeaderFormat ?
                new MifareUltralightCardInfo()
                {
                    Version = MifareClassicModel.ByteArrayToString(data.Take(8)),
                    TBO_0 = MifareClassicModel.ByteArrayToString(data.Skip(8).Take(2)),
                    Tearing0 = MifareClassicModel.ByteArrayToString(data.Skip(44 + 3).Take(1)),
                    Tearing1 = MifareClassicModel.ByteArrayToString(data.Skip(44 + 7).Take(1)),
                    Tearing2 = MifareClassicModel.ByteArrayToString(data.Skip(44 + 11).Take(1)),
                    Pack = "0000",
                    TBO_1 = MifareClassicModel.ByteArrayToString(data.Skip(10).Take(1)),
                    Signature = MifareClassicModel.ByteArrayToString(data.Skip(12).Take(32)),
                    Counter0 = MifareClassicModel.ByteArrayToString(data.Skip(44 + 0).Take(3)),
                    Counter1 = MifareClassicModel.ByteArrayToString(data.Skip(44 + 4).Take(3)),
                    Counter2 = MifareClassicModel.ByteArrayToString(data.Skip(44 + 8).Take(3))
                }
                : isOldHeaderFormat ?
                new MifareUltralightCardInfo()
                {
                    Version = MifareClassicModel.ByteArrayToString(data.Take(8)),
                    TBO_0 = MifareClassicModel.ByteArrayToString(data.Skip(8).Take(2)),
                    Tearing0 = MifareClassicModel.ByteArrayToString(data.Skip(10).Take(1)),
                    Tearing1 = MifareClassicModel.ByteArrayToString(data.Skip(10).Take(1)),
                    Tearing2 = MifareClassicModel.ByteArrayToString(data.Skip(10).Take(1)),
                    Pack = MifareClassicModel.ByteArrayToString(data.Skip(13).Take(2)),
                    TBO_1 = MifareClassicModel.ByteArrayToString(data.Skip(15).Take(1)),
                    Signature = MifareClassicModel.ByteArrayToString(data.Skip(16).Take(32)),
                    Counter0 = "000000",
                    Counter1 = "000000",
                    Counter2 = "000000"
                }
                :
                new MifareUltralightCardInfo()
                {
                    Version = null,
                    TBO_0 = null,
                    Tearing0 = null,
                    Tearing1 = null,
                    Tearing2 = null,
                    Pack = null,
                    TBO_1 = null,
                    Signature = null,
                    Counter0 = null,
                    Counter1 = null,
                    Counter2 = null
                }
                ,
                Blocks = MifareClassicModel.ToNestedByteArray(data.Skip(headerLength).ToArray(), 4)
            };
        }
    }
}
