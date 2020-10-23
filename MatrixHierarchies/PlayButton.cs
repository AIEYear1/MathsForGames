using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class PlayButton : UISprite
    {
        Vector2 position;
        string text;
        int fontSize;
        Color textColor;

        Color curTextColor;
        Color curColor;

        public PlayButton(Vector2 position, float width, float height, Color buttonColor, Vector2 textPosition, string text, int fontSize, Color textColor) : base(position, width, height, buttonColor)
        {
            this.position = textPosition;
            this.text = text;
            this.fontSize = fontSize;
            this.textColor = textColor;
            curColor = buttonColor;
            curTextColor = textColor;
        }

        public void Update()
        {
            curColor = color;
            curTextColor = textColor;
            if (CheckCollisionPointRec(GetMousePosition(), rectangle))
            {
                curColor.r -= 11;
                curColor.g -= 11;
                curColor.b -= 11;

                curTextColor.r -= 11;
                curTextColor.g -= 11;
                curTextColor.b -= 11;

                if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    Program.state = GameState.Play;
                    EnemyManager.Paused = false;
                }

                return;
            }
        }

        public override void Draw()
        {
            DrawRectangleRec(rectangle, curColor);
            DrawText(text, (int)(Position.x + position.x), (int)(Position.y + position.y), fontSize, curTextColor);
        }
    }
}
