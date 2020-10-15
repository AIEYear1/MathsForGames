using System;

namespace Matrices
{
    class Program
    {
        static void Main(string[] args)
        {
            //    Matrix3 mat1 = new float[]
            //    {
            //        1,4,7,
            //        2,5,8,
            //        3,6,9
            //    };
            //    Matrix3 mat2 = new float[]
            //    {
            //        9,6,3,
            //        8,5,2,
            //        7,4,1
            //    };
            //    Matrix3 result = mat1 * mat2;

            //    Console.WriteLine($"{result[0, 0]},{result[0, 1]},{result[0, 2]}\n" +
            //                      $"{result[1, 0]},{result[1, 1]},{result[1, 2]}\n" +
            //                      $"{result[2, 0]},{result[2, 1]},{result[2, 2]}");

            //Matrix3 m3b = new Matrix3(1, 0, 0,
            //                          0, 1, 0,
            //                          55, 44, 1);

            //Vector3 v3a = new Vector3(13.5f, -48.23f, 0);

            //Vector3 v3b = m3b * v3a;

            //Console.WriteLine(v3b.ToString());
            // homogeneous point translation
            Matrix3 m3b = new Matrix3(1, 0, 0,
                                      0, 1, 0,
                                      55, 44, 1);

            Vector3 v3a = new Vector3(13.5f, -48.23f, 1);

            Vector3 v3b = m3b * v3a;
            Console.WriteLine(v3b.ToString());
        }
    }
    }
