using System;
using System.Linq;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    abstract class Collider
    {
        protected Vector2 position = new Vector2();

        public virtual Vector2 Position
        {
            get => position;
        }

        public Collider(Vector2 position)
        {
            this.position = position;
        }
        public Collider()
        {

        }

        public static bool BoxCollision(BoxCollider lhs, BoxCollider rhs)
        {
            if (lhs.position.Distance(rhs.position) > 
               (lhs.TopRightPoint - lhs.position).Magnitude() + (rhs.TopRightPoint - rhs.position).Magnitude())
            {
                return false;
            }

            Vector2[] axes = new Vector2[]
            {
                lhs.TopRightPoint - lhs.TopLeftPoint,
                lhs.TopRightPoint - lhs.BottomRightPoint,
                rhs.TopLeftPoint - rhs.BottomLeftPoint,
                rhs.TopLeftPoint - rhs.TopRightPoint,
            };

            for (int x = 0; x < 4; x++)
            {
                Vector2 axis = axes[x];
                float[] lhsProjected = new float[]
                {
                    ((lhs.TopLeftPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
                    ((lhs.TopRightPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
                    ((lhs.BottomLeftPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
                    ((lhs.BottomRightPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
                };
                float[] rhsProjected = new float[]
                {
                    ((rhs.TopLeftPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
                    ((rhs.TopRightPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
                    ((rhs.BottomLeftPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
                    ((rhs.BottomRightPoint.Dot(axis) / axis.MagnitudeSqr()) * axis).Dot(axis),
                };

                if (rhsProjected.Min() > lhsProjected.Max() || rhsProjected.Max() < lhsProjected.Min())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CircleCollision(CircleCollider lhs, CircleCollider rhs)
        {
            return lhs.position.Distance(rhs.position) < lhs.Radius + rhs.Radius;
        }

        public static bool BoxCircleCollision(BoxCollider lhs, CircleCollider rhs)
        {
            // Check to see if they are near enough to collide
            if (lhs.position.Distance(rhs.position) > (lhs.TopRightPoint - lhs.position).Magnitude() + rhs.Radius)
            {
                return false;
            }

            // Calc box width and height
            float recWidth = lhs.TopLeftPoint.Distance(lhs.TopRightPoint);
            float recHeight = lhs.TopLeftPoint.Distance(lhs.BottomLeftPoint);
            // Make UnRotated Rectangle in the same spot as the rotated rectangle
            Rectangle rec = new Rectangle(lhs.position.x - (recWidth / 2), lhs.position.y - (recHeight / 2), 
                                          recWidth, recHeight);

            // Calculate the amount of rotation in the rectangle in degrees using the top left corner of the rectangle
            Vector2 orig = lhs.TopLeftPoint - lhs.position;
            Vector2 unRotated = new Vector2(rec.x, rec.y) - lhs.position;
            float angleRad = MathF.Atan2(unRotated.y, unRotated.x) - MathF.Atan2(orig.y, orig.x);
            angleRad = (angleRad < 0) ? angleRad + (2 * MathF.PI) : angleRad;

            // Create a rotation matrix using angle
            Matrix3 rotationMatrix = new Matrix3();
            rotationMatrix.RotateZ(angleRad);

            // Rotate circle around rectangle to match rectangle
            Vector2 circlePos = (Vector2)(rotationMatrix * (rhs.position - lhs.position)) + lhs.position;


            #region Debug
            //DrawRectangleLinesEx(rec, 5, Color.MAROON);
            //DrawCircleV(circlePos, 10, Color.MAROON);
            //DrawText(angleRad.ToString(), 0, 460, 20, Color.MAROON);
            #endregion


            // Check collision
            Vector2 closestCirclePos = Vector2.Clamp(circlePos, new Vector2(rec.x, rec.y), 
                                                     new Vector2(rec.x + rec.width, rec.y + rec.height));

            return (circlePos.Distance(closestCirclePos) <= rhs.Radius);
        }
        public static bool BoxCircleCollision(CircleCollider lhs, BoxCollider rhs)
        {
            // Check to see if they are near enough to collide
            if (rhs.position.Distance(lhs.position) > (rhs.TopRightPoint - rhs.position).Magnitude() + lhs.Radius)
            {
                return false;
            }

            // Calc box width and height
            float recWidth = rhs.TopLeftPoint.Distance(rhs.TopRightPoint);
            float recHeight = rhs.TopLeftPoint.Distance(rhs.BottomLeftPoint);
            // Make UnRotated Rectangle in the same spot as the rotated rectangle
            Rectangle rec = new Rectangle(rhs.position.x - (recWidth / 2), rhs.position.y - (recHeight / 2),
                                          recWidth, recHeight);

            // Calculate the amount of rotation in the rectangle in degrees using the top left corner of the rectangle
            Vector2 orig = rhs.TopLeftPoint - rhs.position;
            Vector2 unRotated = new Vector2(rec.x, rec.y) - rhs.position;
            float angleRad = MathF.Atan2(unRotated.y, unRotated.x) - MathF.Atan2(orig.y, orig.x);
            angleRad = (angleRad < 0) ? angleRad + (2 * MathF.PI) : angleRad;

            // Create a rotation matrix using angle
            Matrix3 rotationMatrix = new Matrix3();
            rotationMatrix.RotateZ(angleRad);

            // Rotate circle around rectangle to match rectangle
            Vector2 circlePos = (Vector2)(rotationMatrix * (lhs.position - rhs.position)) + rhs.position;


            #region Debug
            //DrawRectangleLinesEx(rec, 5, Color.MAROON);
            //DrawCircleV(circlePos, 10, Color.MAROON);
            //DrawText(angleRad.ToString(), 0, 460, 20, Color.MAROON);
            #endregion


            // Check collision
            Vector2 closestCirclePos = Vector2.Clamp(circlePos, new Vector2(rec.x, rec.y),
                                                     new Vector2(rec.x + rec.width, rec.y + rec.height));

            return (circlePos.Distance(closestCirclePos) <= lhs.Radius);
        }
    }
}
