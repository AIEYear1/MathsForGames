using Raylib_cs;
using System;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class SpriteObject : SceneObject
    {
        public Texture2D texture = new Texture2D();
        public Color spriteColor = Color.WHITE;

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

            collider = new BoxCollider(Position + new Vector2(img.width / 2, img.height / 2), img.width, img.height, MathF.Atan2(globalTransform.m2, globalTransform.m1));
        }
        public void PreLoad(ref Texture2D _texture)
        {
            texture = _texture;
            collider = new BoxCollider(Position + new Vector2(_texture.width / 2, _texture.height / 2), _texture.width, _texture.height, MathF.Atan2(globalTransform.m2, globalTransform.m1));
        }

        public override void OnUpdate(float deltaTime)
        {
            float rotation = MathF.Atan2(globalTransform.m2, globalTransform.m1);

            collider.Rotate(rotation);

            base.OnUpdate(deltaTime);
        }

        public override void OnDraw()
        {
            float rotation = MathF.Atan2(globalTransform.m2, globalTransform.m1);

            if (Collider.Collision(collider, (BoxCollider)Program.ScreenSpace))
            {
                DrawTextureEx(texture, Position, rotation * (180.0f / MathF.PI), 1, spriteColor);
            }
        }
    }
}
