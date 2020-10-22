using Raylib_cs;
using System.Linq.Expressions;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class SimpleSpriteObject : SceneObject
    {
        public float width, height;

        Color color;

        public SimpleSpriteObject(Vector2 position, float width, float height, Color color)
        {
            this.width = width;
            this.height = height;
            this.color = color;

            collider = (BoxCollider)(new Rectangle(position.x, position.y, width, height));

            SetPosition(position.x, position.y);
        }

        public override void OnUpdate(float deltaTime)
        {
            collider.SetPosition(Position + new Vector2(width / 2, height / 2));
            base.OnUpdate(deltaTime);
        }

        public override void OnDraw()
        {
            if (Collider.Collision(collider, (BoxCollider)Program.ScreenSpace))
            {
                DrawRectangle((int)Position.x, (int)Position.y, (int)width, (int)height, color);
            }
        }
    }
}
