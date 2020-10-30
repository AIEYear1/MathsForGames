using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TestProject
{
    class Program
    {
        static int valCount = 50;
        static List<int> vals = new List<int>();
        static void Main()
        {
            for(int curVal = 1; curVal <= valCount; curVal++)
            {
                int tmp = (curVal > 1) ? vals[curVal - 2] / 2 : 0;
                tmp += 6 + (int)(2.0f * (curVal - 1));
                if (curVal > 6)
                    for (int i = 0; i < curVal / 6; i++)
                        tmp += 1 + (int)(curVal / 3);
                if (curVal > 12)
                    for (int i = 0; i < (curVal / 6) - 1; i++)
                        tmp += 1 + (int)(curVal / 3);
                if (curVal > 24)
                    for (int i = 0; i < (curVal / 6) - 2; i++)
                        tmp += 1 + (int)(curVal / 3);
                vals.Add(tmp);
            }

            //for (int x = 1; x < vals.Count; x++)
            //{
            //    Console.WriteLine("\t" + (vals[x] - vals[x - 1]));
            //}
            for (int x = 0; x < vals.Count; x++)
            {
                Console.WriteLine((x + 1) + ": " + vals[x]);
            }
        }
    }
}
