using System;
using static Utils;

namespace MathsFormula
{
    class Program
    {
        static void Main()
        {
            int x = 3;
            Console.WriteLine("Start " + x);
            Add(ref x, 4);
            Console.WriteLine("After Add " + x);
            Multiply(ref x, 3);
            Console.WriteLine("After Multiply " + x);
            Exponent(ref x, 2);
            Console.WriteLine("After Exponent " + x);
            Root(ref x, 2);
            Console.WriteLine("After Root " + x);
            Divide(ref x, 3);
            Console.WriteLine("After Divide " + x);
            Subtract(ref x, 4);
            Console.WriteLine("After Subtract " + x);
        }

    }
}
