using Raylib_cs;
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

        public BoxCollider(Vector2 position, float width, float height, float rotation) : base(position)
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

            if (rotation != 0)
            {
                Rotate(rotation);
            }
        }

        public override void SetPosition(Vector2 pos)
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

        public override void Rotate(float radians)
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

        public override void Debug()
        {
            DrawCircleV(topLeftPoint, 10, Color.MAROON);
            DrawCircleV(topRightPoint, 10, Color.MAROON);
            DrawCircleV(bottomLeftPoint, 10, Color.MAROON);
            DrawCircleV(bottomRightPoint, 10, Color.MAROON);
        }

        public static implicit operator BoxCollider(Rectangle rec)
        {
            return new BoxCollider(new Vector2(rec.x + (rec.width / 2), rec.y + (rec.height / 2)), rec.width, rec.height, 0);
        }
        public static explicit operator Rectangle(BoxCollider box)
        {
            // Calc box width and height
            float recWidth = box.TopLeftPoint.Distance(box.TopRightPoint);
            float recHeight = box.TopLeftPoint.Distance(box.BottomLeftPoint);

            return new Rectangle(box.position.x - (recWidth / 2), box.position.y - (recHeight / 2), recWidth, recHeight);
        }
    }
}
