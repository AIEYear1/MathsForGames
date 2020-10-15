using System.Numerics;

namespace Matrices
{
    struct Matrix2
    {
        float[][] matrix;

        public float[] Row1
        {
            get
            {
                return new float[]
                {
                    matrix[0][0],
                    matrix[1][0]
                };
            }
            set
            {
                matrix[0][0] = value[0];
                matrix[1][0] = value[1];
            }
        }
        public float[] Row2
        {
            get
            {
                return new float[]
                {
                    matrix[0][1],
                    matrix[1][1]
                };
            }
            set
            {
                matrix[0][0] = value[0];
                matrix[1][0] = value[1];
            }
        }
        public float[] Column1
        {
            get => matrix[0];
            set => matrix[0] = value;
        }
        public float[] Column2
        {
            get => matrix[1];
            set => matrix[1] = value;
        }
        public float this[int column, int row]
        {
            get => matrix[column][row];
            set => matrix[column][row] = value;
        }
        public static Matrix2 Identity
        {
            get => new float[] { 1, 0, 0, 1 };
        }

        public Matrix2(float[][] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix2(float c1r1, float c1r2, float c2r1, float c2r2)
        {
            matrix = new float[][]
            {
                new float[]
                {
                    c1r1,
                    c1r2
                },
                new float[]
                {
                    c2r1,
                    c2r2
                }
            };
        }

        public void Transpose()
        {
            float tmpFloat = matrix[0][1];
            matrix[0][1] = matrix[1][0];
            matrix[1][0] = tmpFloat;
        }

        public static Matrix2 operator *(Matrix2 lhs, Matrix2 rhs)
        {
            return new Matrix2((lhs[0, 0] * rhs[0, 0]) + (lhs[1, 0] * rhs[0, 1]), (lhs[0, 1] * rhs[0, 0]) + (lhs[1, 1] * rhs[0, 1]),
                               (lhs[0, 0] * rhs[1, 0]) + (lhs[1, 0] * rhs[1, 1]), (lhs[0, 1] * rhs[1, 0]) + (lhs[1, 1] * rhs[1, 1]));
        }
        public static Vector2 operator *(Matrix2 lhs, Vector2 rhs)
        {
            return new Vector2((lhs[0, 0] * rhs.x) + (lhs[1, 0] * rhs.y), (lhs[0, 1] * rhs.x) + (lhs[1, 1] * rhs.y));
        }
        public static implicit operator Matrix2(float[] values)
        {
            return new Matrix2(values[0], values[1], values[2], values[3]);
        }
    }
}
