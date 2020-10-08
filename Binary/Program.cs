﻿using System;
using System.Collections;
using System.ComponentModel;

namespace Binary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DecToBin(1, 8));
            Console.WriteLine(DecToBin(42));
            Console.WriteLine(DecToBin(256));
            Console.WriteLine(DecToBin(4294967296));
            Console.WriteLine();
            Console.WriteLine(BinToDec("010000000"));
            Console.WriteLine(BinToDec("010101010"));
            Console.WriteLine(BinToDec("011110000"));
            Console.WriteLine(BinToDec("011001100"));
            Console.WriteLine();
            Console.WriteLine(BinAddition("111", "111"));
            Console.WriteLine(BinAddition("1010", "1010"));
            Console.WriteLine(BinSubtraction("01101", "011", 8));
            Console.WriteLine(BinSubtraction("010001", "0100", 8));
            Console.WriteLine(BinMultiplication("101", "10"));
            Console.WriteLine(BinMultiplication("1011", "11"));
            Console.WriteLine(BinDivision("01101", "011"));
            Console.WriteLine();
            Console.WriteLine(BinToDec("10000000"));
            Console.WriteLine(BinToDec("10101010"));
            Console.WriteLine(BinToDec("11110000"));
            Console.WriteLine(BinToDec("11001100"));
            Console.WriteLine(DecToBin(-16));
            Console.WriteLine(DecToBin(-128));
            Console.WriteLine(DecToBin(128));
            Console.WriteLine(DecToBin(-123));
            Console.WriteLine();
            Console.WriteLine(BinOr("11111", "11111"));
            Console.WriteLine(BinXor("11111", "11111"));
            Console.WriteLine(BinAnd("10101", "11111"));
            Console.WriteLine(BinOr("10101", "11111"));
            Console.WriteLine(BinXor("00000", "11111"));
            Console.WriteLine(LeftShift("01", 3));
            Console.WriteLine(RightShift("0100", 2));
            Console.WriteLine(BinNot("10101"));
            Console.WriteLine(LeftShift("0100", 1));
            Console.WriteLine(RightShift("1010", 1));
            Console.WriteLine(BinNot("11111"));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(IsLeftMostBitSet(32));
            Console.WriteLine(IsRightMostBitSet(25));
            Console.WriteLine(IsBitSet(13, 2));
            Console.WriteLine(GetRightMostSetBit(40));
            PrintBinary(12);
            Console.WriteLine(IsPowerOfTwo(4));
        }
        #region My Binary shiz that makes my soul hurt
        static string DecToBin(long dec)
        {
            if (dec == 0)
            {
                return "0";
            }

            string toReturn = "";
            bool isNeg = dec < 0;
            dec *= (isNeg ? -1 : 1);

            while (dec != 0)
            {
                toReturn = toReturn.Insert(0, (dec % 2).ToString());
                dec /= 2;
            }

            if (isNeg)
            {
                toReturn = BinToNeg(toReturn);
            }

            return (isNeg ? "1" : "0") + toReturn;
        }
        static string DecToBin(long dec, int length)
        {
            string toReturn = "";
            bool isNeg = dec < 0;
            dec *= (isNeg ? -1 : 1);
            while (dec != 0)
            {
                toReturn = toReturn.Insert(0, (dec % 2).ToString());
                dec /= 2;
            }

            if (isNeg)
            {
                toReturn = BinToNeg(toReturn);
            }

            if(toReturn.Length < length)
            {
                toReturn = toReturn.PadLeft(length, '0');
            }

            return toReturn.Substring(toReturn.Length - length);
        }
        static long BinToDec(string bin)
        {
            long toReturn = 0;

            if(bin[0] == '1')
            {
                bin = BinFromNeg(bin);
            }

            for (int x = bin.Length - 1; x > -1; x--)
            {
                uint.TryParse(bin[x].ToString(), out uint bit);
                toReturn += bit << bin.Length - 1 - x;
            }
            return toReturn;
        }

        static string BinToNeg(string bin)
        {
            string toReturn = "";
            for(int x = 0; x < bin.Length; x++)
            {
                toReturn += (bin[x] == '1') ? '0' : '1';
            }
            return "1" + BinAddition(toReturn, "1");
        }
        static string BinFromNeg(string bin)
        {
            string toReturn = "";
            for (int x = 0; x < bin.Length; x++)
            {
                toReturn += (bin[x] == '1') ? '0' : '1';
            }
            return BinAddition(toReturn, "1");
        }

        static string BinSubtraction(string bin1, string bin2, int length)
        {
            bin2 = BinToNeg(bin2);
            return BinAddition(bin1, bin2, length);
        }

        // Unsigned
        static string BinAddition(string bin1, string bin2)
        {
            string toReturn = "";
            bool carryOver = false;

            if(bin1.Length > bin2.Length)
            {
                bin2 = bin2.PadLeft(bin1.Length, '0');
            }
            else if (bin1.Length < bin2.Length)
            {
                bin1 = bin1.PadLeft(bin2.Length, '0');
            }
            
            for(int x = bin1.Length-1; x > -1; x--)
            {
                if (carryOver)
                {
                    if (bin1[x] == '1' && bin2[x] == '1')
                    {
                        toReturn = toReturn.Insert(0, "1");
                        continue;
                    }

                    if (bin1[x] == '1' || bin2[x] == '1')
                    {
                        toReturn = toReturn.Insert(0, "0");
                        continue;
                    }

                    toReturn = toReturn.Insert(0, "1");
                    carryOver = false;
                    continue;
                }

                if(bin1[x] == '1' && bin2[x] == '1')
                {
                    toReturn = toReturn.Insert(0, "0");
                    carryOver = true;
                    continue;
                }

                if(bin1[x] == '1' || bin2[x] == '1')
                {
                        toReturn = toReturn.Insert(0, "1");
                    continue;
                }

                toReturn = toReturn.Insert(0, "0");
            }

            if (carryOver)
            {
                toReturn = toReturn.Insert(0, "1");
            }

            return toReturn;
        }
        // Signed
        static string BinAddition(string bin1, string bin2, int length)
        {
            string toReturn = "";
            bool carryOver = false;

            bin1 = bin1.PadLeft(length, bin1[0]);
            bin1 = bin1.Substring(bin1.Length - length);

            bin2 = bin2.PadLeft(length, bin2[0]);
            bin2 = bin2.Substring(bin1.Length - length);

            for (int x = bin1.Length - 1; x > -1; x--)
            {
                if (carryOver)
                {
                    if (bin1[x] == '1' && bin2[x] == '1')
                    {
                        toReturn = toReturn.Insert(0, "1");
                        continue;
                    }

                    if (bin1[x] == '1' || bin2[x] == '1')
                    {
                        toReturn = toReturn.Insert(0, "0");
                        continue;
                    }

                    toReturn = toReturn.Insert(0, "1");
                    carryOver = false;
                    continue;
                }

                if (bin1[x] == '1' && bin2[x] == '1')
                {
                    toReturn = toReturn.Insert(0, "0");
                    carryOver = true;
                    continue;
                }

                if (bin1[x] == '1' || bin2[x] == '1')
                {
                    toReturn = toReturn.Insert(0, "1");
                    continue;
                }

                toReturn = toReturn.Insert(0, "0");
            }

            return toReturn;
        }

        static string BinMultiplication(string bin1, string bin2)
        {
            string[] mults = new string[bin2.Length];

            int count = 0;
            for(int x = bin2.Length - 1; x > -1; x--)
            {
                int.TryParse(bin2[x].ToString(), out int bit2);
                string toAdd = "";
                for(int y = bin1.Length - 1; y > -1; y--)
                {
                    int.TryParse(bin1[y].ToString(), out int bit1);
                    toAdd = toAdd.Insert(0, (bit1 * bit2).ToString());
                }

                for (int z = 0; z < count; z++)
                {
                    toAdd += '0';
                }
                mults[count] = toAdd;
                count++;
            }

            string toReturn = mults[0];

            for(int x = 1; x < mults.Length; x++)
            {
                toReturn = BinAddition(toReturn, mults[x]);
            }

            return toReturn;
        }

        static string BinDivision(string bin1, string bin2)
        {
            int bin2Size = (int)BinToDec(bin2);
            string miniBin = "0";
            string toReturn = "";

            for(int x = 0; x < bin1.Length; x++)
            {
                miniBin += bin1[x];
                long miniBinSize = BinToDec(miniBin);
                
                if(miniBinSize >= bin2Size)
                {
                    miniBin = BinSubtraction(miniBin, bin2, bin1.Length);
                    toReturn += '1';
                    continue;
                }

                toReturn += '0';
            }

            return toReturn;
        }

        static string BinOr(string bin1, string bin2)
        {
            string toReturn = "";


            if (bin1.Length > bin2.Length)
            {
                bin2 = bin2.PadLeft(bin1.Length, '0');
            }
            else if (bin1.Length < bin2.Length)
            {
                bin1 = bin1.PadLeft(bin2.Length, '0');
            }

            for (int x = 0; x < bin1.Length; x++)
            {
                toReturn += (bin1[x] == '1' || bin2[x] == '1') ? '1' : '0';
            }

            return toReturn;
        }
        static string BinXor(string bin1, string bin2)
        {
            string toReturn = "";


            if (bin1.Length > bin2.Length)
            {
                bin2 = bin2.PadLeft(bin1.Length, '0');
            }
            else if (bin1.Length < bin2.Length)
            {
                bin1 = bin1.PadLeft(bin2.Length, '0');
            }

            for (int x = 0; x < bin1.Length; x++)
            {
                toReturn += (bin1[x] == '1' ^ bin2[x] == '1') ? '1' : '0';
            }

            return toReturn;
        }
        static string BinAnd(string bin1, string bin2)
        {
            string toReturn = "";


            if (bin1.Length > bin2.Length)
            {
                bin2 = bin2.PadLeft(bin1.Length, '0');
            }
            else if (bin1.Length < bin2.Length)
            {
                bin1 = bin1.PadLeft(bin2.Length, '0');
            }

            for (int x = 0; x < bin1.Length; x++)
            {
                toReturn += (bin1[x] == '1' && bin2[x] == '1') ? '1' : '0';
            }

            return toReturn;
        }
        static string BinNot(string bin)
        {
            string toReturn = "";

            for (int x = 0; x < bin.Length; x++)
            {
                toReturn += (bin[x] == '0') ? '1' : '0';
            }

            return toReturn;
        }
        static string LeftShift(string bin, int shift)
        {
            return bin.PadRight(bin.Length + shift, '0');
        }
        static string RightShift(string bin, int shift)
        {
            int length = bin.Length;
            return bin.PadLeft(length + shift, '0').Substring(0, length);
        }
        #endregion

        static bool IsLeftMostBitSet(uint value)
        {
            BitArray bitArray = new BitArray(BitConverter.GetBytes(value));
            return bitArray[bitArray.Count - 1];
        }
        static bool IsRightMostBitSet(uint value)
        {
            BitArray bitArray = new BitArray(BitConverter.GetBytes(value));
            return bitArray[0];
        }
        static bool IsBitSet(uint value, ushort bitToCheck)
        {
            BitArray bitArray = new BitArray(BitConverter.GetBytes(value));
            return bitArray[bitToCheck];
        }
        static int GetRightMostSetBit(uint value)
        {
            BitArray bitArray = new BitArray(BitConverter.GetBytes(value));

            for(int x = 0; x < bitArray.Count; x++)
            {
                if (bitArray[x])
                {
                    return 1 << x;
                }
            }

            return -1;
        }
        static void PrintBinary(uint value)
        {
            Console.WriteLine(DecToBin(value));
        }
        static bool IsPowerOfTwo(ulong x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }
    }
}
