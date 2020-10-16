using System.Linq;
using Raylib_cs;
using System;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class BoxCollider : Collider
    {
        protected Vector2 topLeftPoint = new Vector2(), topRightPoint = new Vector2();
        protected Vector2 bottomLeftPoint = new Vector2(), bottomRightPoint = new Vector2();

        Matrix3 rotationMatrix = new Matrix3();

        public Vector2 TopLeftPoint
        {
            get => topLeftPoint;
        }
        public Vector2 TopRightPoint
        {
            get => topRightPoint;
        }
        public Vector2 BottomLeftPoint
        {
            get => bottomLeftPoint;
        }
        public Vector2 BottomRightPoint
        {
            get => bottomRightPoint;
        }

        public BoxCollider(Vector2 position, float width, float height, float rotation) : base (position)
        {
            width /= 2;
            height /= 2;

            topLeftPoint = position;
            topLeftPoint.x -= width;
            topLeftPoint.y += height;

            topRightPoint = position;
            topRightPoint.x += width;
            topRightPoint.y += height;

            bottomLeftPoint = position;
            bottomLeftPoint.x -= width;
            bottomLeftPoint.y -= height;

            bottomRightPoint = position;
            bottomRightPoint.x += width;
            bottomRightPoint.y -= height;

            if(rotation != 0)
            {
                Rotate(rotation);
            }
        }

        public void SetPosition(Vector2 pos)
        {
            topLeftPoint -= Position;
            topLeftPoint += pos;

            topRightPoint -= Position;
            topRightPoint += pos;

            bottomLeftPoint -= Position;
            bottomLeftPoint += pos;

            bottomRightPoint -= Position;
            bottomRightPoint += pos;

            position = pos;
        }

        public void Rotate(float radians)
        {
            rotationMatrix.SetRotateZ(radians);

            Vector2 tmpVector = topLeftPoint - position;
            tmpVector = (Vector2)(rotationMatrix * tmpVector);
            topLeftPoint = tmpVector + position;

            tmpVector = topRightPoint - position;
            tmpVector = (Vector2)(rotationMatrix * tmpVector);
            topRightPoint = tmpVector + position;

            tmpVector = bottomLeftPoint - position;
            tmpVector = (Vector2)(rotationMatrix * tmpVector);
            bottomLeftPoint = tmpVector + position;

            tmpVector = bottomRightPoint - position;
            tmpVector = (Vector2)(rotationMatrix * tmpVector);
            bottomRightPoint = tmpVector + position;
        }

        public void Debug()
        {
            DrawCircleV(topLeftPoint, 10, Color.MAROON);
            DrawCircleV(topRightPoint, 10, Color.MAROON);
            DrawCircleV(bottomLeftPoint, 10, Color.MAROON);
            DrawCircleV(bottomRightPoint, 10, Color.MAROON);
        }

        //public static bool BoxCollision(BoxCollider lhs, BoxCollider rhs)
        //{
        //    if ((lhs.topRightPoint - lhs.position).Magnitude() + 
        //        (rhs.topRightPoint - rhs.position).Magnitude() < (lhs.position - rhs.position).Magnitude())
        //    {
        //        return false;
        //    }

        //    Vector2[] axes = new Vector2[]
        //    {
        //        lhs.topRightPoint - lhs.topLeftPoint,
        //        lhs.topRightPoint - lhs.bottomRightPoint,
        //        rhs.topLeftPoint - rhs.bottomLeftPoint,
        //        rhs.topLeftPoint - rhs.topRightPoint,
        //    };

        //    for (int x = 0; x < 4; x++)
        //    {
        //        Vector2 axis = axes[x];
        //        float[] lhsProjected = new float[]
        //        {
        //            ((lhs.topLeftPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
        //            ((lhs.topRightPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
        //            ((lhs.bottomLeftPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
        //            ((lhs.bottomRightPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
        //        };
        //        float[] rhsProjected = new float[]
        //        {
        //            ((rhs.topLeftPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
        //            ((rhs.topRightPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
        //            ((rhs.bottomLeftPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
        //            ((rhs.bottomRightPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
        //        };

        //        if (rhsProjected.Min() > lhsProjected.Max() || rhsProjected.Max() < lhsProjected.Min())
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}
