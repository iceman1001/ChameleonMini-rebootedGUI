using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    [DataContract]
    public class MifareClassicCardInfo : MifareCardInfo
    {
        MifareClassicModel mfc
        {
            get { return Mifare as MifareClassicModel; }
        }

        public MifareClassicCardInfo()
        { }

        public MifareClassicCardInfo(MifareClassicModel mfc)
        {
            Mifare = mfc;
        }

        [DataMember(Name = "UID", Order = 0)]
        public override string Uid
        {
            get { return MifareClassicModel.ByteArrayToString(mfc.Blocks[0].Take(4)); }
            set { }
        }

        [DataMember(Name = "SAK", Order = 1)]
        public string Sak
        {
            get { return MifareClassicModel.ByteArrayToString(mfc.Blocks[0].Skip(5).Take(1)); }
            set { }
        }

        [DataMember(Name = "ATQA", Order = 2)]
        public string Atqa
        {
            get { return MifareClassicModel.ByteArrayToString(mfc.Blocks[0].Skip(6).Take(2)); }
            set { }
        }
    }
}
