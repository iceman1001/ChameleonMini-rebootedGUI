using Crapto1Sharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace ChameleonMiniGUI
{
    public class MyKey
    {
        public UInt32 UID;
        public UInt32 nt0;
        public UInt32 nt1;
        public UInt32 nr0;
        public UInt32 nr1;
        public UInt32 ar0;
        public UInt32 ar1;
        public UInt64 key;
        public byte Sector;
        public byte Block;
        public byte KeyType;
        public bool Found;
        public const UInt64 DoNotDecryptCanary = 0x5245564556312E34;
        public const int DoNotDecryptCanaryOffset = 8;
        public const int DoNotDecryptCanarySize = 8;
    }

    /*
     * Downloaded data from device should be 208 byte plus 2 for CRC. (210)
     *  4 bytes uid,
     *  12 empty bytes
     *  192 bytes of collected nonce
     * Layout like this::
     *
     *  byte 0 - 3 == UID
     *  byte 4 - 15 == empty
     *  --repeating 16 bytes
     *  byte 16  == keytype A/B
     *  byte 17  == sector
     *  byte 20 - 23 == NT
     *  byte 24 - 27 == NR
     *  byte 28 - 31 == AR
     *  --
     *  byte 32  == keytype A/B
     *  byte 33  == sector
     *  byte 36 - 39 == NT
     *  byte 40 - 43 == NR
     *  byte 44 - 47 == AR
     *
     *  In order to run mfkey32mobieus attack, you need two authentication tries against same sector and keytype (A/B).
     *
     */


    public partial class MfKeyAttacks
    {
        public MfKeyAttacks()
        {
        }

        public MfKeyAttacks(bool doSelfTest)
        {
            if ( doSelfTest )
                selftest();
        }

        private void selftest()
        {
            // MOEBIUS test
            //   <uid>      <nt>       <nr_0>     <ar_0>     <nt1>      <nr_1>    <ar_1>
            // 0x12345678 0x1AD8DF2B 0x1D316024 0x620EF048 0x30D6CB07 0xC52077E2 0x837AC61A
            //  Found Key: [a0a1a2a3a4a5]

            var t = new MyKey
            {
                UID = 0x12345678,
                nt0 = 0x1AD8DF2B,
                nr0 = 0x1D316024,
                ar0 = 0x620EF048,
                nt1 = 0x30D6CB07,
                nr1 = 0xC52077E2,
                ar1 = 0x837AC61A
            };
            t.key = MfKey.MfKey32(t.UID, t.nt0, t.nr0, t.ar0, t.nt1, t.nr1, t.ar1);
            t.Found = t.key != ulong.MaxValue;
            if (t.Found && t.key == 0xa0a1a2a3a4a5)
            {
                var s = $"[S{t.Sector}/B%d] Type {t.KeyType} Key found [{t.key:x12}] {Environment.NewLine} ";
                Debug.WriteLine(s);
            }

            // MFKEY32 standard
            //::        < uid >    < nt >     < nr_0 >   < ar_0 >   < nr_1 >   < ar_1 >
            //         0x52B0F519 0x5417D1F8 0x4D545EA7 0xE15AC8C2 0xDAC1A7F4 0x5AE5C37F
            //t.UID = 0x52B0F519;
            //t.nt0 = 0x5417D1F8; t.nr0 = 0x4D545EA7; t.ar0 = 0xE15AC8C2;
            //                    t.nr1 = 0xDAC1A7F4; t.ar1 = 0x5AE5C37F;

            //t.Found = mfkey32(t.UID, t.nt0, t.nr0, t.ar0, t.nr1, t.ar1, out t.key);
            //if (t.Found)
            //{
            //    var s = $"[S{t.Sector}/B%d] Type {t.KeyType} Key found [{t.key:x12}] {Environment.NewLine} ";
            //    Debug.WriteLine(s);
            //}
        }

        private static UInt32 ToUInt32(byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian)
            {
                return BitConverter.ToUInt32(data.Skip(offset).Take(4).Reverse().ToArray(), 0);
            }
            return BitConverter.ToUInt32(data, offset);
        }

        private static UInt64 ToUInt64(byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian)
            {
                return BitConverter.ToUInt64(data.Skip(offset).Take(8).Reverse().ToArray(), 0);
            }
            return BitConverter.ToUInt64(data, offset);
        }

        public static string Attack(byte[] bytes)
        {
            var show_all = "";
            if (bytes == null || !bytes.Any())
                return $"No data found on device{Environment.NewLine}";

            UInt64 canary = ToUInt64(bytes, MyKey.DoNotDecryptCanaryOffset);
            if (canary != MyKey.DoNotDecryptCanary)
            {
                // Decrypt data,  with key 123321,  length 208
                DecryptData(bytes, 123321, 208);
            }

            // validate CRC is ok.  (length 210,  since two last bytes is crc)
            if (!Crc.CheckCrc14443(Crc.CRC16_14443_A, bytes, 210))
                return $"Data failed CRC check{Environment.NewLine}";

            /*
            * Data layout
            * first 16byte is Sector0, Block0
            *
            * then comes items of 16bytes length
            *   0           auth cmd  (0x60 or 0x61)
                1           blocknumber  (0 - 0x7F)
                2,3         crc 2bytes
                4,5,6,7     NT
                8,9,10,11   NR
                12,13,14,15 AR
            */

            var uid = ToUInt32(bytes, 0);

            var myKeys = new List<MyKey>();

            // Copy nonce - data into object and list
            for (int i = 0; i < 12; i++)
            {
                var mykey = new MyKey
                {
                    UID = uid,
                    KeyType = bytes[(i + 1) * 16],
                    Block = bytes[(i + 1) * 16 + 1],
                    nt0 = ToUInt32(bytes, (i + 1) * 16 + 4),
                    nr0 = ToUInt32(bytes, (i + 1) * 16 + 8),
                    ar0 = ToUInt32(bytes, (i + 1) * 16 + 12)
                };
                mykey.Sector = ToSector(mykey.Block);

                // skip sectors with 0xFF
                if ( mykey.Sector != 0xFF)
                    myKeys.Add(mykey);
            }


            var my_cmp = new KeyComparer();
            myKeys.Sort(my_cmp);

            show_all = KeyWorker(myKeys);
            return show_all;
        }

        public static byte ToSector(byte block)
        {
            // 32 first sectors has 4blocks
            if (block < 128)
                return  (byte)(block/4);

            // above 32, they have 16blocks
            return (byte)(32 + (block - 128)/16);
        }

        private static string KeyWorker(List<MyKey> keys)
        {
            var results = keys
                .GroupBy(k => new { k.UID, k.Sector, k.Block, k.KeyType })
                .AsParallel()
                .AsOrdered()
                .Select(group =>
                {
                    var list = group.Select(k => new Nonce()
                    {
                        Nt = k.nt0,
                        Nr = k.nr0,
                        Ar = k.ar0
                    }).ToList();

                    if (list.Count < 2)
                        return null;

                    var keyType = (group.Key.KeyType == 0x60) ? "A" : "B";
                    Debug.WriteLine($"{group.Key.Sector} - {keyType} | {list.Count}");

                    var key = MfKey.MfKey32(group.Key.UID, list);
                    if (key != ulong.MaxValue)
                    {
                        foreach (var item in group)
                        {
                            item.Found = true;
                            item.key = key;
                        }
                        var s = $"[S{group.Key.Sector} / B{group.Key.Block}] Key{keyType} [{key:x12}]";
                        Debug.WriteLine(s);
                        return s;
                    }
                    return null;
                })
                .Where(r => r != null)
                .ToList();
            results.Add(string.Empty);
            return string.Join(Environment.NewLine, results);
        }

        // Takes encrypted data from device,  decodes it, and puts the decoded data back to same array.
        static void DecryptData(byte[] enc_arr, uint key, int size)
        {
            var arr = new byte[size];
            Array.Copy(enc_arr, arr, size);

            for (int i = 0; i < size; i++)
            {
                int s = arr[i];
                var t = size + key + i - size / key ^ s;
                enc_arr[i] = (byte)t;
            }
        }
    }
}
