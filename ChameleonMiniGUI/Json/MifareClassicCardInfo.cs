using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    public class MifareClassicCardInfo
    {
        MifareClassicModel mfc;

        public MifareClassicCardInfo()
        { }

        public MifareClassicCardInfo(MifareClassicModel mfc)
        {
            this.mfc = mfc;
        }

        [DataMember(Name = "UID")]
        public string Uid
        {
            get { return MifareClassicModel.ByteArrayToString(mfc.Blocks[0].Take(4)); }
            set { }
        }

        [DataMember(Name = "SAK")]
        public string Sak
        {
            get { return MifareClassicModel.ByteArrayToString(mfc.Blocks[0].Skip(5).Take(1)); }
            set { }
        }

        [DataMember(Name = "ATQA")]
        public string Atqa
        {
            get { return MifareClassicModel.ByteArrayToString(mfc.Blocks[0].Skip(6).Take(2)); }
            set { }
        }
    }
}
