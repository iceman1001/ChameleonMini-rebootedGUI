using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    [DataContract]
    public class MifareUltralightCardInfo : MifareCardInfo
    {
        public const int PrefixLength = 48;
        public const int NewPrefixLength = 56;

        MifareUltralightModel mfu { get { return Mifare as MifareUltralightModel; } }

        [DataMember(Name = "UID", Order = 0)]
        public override string Uid
        {
            get
            { return MifareClassicModel.ByteArrayToString(mfu.Blocks[0].Take(3).Concat(mfu.Blocks[1])); }
            set
            { }
        }

        [DataMember(Order = 1)]
        public string Version { get; set; }

        [DataMember(Order = 2)]
        public string TBO_0 { get; set; }

        [DataMember(Order = 3)]
        public string TBO_1 { get; set; }

        [DataMember(Order = 4)]
        public string Signature { get; set; }

        [DataMember(Order = 5)]
        public string Counter0 { get; set; }

        [DataMember(Order = 6)]
        public string Tearing0 { get; set; }

        [DataMember(Order = 7)]
        public string Counter1 { get; set; }

        [DataMember(Order = 8)]
        public string Tearing1 { get; set; }

        [DataMember(Order = 9)]
        public string Counter2 { get; set; }

        [DataMember(Order = 10)]
        public string Tearing2 { get; set; }
    }
}
