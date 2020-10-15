using System;

namespace MatrixHierarchies
{
    public class Matrix3
    {
        float[][] matrix;

        public float[] Row1
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
        public float[] Row2
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
        public float[] Row3
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
        public float[] Column3
        {
            get => matrix[2];
            set => matrix[2] = value;
        }
        public float this[int column, int row]
        {
            get => matrix[column][row];
            set => matrix[column][row] = value;
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

        public Matrix3(float c1r1, float c1r2, float c1r3,
                       float c2r1, float c2r2, float c2r3,
                       float c3r1, float c3r2, float c3r3)
        {
            matrix = new float[][]
            {
                new float[]
                {
                    c1r1, c1r2, c1r3
                },
                new float[]
                {
                    c2r1, c2r2, c2r3
                },
                new float[]
                {
                    c3r1, c3r2, c3r3
                }
            };
        }
        public Matrix3()
        {
            matrix = Identity.matrix;
        }

        public void SetTranslation(float x, float y)
        {
            m7 = x;
            m8 = y;
            m9 = 1;
        }
        public void Translate(float x, float y)
        {
            m7 += x;
            m8 += y;
        }

        public void SetRotateX(float rot)
        {
            //1, 0,         0
            //0, cos(rot),  sin(rot)
            //0, -sin(rot), cos(rot)
            Matrix3 tmp = new float[]
            {
                1, 0,                      0,
                0, (float)Math.Cos(rot),  (float)Math.Sin(rot),
                0, -(float)Math.Sin(rot), (float)Math.Cos(rot)
            };
            matrix = tmp.matrix;
        }
        public void SetRotateY(float rot)
        {
            //cos(rot), 0, -sin(rot)
            //0,        1, 0
            //sin(rot), 0, cos(rot)
            Matrix3 tmp = new float[]
            {
                (float)Math.Cos(rot), 0, -(float)Math.Sin(rot),
                0,                    1, 0,
                (float)Math.Sin(rot), 0, (float)Math.Cos(rot)
            };
            matrix = tmp.matrix;
        }
        public void SetRotateZ(float rot)
        {
            //cos(rot),  sin(rot), 0
            //-sin(rot), cos(rot), 0
            //0,         0,        1
            Matrix3 tmp = new float[]
            {
                (float)Math.Cos(rot), (float)Math.Sin(rot),  0,
                -(float)Math.Sin(rot), (float)Math.Cos(rot), 0,
                0,                      0,                   1
            };
            matrix = tmp.matrix;
        }

        public void RotateX(float rot)
        {
            //1, 0,         0
            //0, cos(rot),  sin(rot)
            //0, -sin(rot), cos(rot)
            matrix = (this * (Matrix3)new float[]
            {
                1, 0,               0,
                0, (float)Math.Cos(rot),  (float)Math.Sin(rot),
                0, (float)-Math.Sin(rot), (float)Math.Cos(rot)
            }).matrix;
        }
        public void RotateY(float rot)
        {
            //cos(rot), 0, -sin(rot)
            //0,        1, 0
            //sin(rot), 0, cos(rot)
            matrix = (this * (Matrix3)new float[]
            {
                (float)Math.Cos(rot), 0, (float)-Math.Sin(rot),
                0,              1, 0,
                (float)Math.Sin(rot), 0, (float)Math.Cos(rot)
            }).matrix;
        }
        public void RotateZ(float rot)
        {
            //cos(rot),  sin(rot), 0
            //-sin(rot), cos(rot), 0
            //0,         0,        1
            matrix = (this * (Matrix3)new float[]
            {
                (float)Math.Cos(rot),  (float)Math.Sin(rot), 0,
                (float)-Math.Sin(rot), (float)Math.Cos(rot), 0,
                0,               0,              1
            }).matrix;
        }

        public void Rotate(float pitch, float yaw, float roll)
        {
            pitch *= ((float)Math.PI / 180);
            yaw *= ((float)Math.PI / 180);
            roll *= ((float)Math.PI / 180);

            matrix = Matrix3.Identity.matrix;

            RotateZ(roll);
            RotateY(yaw);
            RotateX(pitch);
        }

        public void SetScale(float x, float y, float z)
        {
            Matrix3 tmp = new float[]
            {
                x, 0, 0,
                0, y, 0,
                0, 0, z
            };
            matrix = tmp.matrix;
        }
        public void Scale(float x, float y, float z)
        {
            matrix = (this * (Matrix3)new float[]
            {
                x, 0, 0,
                0, y, 0,
                0, 0, z
            }).matrix;
        }

        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            //float a = (lhs[0, 0] * rhs[0, 0]) + (lhs[1, 0] * rhs[0, 1]) + (lhs[2, 0] * rhs[0, 2]);
            ////
            //float b = (lhs[0, 0] * rhs[1, 0]) + (lhs[1, 0] * rhs[1, 1]) + (lhs[2, 0] * rhs[1, 2]);
            //float c = (lhs[0, 0] * rhs[2, 0]) + (lhs[1, 0] * rhs[2, 1]) + (lhs[2, 0] * rhs[2, 2]);
            ////
            //float d = (lhs[0, 1] * rhs[0, 0]) + (lhs[1, 1] * rhs[0, 1]) + (lhs[2, 1] * rhs[0, 2]);
            //float e = (lhs[0, 1] * rhs[1, 0]) + (lhs[1, 1] * rhs[1, 1]) + (lhs[2, 1] * rhs[1, 2]);
            ////
            //float f = (lhs[0, 1] * rhs[2, 0]) + (lhs[1, 1] * rhs[2, 1]) + (lhs[2, 1] * rhs[2, 2]);
            //float g = (lhs[0, 2] * rhs[0, 0]) + (lhs[1, 2] * rhs[0, 1]) + (lhs[2, 2] * rhs[0, 2]);
            ////
            //float h = (lhs[0, 2] * rhs[1, 0]) + (lhs[1, 2] * rhs[1, 1]) + (lhs[2, 2] * rhs[1, 2]);
            //float i = (lhs[0, 2] * rhs[2, 0]) + (lhs[1, 2] * rhs[2, 1]) + (lhs[2, 2] * rhs[2, 2]);
            float A = Vector3.Dot(lhs.Row1, rhs.Column1);
            float B = Vector3.Dot(lhs.Row1, rhs.Column2);
            float C = Vector3.Dot(lhs.Row1, rhs.Column3);
            float D = Vector3.Dot(lhs.Row2, rhs.Column1);
            float E = Vector3.Dot(lhs.Row2, rhs.Column2);
            float F = Vector3.Dot(lhs.Row2, rhs.Column3);
            float G = Vector3.Dot(lhs.Row3, rhs.Column1);
            float H = Vector3.Dot(lhs.Row3, rhs.Column2);
            float I = Vector3.Dot(lhs.Row3, rhs.Column3);
            return new Matrix3(A, D, G,
                               B, E, H,
                               C, F, I);
        }
        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3(Vector3.Dot(lhs.Row1, rhs),
                               Vector3.Dot(lhs.Row2, rhs),
                               Vector3.Dot(lhs.Row3, rhs));
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

        #region the great ms
        public float m1
        {
            get => this[0, 0];
            set => this[0, 0] = value;
        }
        public float m2
        {
            get => this[0, 1];
            set => this[0, 1] = value;
        }
        public float m3
        {
            get => this[0, 2];
            set => this[0, 2] = value;
        }
        public float m4
        {
            get => this[1, 0];
            set => this[1, 0] = value;
        }
        public float m5
        {
            get => this[1, 1];
            set => this[1, 1] = value;
        }
        public float m6
        {
            get => this[1, 2];
            set => this[1, 2] = value;
        }
        public float m7
        {
            get => this[2, 0];
            set => this[2, 0] = value;
        }
        public float m8
        {
            get => this[2, 1];
            set => this[2, 1] = value;
        }
        public float m9
        {
            get => this[2, 2];
            set => this[2, 2] = value;
        }
        #endregion
    }
}
