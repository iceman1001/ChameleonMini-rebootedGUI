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
        public string Tearing { get; set; }

        [DataMember(Order = 4)]
        public string Pack { get; set; }

        [DataMember(Order = 5)]
        public string TBO_1 { get; set; }

        [DataMember(Order = 6)]
        public string Signature { get; set; }

        [DataMember(Order = 7)]
        public string Counter { get; set; }
    }
}
