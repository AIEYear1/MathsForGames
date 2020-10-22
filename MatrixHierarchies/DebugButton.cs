using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class DebugButton : SceneObject
    {
        Color curColor = Color.WHITE;
        Color defaultColor = Color.RED;
        Color pressedColor = Color.GREEN;
        Rectangle rectangle;
        Tank ObjectColliding;

        public DebugButton(Rectangle rec, Tank tank)
        {
            SetPosition(rec.x, rec.y);
            rectangle = rec;
            collider = new BoxCollider(new Vector2(rec.x + (rec.width / 2), rec.y + (rec.height / 2)),
                                       rec.width, rec.height, 0);

            ObjectColliding = tank;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (Collider.Collision(collider, ObjectColliding.collider))
            {
                curColor = pressedColor;
            }
            else
            {
                curColor = defaultColor;
            }

            rectangle.x = globalTransform.m7;
            rectangle.y = globalTransform.m8;
            collider.SetPosition(new Vector2(rectangle.x + (rectangle.width / 2), rectangle.y + (rectangle.height / 2)));
            base.OnUpdate(deltaTime);
        }

        public override void OnDraw()
        {
            if (CheckCollisionRecs(Program.ScreenSpace, rectangle))
            {
                DrawRectangleRec(rectangle, curColor);
            }
        }
    }
}
