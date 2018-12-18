using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    [DataContract]
    public abstract class MifareModel
    {
        [DataMember(Order = 0)]
        public string Created { get; set; }

        [DataMember(Order = 1)]
        public virtual string FileType { get; set; }

        public abstract byte[] ToByteArray();
    }
}
