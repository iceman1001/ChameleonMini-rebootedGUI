using System;
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
        [DataMember]
        public string Created { get; set; }

        [DataMember]
        public string FileType { get; set; }

        [DataMember(Name = "blocks")]
        public byte[][] Blocks { get; set; }

        public byte[] ToByteArray()
            => Blocks.SelectMany(bytes => bytes).ToArray();
    }
}
