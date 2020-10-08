using System;

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
            Console.WriteLine(BinSubtraction("1101", "11", 8));
            Console.WriteLine(BinSubtraction("10001", "100", 8));
        }

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

            return toReturn;
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

        // TODO: SUBTRACTION DOES NOT WORK
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            bin1 = bin1.PadLeft(length);
            bin1 = bin1.Substring(bin1.Length - length);

            bin2 = bin2.PadLeft(length);
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

                    toReturn = toReturn.Insert(0, "0");
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
    }
}
