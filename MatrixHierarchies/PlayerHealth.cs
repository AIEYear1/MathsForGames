using Raylib_cs;

namespace MatrixHierarchies
{
    class PlayerHealth : UISprite
    {
        public Timer health;
        UISprite healthBar;

        public PlayerHealth(Vector2 position, float width, float height, float healthValue) : base(position, width, height, Color.RED)
        {
            healthBar = new UISprite(position, width, height, Color.GREEN);
            health = new Timer(healthValue);
        }

        public void Update()
        {
            healthBar.Width = Width * (health.TimeRemaining / health.delay);
        }
    }
}
