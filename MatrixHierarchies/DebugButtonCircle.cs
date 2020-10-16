using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class DebugButtonCircle : SceneObject
    {
        public float radius = 0;

        CircleCollider collider;
        Color curColor = Color.WHITE;
        Color defaultColor = Color.RED;
        Color pressedColor = Color.GREEN;
        Tank ObjectColliding;

        public DebugButtonCircle(Vector2 position, float radius, Tank tank)
        {
            SetPosition(position.x, position.y);
            this.radius = radius;
            collider = new CircleCollider(position, radius);

            ObjectColliding = tank;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (Collider.BoxCircleCollision(ObjectColliding.collider, collider))
            {
                curColor = pressedColor;
            }
            else
            {
                curColor = defaultColor;
            }

            collider.SetPosition(Position);
            base.OnUpdate(deltaTime);
        }

        public override void OnDraw()
        {
            if (CheckCollisionCircleRec(Position, radius, Program.ScreenSpace))
            {
                DrawCircleV(Position, radius, curColor);
            }
        }
    }
}
