using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class UI
    {
        public static PlayerHealth playerHealth;
        static Radar radar;
        public static void Initialize()
        {
            playerHealth = new PlayerHealth(Vector2.Up * (Program.ScreenSpace.height - 25), 300, 40);
            radar = new Radar(new Vector2(Program.ScreenSpace.width - 200, Program.ScreenSpace.height - 200), 200, 200);
        }

        public static void Draw(Tank player)
        {
            DrawText(fps.ToString(), 10, 10, 12, Color.RED);
            DrawAmmo(player);
            playerHealth.Draw();
            DrawText("Enemies " + EnemyManager.currentNumberOfEnemies.ToString("00") + " : Wave " + EnemyManager.waveNum.ToString("00"),
                     (int)(Program.ScreenSpace.width / 2) - 190, 0, 30, Color.DARKGRAY);
            radar.Draw();
        }

        static void DrawAmmo(Tank player)
        {
            Color colorOpacity = Color.WHITE;
            colorOpacity.a = (byte)(255 * Utils.Lerp(0.2f, 1, player.attackDelay.PercentComplete));
            for (int x = 0; x < player.ammoCount.TimeRemaining; x++)
            {
                float heightOffset = 1 + (x * 0.5f);
                DrawTextureEx(PreLoadedTextures.ammoPickupTexture, new Vector2(Program.ScreenSpace.width,
                    Program.ScreenSpace.height - 200 - (PreLoadedTextures.ammoPickupTexture.width * heightOffset)), 90, 1, colorOpacity);
            }
        }
    }
}
