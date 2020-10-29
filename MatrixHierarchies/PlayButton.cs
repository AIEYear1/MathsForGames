﻿using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class PlayButton : UISprite
    {
        Vector2 position;
        string text;
        int fontSize;
        Color textColor, textHighlightColor, buttonHighlightColor;

        Color curTextColor;
        Color curColor;

        public PlayButton(Vector2 position, float width, float height, Color buttonColor, Vector2 textPosition, string text, int fontSize, Color textColor, float dim) : base(position, width, height, buttonColor)
        {
            this.position = textPosition;
            this.text = text;
            this.fontSize = fontSize;
            this.textColor = textColor;
            curColor = buttonColor;
            curTextColor = textColor;
            textHighlightColor = (ColorRGB)textColor - dim;
            buttonHighlightColor = (ColorRGB)buttonColor - dim;
        }

        public void Update()
        {
            if (CheckCollisionPointRec(GetMousePosition(), rectangle))
            {
                curColor = buttonHighlightColor;
                curTextColor = textHighlightColor;

                if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    Game.currentState = GameState.PLAY;
                    EnemyManager.Paused = false;
                }

                return;
            }

            curColor = color;
            curTextColor = textColor;
        }

        public override void Draw()
        {
            DrawRectangleRec(rectangle, curColor);
            DrawText(text, (int)(Position.x + position.x), (int)(Position.y + position.y), fontSize, curTextColor);
        }
    }
}
