using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    static class PreLoadedTextures
    {
        public static Texture2D BackgroundTexture;

        public static Texture2D PlayerBulletTexture;
        public static Texture2D EnemyBulletTexture;

        public static Texture2D AmmoUITexture;

        public static Texture2D AmmoPickupTexture;
        public static Texture2D HealthPickupTexture;

        public static Texture2D EnemyTankTexture;
        public static Texture2D EnemyTurretTexture;

        public static void Initialize()
        {
            BackgroundTexture = LoadTexture(@"Textures\Background.png");

            PlayerBulletTexture = LoadTexture(@"Textures\bulletBlue_outline.png");
            EnemyBulletTexture = LoadTexture(@"Textures\bulletBlue_outline.png");

            AmmoUITexture = LoadTexture(@"Textures\bulletBlue_outline.png");

            AmmoPickupTexture = LoadTexture(@"Textures\bulletBlueSilver_outline.png");
            HealthPickupTexture = LoadTexture(@"Textures\MedPak2_Pixel.png");

            EnemyTankTexture = LoadTexture(@"Textures\tankRed_outline.png");
            EnemyTurretTexture = LoadTexture(@"Textures\barrelRed.png");
        }
    }
}
