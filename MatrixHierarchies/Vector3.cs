using System;

namespace MatrixHierarchies
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public static Vector3 One
        {
            get => new Vector3(1, 1, 1);
        }
        public static Vector3 Zero
        {
            get => new Vector3();
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }
        public float MagnitudeSqr()
        {
            float mag = Magnitude();
            return mag * mag;
        }

        public float Distance(Vector3 point)
        {
            float distX = point.x - x;
            float distY = point.y - y;
            float distZ = point.z - z;
            return (float)Math.Sqrt((distX * distX) + (distY * distY) + (distZ * distZ));
        }
        public float DistanceSqr(Vector3 point)
        {
            float dist = Distance(point);
            return dist * dist;
        }

        public void Normalize()
        {
            float multiplier = 1 / Magnitude();
            x *= multiplier;
            y *= multiplier;
            z *= multiplier;
        }

        public float Dot(Vector3 vector)
        {
            return (x * vector.x) + (y * vector.y) + (z * vector.z);
        }
        public static float Dot(Vector3 vector1, Vector3 vector2)
        {
            return (vector2.x * vector1.x) + (vector2.y * vector1.y) + (vector2.z * vector1.z);
        }
        public Vector3 Cross(Vector3 vector)
        {
            return new Vector3((y * vector.z) - (z * vector.y), (z * vector.x) - (x * vector.z), (x * vector.y) - (y * vector.x));
        }

        /// <summary>
        /// finds the angle between to Vectors
        /// </summary>
        public float AngleBetween(Vector3 vector)
        {
            float toReturn = (float)Math.Acos(Dot(vector) / (Magnitude() * vector.Magnitude()));
            return toReturn * (float)(180 / Math.PI);
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }
        public static Vector3 operator *(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
        }
        public static Vector3 operator /(Vector3 vector, float scalar)
        {
            return new Vector3(vector.x / scalar, vector.y / scalar, vector.z / scalar);
        }
        public static Vector3 operator *(float scalar, Vector3 vector)
        {
            return new Vector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
        }
        public static Vector3 operator /(float scalar, Vector3 vector)
        {
            return new Vector3(vector.x / scalar, vector.y / scalar, vector.z / scalar);
        }
        public static implicit operator Vector3(float[] values)
        {
            return new Vector3(values[0], values[1], values[2]);
        }
        public static implicit operator Vector3(Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0);
        }
    }
}
