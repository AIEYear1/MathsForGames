using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class InputField : Button
    {
        public string OutString { get; private set; } = "";
        public bool HasInput
        {
            get => OutString != "";
        }

        // string that contains / shows player input
        string inputText = "";
        // string that holds placeHolderText
        string placeHolderText = "";

        // Number of chars the InputField can hold
        uint charLimit = 0;

        // Whether the input field is taking player input or not
        bool isInputing = false;
        
        public InputField(Vector2 position, float width, float height, Color buttonColor, Vector2 textPosition, string text, int fontSize, Color textColor, float dim, uint charLimit) : base(position, width, height, buttonColor, textPosition, text, fontSize, textColor, dim)
        {
            this.charLimit = charLimit;
            placeHolderText = text;
        }

        public override void Update()
        {
            curColor = color;
            curTextColor = textColor;
            if (isInputing)
            {
                if (inputText.Length < charLimit && Utils.KeyboardInput(out string input))
                {
                    inputText += input;
                    if(inputText.Length >= charLimit)
                    {
                        inputText = inputText.Substring(0, (int)charLimit);
                    }
                }

                if (IsKeyPressed(KeyboardKey.KEY_BACKSPACE) && inputText.Length > 0)
                {
                    inputText = inputText.Substring(0, inputText.Length - 1);
                }

                text = inputText;

                if(IsKeyPressed(KeyboardKey.KEY_ENTER) || (!CheckCollisionPointRec(GetMousePosition(), rectangle) && IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)))
                {
                    isInputing = false;
                    OutString = inputText;
                    text = (inputText != "") ? inputText : placeHolderText;
                }
                return;
            }

            if (CheckCollisionPointRec(GetMousePosition(), rectangle))
            {
                curColor = buttonHighlightColor;
                curTextColor = textHighlightColor;

                if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    inputText = "";
                    isInputing = true;
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
