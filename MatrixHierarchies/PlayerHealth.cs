using Raylib_cs;

namespace MatrixHierarchies
{
    class PlayerHealth : UISprite
    {
        UISprite healthBar;

        public PlayerHealth(Vector2 position, float width, float height) : base(position, width, height, Color.RED)
        {
            healthBar = new UISprite(position, width, height, Color.GREEN);
        }

        public override void Draw()
        {
            base.Draw();
            healthBar.Draw();
        }

        public void SetHealth(float value)
        {
            healthBar.Width = Width * value;
        }
    }
}
