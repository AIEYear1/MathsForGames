using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class UI
    {
        static Texture2D ammoSprite;

        public static void Initialize()
        {
            Image img = LoadImage("bulletBlue_outline.png");
            ammoSprite = LoadTextureFromImage(img);
        }

        public static void Draw(Tank player)
        {
            DrawAmmo(player);
        }

        static void DrawAmmo(Tank player)
        {
            Color colorOpacity = Color.WHITE;
            colorOpacity.a = (byte)(255 * Utils.Lerp(0.2f, 1, player.attackDelay.PercentComplete));
            for (int x = 0; x < player.ammoCount.TimeRemaining; x++)
            {
                float heightOffset = 1 + (x * 0.5f);
                DrawTextureEx(ammoSprite, new Vector2(Program.ScreenSpace.width, Program.ScreenSpace.height - (ammoSprite.width * heightOffset)), 90, 1, colorOpacity);
            }
        }
    }
}
