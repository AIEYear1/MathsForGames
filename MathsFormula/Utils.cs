using System;
using System.Collections.Generic;
using System.Text;

public static class Utils
{
    public static void Add(ref int amount, int amountToAdd)
    {
        for (int x = 0; x < amountToAdd; x++)
        {
            amount++;
        }
    }
    public static void Multiply(ref int amount, int amountToMultiplyBy)
    {
        int tmp = amount;
        for (int x = 1; x < amountToMultiplyBy; x++)
        {
            Add(ref amount, tmp);
        }
    }
    public static void Exponent(ref int amount, int amountToExponentBy)
    {
        int tmp = amount;
        for (int x = 1; x < amountToExponentBy; x++)
        {
            Multiply(ref amount, tmp);
        }
    }

    public static void Subtract(ref int amount, int amountToSubtract)
    {
        for (int x = 0; x < amountToSubtract; x++)
        {
            amount--;
        }
    }
    public static void Divide(ref int amount, int amountToDivideBy)
    {
        int tmp = amount;

        for (int x = 1; tmp > 0; x++)
        {
            Subtract(ref tmp, amountToDivideBy);
            amount = x;
        }
    }
    public static void Root(ref int amount, int amountToRootBy)
    {
        int tmpInt = 0;
        int tmpAmount = amount;

        Exponent(ref tmpInt, amountToRootBy);
        for (int x = 1; tmpInt != tmpAmount; x++)
        {
            tmpInt = x;
            Exponent(ref tmpInt, amountToRootBy);
            amount = x;
        }
    }
}
