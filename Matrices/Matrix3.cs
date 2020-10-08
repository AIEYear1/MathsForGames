using System.Numerics;

namespace Matrices
{
    struct Matrix3
    {
        float[][] matrix;

        public float[] Column1
        {
            get
            {
                return new float[]
                {
                    matrix[0][0],
                    matrix[1][0],
                    matrix[2][0]
                };
            }
            set
            {
                matrix[0][0] = value[0];
                matrix[1][0] = value[1];
                matrix[2][0] = value[2];
            }
        }
        public float[] Column2
        {
            get
            {
                return new float[]
                {
                    matrix[0][1],
                    matrix[1][1],
                    matrix[2][1]
                };
            }
            set
            {
                matrix[0][1] = value[0];
                matrix[1][1] = value[1];
                matrix[2][1] = value[2];
            }
        }
        public float[] Column3
        {
            get
            {
                return new float[]
                {
                    matrix[0][2],
                    matrix[1][2],
                    matrix[2][2]
                };
            }
            set
            {
                matrix[0][2] = value[0];
                matrix[1][2] = value[1];
                matrix[2][2] = value[2];
            }
        }
        public float[] Row1
        {
            get => matrix[0];
            set => matrix[0] = value;
        }
        public float[] Row2
        {
            get => matrix[1];
            set => matrix[1] = value;
        }
        public float[] Row3
        {
            get => matrix[2];
            set => matrix[2] = value;
        }
        public float this[int row, int column]
        {
            get => matrix[row][column];
            set => matrix[row][column] = value;
        }
        public static Matrix3 Identity
        {
            get => new float[] { 1, 0, 0,
                                 0, 1, 0,
                                 0, 0, 1 };
        }

        public Matrix3(float[][] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix3(float c1r1, float c2r1, float c3r1, float c1r2, float c2r2, float c3r2, float c1r3, float c2r3, float c3r3)
        {
            matrix = new float[][]
            {
                new float[]
                {
                    c1r1,
                    c2r1,
                    c3r1
                },
                new float[]
                {
                    c1r2,
                    c2r2,
                    c3r2
                },
                new float[]
                {
                    c1r3,
                    c2r3,
                    c3r3
                }
            };
        }

        public void Transpose()
        {
            float tmpFloat = matrix[0][1];
            matrix[0][1] = matrix[1][0];
            matrix[1][0] = tmpFloat;
        }

        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3((lhs[0, 0] * rhs[0, 0]) + (lhs[0, 1] * rhs[1, 0]) + (lhs[0, 2] * rhs[2, 0]), (lhs[0, 0] * rhs[0, 1]) + (lhs[0, 1] * rhs[1, 1]) + (lhs[0, 2] * rhs[2, 1]),
                               (lhs[0, 0] * rhs[0, 2]) + (lhs[0, 1] * rhs[1, 2]) + (lhs[0, 2] * rhs[2, 2]), (lhs[1, 0] * rhs[0, 0]) + (lhs[1, 1] * rhs[1, 0]) + (lhs[1, 2] * rhs[2, 0]),
                               (lhs[1, 0] * rhs[0, 1]) + (lhs[1, 1] * rhs[1, 1]) + (lhs[1, 2] * rhs[2, 1]), (lhs[1, 0] * rhs[0, 2]) + (lhs[1, 1] * rhs[1, 2]) + (lhs[1, 2] * rhs[2, 2]),
                               (lhs[2, 0] * rhs[0, 0]) + (lhs[2, 1] * rhs[1, 0]) + (lhs[2, 2] * rhs[2, 0]), (lhs[2, 0] * rhs[0, 1]) + (lhs[2, 1] * rhs[1, 1]) + (lhs[2, 2] * rhs[2, 1]),
                               (lhs[2, 0] * rhs[0, 2]) + (lhs[2, 1] * rhs[1, 2]) + (lhs[2, 2] * rhs[2, 2]));
        }
        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3((lhs[0, 0] * rhs.X) + (lhs[0, 1] * rhs.Y) + (lhs[0, 2] * rhs.Z),
                               (lhs[1, 0] * rhs.X) + (lhs[1, 1] * rhs.Y) + (lhs[1, 2] * rhs.Z),
                               (lhs[2, 0] * rhs.X) + (lhs[2, 1] * rhs.Y) + (lhs[2, 2] * rhs.Z));
        }

        public static Matrix3 operator +(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(lhs[0, 0] + rhs[0, 0], lhs[0, 1] + rhs[0, 1], lhs[0, 2] + rhs[0, 2],
                               lhs[1, 0] + rhs[1, 0], lhs[1, 1] + rhs[1, 1], lhs[1, 2] + rhs[1, 2],
                               lhs[2, 0] + rhs[2, 0], lhs[2, 1] + rhs[2, 1], lhs[2, 2] + rhs[2, 2]);
        }

        public static Matrix3 operator -(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(lhs[0, 0] - rhs[0, 0], lhs[0, 1] - rhs[0, 1], lhs[0, 2] - rhs[0, 2],
                               lhs[1, 0] - rhs[1, 0], lhs[1, 1] - rhs[1, 1], lhs[1, 2] - rhs[1, 2],
                               lhs[2, 0] - rhs[2, 0], lhs[2, 1] - rhs[2, 1], lhs[2, 2] - rhs[2, 2]);
        }

        public static implicit operator Matrix3(float[] values)
        {
            return new Matrix3(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8]);
        }
    }
}
