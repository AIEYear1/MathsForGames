using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    static class PreLoadedTextures
    {
        public static Texture2D BackgroundTexture;

        public static Texture2D PlayerBulletTexture;
        public static Texture2D EnemyBulletTexture;

        public static Texture2D AmmoPickupTexture;
        public static Texture2D AmmoUITexture;

        public static void Initialize()
        {
            BackgroundTexture = LoadTexture(@"Background.png");

            PlayerBulletTexture = LoadTexture("bulletBlue_outline.png");
            EnemyBulletTexture = LoadTexture("bulletBlue_outline.png");

            AmmoPickupTexture = LoadTexture("bulletBlue_outline.png");
            AmmoUITexture = LoadTexture("bulletBlueSilver_outline.png");
        }
    }
}
