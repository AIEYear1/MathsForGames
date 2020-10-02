using System;

namespace Vectors
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Magnitude:");
            Console.WriteLine("a. " + new Vector3(1, 1, 1).Magnitude());
            Console.WriteLine("b. " + new Vector2(3, -2).Magnitude());
            Console.WriteLine("c. " + new Vector3(-1, 1, -1).Magnitude());
            Console.WriteLine("d. " + new Vector3(0.5f, -1, 0.25f).Magnitude());
            Console.WriteLine();
            Console.WriteLine("Distance:");
            Console.WriteLine("a. " + new Vector2(-2, 5.5f).Distance(new Vector2(9, -22)));
            Console.WriteLine("b. " + new Vector3(0, 1, 2).Distance(new Vector3(9, -2, 7)));
            Console.WriteLine("\n--------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Dot Product:");
            Console.WriteLine("a. " + new Vector2(1, 0).Dot(new Vector2(0, 1)));
            Console.WriteLine("b. " + new Vector2(1, 1).Dot(new Vector2(-1, -1)));
            Console.WriteLine("c. " + new Vector3(2, 3, 1).Dot(new Vector3(-3, 1, 2)));
            Console.WriteLine();
            Console.WriteLine("Angle Between:");
            Console.WriteLine("a. " + new Vector2(1, 3).AngleBetween(new Vector2(0.5f, -0.25f)));
            Console.WriteLine("b. " + new Vector3(-0.5f, 0, 2).AngleBetween(new Vector3(-1, 0, -1)));
        }
    }
}
