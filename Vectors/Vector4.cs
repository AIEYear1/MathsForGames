using System;

namespace Vectors
{
    class Vector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Vector4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public float Magnitude()
        {
            return MathF.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
        }
        public float MagnitudeSqr()
        {
            float mag = Magnitude();
            return mag * mag;
        }

        public float Distance(Vector4 point)
        {
            float distX = point.x - x;
            float distY = point.y - y;
            float distZ = point.z - z;
            float distW = point.w - w;
            return MathF.Sqrt((distX * distX) + (distY * distY) + (distZ * distZ) + (distW * distW));
        }
        public float DistanceSqr(Vector4 point)
        {
            float dist = Distance(point);
            return dist * dist;
        }

        public void Normalised()
        {
            float multiplier = 1 / Magnitude();
            x *= multiplier;
            y *= multiplier;
            z *= multiplier;
            w *= multiplier;
        }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
        }
        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        }
        public static Vector4 operator *(Vector4 vector, float scalar)
        {
            return new Vector4(vector.x * scalar, vector.y * scalar, vector.z * scalar, vector.w * scalar);
        }
        public static Vector4 operator /(Vector4 vector, float scalar)
        {
            return new Vector4(vector.x / scalar, vector.y / scalar, vector.z / scalar, vector.w / scalar);
        }
        public static Vector4 operator *(float scalar, Vector4 vector)
        {
            return new Vector4(vector.x * scalar, vector.y * scalar, vector.z * scalar, vector.w * scalar);
        }
        public static Vector4 operator /(float scalar, Vector4 vector)
        {
            return new Vector4(vector.x / scalar, vector.y / scalar, vector.z / scalar, vector.w / scalar);
        }
    }
}
