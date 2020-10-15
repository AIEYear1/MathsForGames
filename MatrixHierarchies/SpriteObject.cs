using Raylib_cs;
using System;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class SpriteObject : SceneObject
    {
        Texture2D texture = new Texture2D();

        public float Width
        {
            get => texture.width;
        }
        public float Height
        {
            get => texture.height;
        }

        public SpriteObject()
        {

        }

        public void Load(string filename)
        {
            Image img = LoadImage(filename);
            texture = LoadTextureFromImage(img);
        }

        public override void OnDraw()
        {
            float rotation = MathF.Atan2(globalTransform.m2, globalTransform.m1);

            DrawTextureEx(texture, new Vector2(globalTransform.m7, globalTransform.m8), rotation * (180.0f / MathF.PI), 1, Color.WHITE);
        }
    }
}
