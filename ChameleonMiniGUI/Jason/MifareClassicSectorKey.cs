﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonMiniGUI.Json
{
    [DataContract]
    public class MifareClassicSectorKey
    {
        MifareClassicModel mfc;
        int sector;

        public MifareClassicSectorKey()
        { }

        public MifareClassicSectorKey(MifareClassicModel mfc, int sector)
        {
            this.mfc = mfc;
            this.sector = sector;
        }

        [DataMember(Order = 0)]
        public string KeyA
        {
            get
            {
                return MifareClassicModel.ByteArrayToString(mfc.Blocks[sector * 4 + 3].Take(6));
            }
            set { }
        }

        [DataMember(Order = 1)]
        public string KeyB
        {
            get
            {
                return MifareClassicModel.ByteArrayToString(mfc.Blocks[sector * 4 + 3].Skip(10).Take(6));
            }
            set { }
        }

        [DataMember(Order = 2)]
        public string AccessConditions
        {
            get
            {
                return MifareClassicModel.ByteArrayToString(mfc.Blocks[sector * 4 + 3].Skip(6).Take(4));
            }
            set { }
        }

        [DataMember(Order = 3)]
        public Dictionary<string, string> AccessConditionsText
        {
            get
            {
                var keyBlock = mfc.Blocks[sector * 4 + 3];
                var conditions = keyBlock.Skip(6).Take(4).ToArray();
                var dic = new Dictionary<string, string>
                {
                    ["block" + (sector * 4)] = GetAccessConditionsDesc(0, conditions),
                    ["block" + (sector * 4 + 1)] = GetAccessConditionsDesc(1, conditions),
                    ["block" + (sector * 4 + 2)] = GetAccessConditionsDesc(2, conditions),
                    ["block" + (sector * 4 + 3)] = GetAccessConditionsDesc(3, conditions),
                    ["UserData"] = MifareClassicModel.ByteArrayToString(keyBlock.Skip(9).Take(1))
                };
                return dic;
            }
            set { }
        }

        static readonly Dictionary<byte, string> MfAccessConditions = new Dictionary<byte, string>()
        {
            [0x00] = "rdAB wrAB incAB dectrAB",
            [0x01] = "rdAB dectrAB",
            [0x02] = "rdAB",
            [0x03] = "rdB wrB",
            [0x04] = "rdAB wrB",
            [0x05] = "rdB",
            [0x06] = "rdAB wrB incB dectrAB",
            [0x07] = "none"
        };

        static readonly Dictionary<byte, string> MfAccessConditionsTrailer = new Dictionary<byte, string>()
        {
            [0x00] = "rdAbyA rdCbyA rdBbyA wrBbyA",
            [0x01] = "wrAbyA rdCbyA wrCbyA rdBbyA wrBbyA",
            [0x02] = "rdCbyA rdBbyA",
            [0x03] = "wrAbyB rdCbyAB wrCbyB wrBbyB",
            [0x04] = "wrAbyB rdCbyAB wrBbyB",
            [0x05] = "rdCbyAB wrCbyB",
            [0x06] = "rdCbyAB",
            [0x07] = "rdCbyAB"
        };

        static string GetAccessConditionsDesc(int blockn, byte[] data)
        {
            byte data1 = (byte)(((data[1] >> 4) & 0x0f) >> blockn);
            byte data2 = (byte)(((data[2]) & 0x0f) >> blockn);
            byte data3 = (byte)(((data[2] >> 4) & 0x0f) >> blockn);

            byte cond = (byte)((data1 & 0x01) << 2 | (data2 & 0x01) << 1 | (data3 & 0x01));

            if (blockn == 3)
            {
                if (MfAccessConditionsTrailer.ContainsKey(cond))
                    return MfAccessConditionsTrailer[cond];
            }
            else
            {
                if (MfAccessConditions.ContainsKey(cond))
                    return MfAccessConditions[cond];
            }
            return "none";
        }
    }
}
