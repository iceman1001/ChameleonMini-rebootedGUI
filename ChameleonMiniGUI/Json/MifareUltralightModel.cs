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
            return stba(info.Version)
                .Concat(stba(info.TBO_0))
                .Concat(stba(info.Tearing))
                .Concat(stba(info.Pack))
                .Concat(stba(info.TBO_1))
                .Concat(stba(info.Signature))
                .Concat(Blocks.SelectMany(bytes => bytes))
                .ToArray();
        }

        public static MifareUltralightModel Parse(byte[] data)
        {
            return new MifareUltralightModel()
            {
                Created = "ChameleonMiniGUI",
                Card = new MifareUltralightCardInfo()
                {
                    Version = MifareClassicModel.ByteArrayToString(data.Take(8)),
                    TBO_0 = MifareClassicModel.ByteArrayToString(data.Skip(8).Take(2)),
                    Tearing = MifareClassicModel.ByteArrayToString(data.Skip(10).Take(3)),
                    Pack = MifareClassicModel.ByteArrayToString(data.Skip(13).Take(2)),
                    TBO_1 = MifareClassicModel.ByteArrayToString(data.Skip(15).Take(1)),
                    Signature = MifareClassicModel.ByteArrayToString(data.Skip(16).Take(32)),
                    Counter = null
                },
                Blocks = MifareClassicModel.ToNestedByteArray(data.Skip(48).ToArray(), 4)
            };
        }
    }
}
