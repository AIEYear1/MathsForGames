using Raylib_cs;

namespace MatrixHierarchies
{
    abstract class Button : UISprite
    {
        protected Vector2 position;
        protected string text;
        protected int fontSize;
        protected Color textColor, textHighlightColor, buttonHighlightColor;

        protected Color curTextColor;
        protected Color curColor;

        public Button(Vector2 position, float width, float height, Color buttonColor, Vector2 textPosition, string text, int fontSize, Color textColor, float dim) : base(position, width, height, buttonColor)
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

        public abstract void Update();

        public abstract override void Draw();
    }
}
