using System;

namespace Trigonometry
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 rad = " + RadToDeg(1) + "\n");

            Console.WriteLine("60 deg = " + DegToRad(60) + "\n");

            Console.WriteLine("The Eqyptians did this by using the now commonly known 3 4 5 triangle\n" +
                "marking that, so long as each segment between the knots were the same length,\n" +
                "and they split the segments into a 5 length side, a 4 length side, and a 3 length side\n" +
                "the resulting triangle will always be a right triangle.\n");

            double small = Math.Atan(3.0 / 4.0);
            double larger = Math.Atan(4.0 / 3.0);
            Console.WriteLine($"The two other angles are about {small} radians or {RadToDeg((float)small)} degrees,\n" +
                $"and about {larger} radians or {RadToDeg((float)larger)} degrees\n");

            Console.WriteLine("the side Length is: " + 13 * (Math.Sin(DegToRad(79)) / Math.Sin(DegToRad(37))) + " centimeters\n");

            Console.WriteLine("the angle is: " + RadToDeg(MathF.Acos(((9*9)+(6*6)-(8*8)) / (float)(2*9*6))) + " degrees\n");

            double[] angles = FindAngles(8, 9, 6);
            Console.WriteLine($"the Solved triangles angles are: a = {angles[0]}, b = {angles[1]}, c = {angles[2]} \n");
        }

        /// <summary>
        /// Converts from Degrees to Radians
        /// </summary>
        /// <param name="deg">Degree angle to convert</param>
        /// <returns>returns an agnle in radian</returns>
        public static double DegToRad(float deg)
        {
            double toReturn = ((deg < 0) ? deg + 360 : deg);
            return toReturn * (MathF.PI / 180);
        }
        /// <summary>
        /// Converts from Radians to Degrees
        /// </summary>
        /// <param name="rad">Radian angle to convert</param>
        /// <returns>Returns an angle in degress</returns>
        public static double RadToDeg(float rad)
        {
            double toReturn = (double)rad * (double)(180 / MathF.PI);
            return (toReturn > 180) ? toReturn - 360 : toReturn;
        }

        static double[] FindAngles(float A, float B, float C)
        {
            double[] toReturn = new double[3];
            toReturn[0] = Math.Acos(((B * B) + (C * C) - (A * A)) / (2f * B * C));
            toReturn[1] = Math.Asin((Math.Sin(toReturn[0]) / A) * B);
            toReturn[2] = Math.Asin((Math.Sin(toReturn[0]) / A) * C);

            for (int x = 0; x < toReturn.Length; x++)
            {
                toReturn[x] = RadToDeg((float)toReturn[x]);
            }

            return toReturn;
        }
    }
}
