using System.Diagnostics.SymbolStore;

namespace ChameleonMiniGUI
{
    public class Crc
    {
        public const ushort CRC16_14443_A = 0x6363;
        public const ushort CRC16_14443_B = 0xFFFF;

        private static ushort UpdateCrc14443(byte b, ushort crc)
        {
            unchecked
            {
                byte ch = (byte) (b ^ (byte) (crc & 0x00ff));
                ch = (byte) (ch ^ (ch << 4));
                return (ushort) ((crc >> 8) ^ (ch << 8) ^ (ch << 3) ^ (ch >> 4));
            }
        }

        public static void ComputeCrc14443(ushort crc_type, byte[] bytes, int len, out byte first, out byte second)
        {
            first = 0;
            second = 0;

            if (len < 2)
                return;

            byte b;
            var res = crc_type;

            for (int i = 0; i < len; i++)
            {
                b = bytes[i];
                res = UpdateCrc14443(b, res);
            }


            if (crc_type == CRC16_14443_B)
                res = (ushort) ~res;                /* ISO/IEC 13239 (formerly ISO/IEC 3309) */


            first = (byte) (res & 0xFF);
            second = (byte) ((res >> 8) & 0xFF);
        }

        public static bool CheckCrc14443(ushort crc_type, byte[] bytes, int len)
        {
            byte b1, b2;
            if (len < 3) return false;

            ComputeCrc14443(crc_type, bytes, len - 2, out b1, out b2);
            if ((b1 == bytes[len - 2]) && (b2 == bytes[len - 1]))
                return true;
            return false;
        }
    }
}
 