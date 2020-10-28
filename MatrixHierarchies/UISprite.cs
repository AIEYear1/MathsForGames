using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class UISprite
    {
        public Rectangle rectangle;
        public Color color;

        public Vector2 Position
        {
            get => new Vector2(rectangle.x, rectangle.y);
            set
            {
                rectangle.x = value.x;
                rectangle.y = value.y;
            }
        }
        public float Width
        {
            get => rectangle.width;
            set => rectangle.width = value;
        }
        public float Height
        {
            get => rectangle.height;
            set => rectangle.height = value;
        }

        public UISprite(Vector2 position, float width, float height, Color color)
        {
            rectangle = new Rectangle(position.x, position.y, width, height);
            this.color = color;
        }

        public virtual void Draw()
        {
            DrawRectangleRec(rectangle, color);
        }
    }
}
