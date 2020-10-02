using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Vectors
{
    class Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public float Magnitude()
        {
            return MathF.Sqrt((x * x) + (y * y) + (z * z));
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
            return MathF.Sqrt((distX * distX) + (distY * distY) + (distZ * distZ));
        }
        public float DistanceSqr(Vector3 point)
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
        }

        public float Dot(Vector3 vector)
        {
            return (x * vector.x) + (y * vector.y) + (z * vector.z);
        }
        public Vector3 Cross(Vector3 vector)
        {
            return new Vector3((y * vector.z) - (z * vector.y), (z * vector.x) - (x * vector.z), (x * vector.y) - (y - vector.x));
        }

        /// <summary>
        /// finds the angle between to Vectors
        /// </summary>
        public float AngleBetween(Vector3 vector)
        {
            float toReturn = MathF.Acos(Dot(vector) / (Magnitude() * vector.Magnitude()));
            return toReturn * (180 / MathF.PI);
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
    }
}
