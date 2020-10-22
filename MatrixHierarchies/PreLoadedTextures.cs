using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    static class PreLoadedTextures
    {
        public static Texture2D playerBulletTexture;
        public static Texture2D enemyBulletTexture;

        public static Texture2D ammoTexture;

        public static void Initialize()
        {
            playerBulletTexture = LoadTexture("bulletBlue_outline.png");
            enemyBulletTexture = LoadTexture("bulletBlue_outline.png");

            ammoTexture = LoadTexture("bulletBlueSilver_outline.png");
        }
    }
}
