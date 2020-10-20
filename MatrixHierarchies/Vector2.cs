using System;

namespace MatrixHierarchies
{
    public struct Vector2
    {
        public float x;
        public float y;

        public static Vector2 Up
        {
            get => new Vector2(0, 1);
        }
        public static Vector2 Right
        {
            get => new Vector2(1, 0);
        }
        public static Vector2 One
        {
            get => new Vector2(1, 1);
        }
        public static Vector2 Zero
        {
            get => new Vector2();
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float Magnitude()
        {
            return MathF.Sqrt((x * x) + (y * y));
        }
        public float MagnitudeSqr()
        {
            float mag = Magnitude();
            return mag * mag;
        }

        public float Distance(Vector2 point)
        {
            float distX = point.x - x;
            float distY = point.y - y;
            return MathF.Sqrt((distX * distX) + (distY * distY));
        }
        public float DistanceSqr(Vector2 point)
        {
            float dist = Distance(point);
            return dist * dist;
        }

        public void Normalize()
        {
            float multiplier = 1 / Magnitude();
            x *= multiplier;
            y *= multiplier;
        }
        public Vector2 Normalised()
        {
            Vector2 toReturn = this;
            toReturn.Normalize();
            return toReturn;
        }

        public float Dot(Vector2 vector)
        {
            return (x * vector.x) + (y * vector.y);
        }
        public Vector2 GetPerpendicular()
        {
            return new Vector2(y, -x);
        }

        /// <summary>
        /// finds the angle between two Vectors
        /// </summary>
        public float AngleBetween(Vector2 vector)
        {
            float toReturn = MathF.Acos(Dot(vector) / (Magnitude() * vector.Magnitude()));
            return toReturn * (180 / MathF.PI);
        }

        public static Vector2 Clamp(Vector2 point, Vector2 minPoint, Vector2 maxPoint)
        {
            point.x = (point.x > maxPoint.x) ? maxPoint.x : (point.x < minPoint.x) ? minPoint.x : point.x;
            point.y = (point.y > maxPoint.y) ? maxPoint.y : (point.y < minPoint.y) ? minPoint.y : point.y;
            return point;
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
        }
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x / scalar, vector.y / scalar);
        }
        public static Vector2 operator *(float scalar, Vector2 vector)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }
        public static Vector2 operator /(float scalar, Vector2 vector)
        {
            return new Vector2(vector.x / scalar, vector.y / scalar);
        }
        public static implicit operator System.Numerics.Vector2(Vector2 vector)
        {
            return new System.Numerics.Vector2(vector.x, vector.y);
        }
        public static implicit operator Vector2(System.Numerics.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
        public static explicit operator Vector2(Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}
