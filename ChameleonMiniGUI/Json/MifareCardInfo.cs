using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    [DataContract]
    public abstract class MifareCardInfo
    {
        public MifareModel Mifare { get; set; }

        [DataMember(Name = "UID", Order = 0)]
        public abstract string Uid { get; set; }
    }
}
