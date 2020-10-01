using System;

namespace SideThing
{
    class Program
    {
        static void Main()
        {
            int x = 3;
            Console.WriteLine("Start " + x);
            Add(ref x, -4);
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


        public static void Add(ref int amount, int amountToAdd)
        {
            if (amountToAdd < 0)
            {
                Subtract(ref amount, -amountToAdd);
                return;
            }

            for (int x = 0; x < amountToAdd; x++)
            {
                amount++;
            }
        }
        public static void Multiply(ref int amount, int amountToMultiplyBy)
        {
            bool multIsNeg = false;
            bool amountIsNeg = false;
            if (amount < 0)
            {
                amount = -amount;
                amountIsNeg = true;
            }
            if (amountToMultiplyBy < 0)
            {
                amountToMultiplyBy = -amountToMultiplyBy;
                multIsNeg = true;
            }

            int tmp = amount;
            for (int x = 1; x < amountToMultiplyBy; x++)
            {
                Add(ref amount, tmp);
            }

            if (amountIsNeg ^ multIsNeg)
            {
                amount = -amount;
            }
        }
        public static void Exponent(ref int amount, int amountToExponentBy)
        {
            int tmp = amount;
            if (amountToExponentBy < 0)
            {
                amountToExponentBy = -amountToExponentBy;
                for (int x = 1; x < amountToExponentBy; x++)
                {
                    Divide(ref amount, tmp);
                }
                return;
            }

            for (int x = 1; x < amountToExponentBy; x++)
            {
                Multiply(ref amount, tmp);
            }
        }

        public static void Subtract(ref int amount, int amountToSubtract)
        {
            if (amountToSubtract < 0)
            {
                Add(ref amount, -amountToSubtract);
                return;
            }

            for (int x = 0; x < amountToSubtract; x++)
            {
                amount--;
            }
        }
        public static void Divide(ref int amount, int amountToDivideBy)
        {
            bool divIsNeg = false;
            bool amountIsNeg = false;
            if (amount < 0)
            {
                amount = -amount;
                amountIsNeg = true;
            }
            if (amountToDivideBy < 0)
            {
                amountToDivideBy = -amountToDivideBy;
                divIsNeg = true;
            }

            int tmp = amount;
            amount = 0;
            for (int x = 1; tmp > amountToDivideBy; x++)
            {
                Subtract(ref tmp, amountToDivideBy);
                amount = x;
            }

            if (amountIsNeg ^ divIsNeg)
            {
                amount = -amount;
            }
        }
        public static void Remainder(ref int amount, int amountToDivideBy)
        {
            bool divIsNeg = false;
            bool amountIsNeg = false;
            if (amount < 0)
            {
                amount = -amount;
                amountIsNeg = true;
            }
            if (amountToDivideBy < 0)
            {
                amountToDivideBy = -amountToDivideBy;
                divIsNeg = true;
            }

            for (int x = 1; amount > amountToDivideBy; x++)
            {
                Subtract(ref amount, amountToDivideBy);
            }

            if (amountIsNeg ^ divIsNeg)
            {
                amount = -amount;
            }
        }
        public static void Root(ref int amount, int amountToRootBy)
        {
            bool amountIsNeg = false;
            if (amount < 0)
            {
                amount = -amount;
                amountIsNeg = true;
            }

            int tmpInt = 0;
            int tmpAmount = amount;
            amount = 0;

            Exponent(ref tmpInt, amountToRootBy);
            for (int x = 1; tmpInt != tmpAmount; x++)
            {
                tmpInt = x;
                Exponent(ref tmpInt, amountToRootBy);
                amount = x;
            }

            if (amountIsNeg)
            {
                Remainder(ref amountToRootBy, 2);
                if (amountToRootBy != 0)
                {
                    amount = -amount;
                }
            }
        }
    }
}
