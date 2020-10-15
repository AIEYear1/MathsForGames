using Raylib_cs;
using System;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Tank : SceneObject
    {
        SpriteObject tankSprite = new SpriteObject();

        SpriteObject turretSprite = new SpriteObject();
        SceneObject turretObject = new SceneObject();

        float speed = 100;

        public Tank()
        {
            tankSprite.Load("tankBlue_outline.png");
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);
            turretSprite.Load("barrelBlue.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            turretSprite.SetPosition(0, turretSprite.Width / 2.0f);

            turretObject.AddChild(turretSprite);
            AddChild(tankSprite);
            AddChild(turretObject);

            SetPosition(Program.Center.x, Program.Center.y);
        }

        public override void OnUpdate(float deltaTime)
        {
            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                Rotate(-deltaTime);
            }
            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                Rotate(deltaTime);
            }
            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                Vector3 facing = new Vector3(LocalTransform.m1, LocalTransform.m2, 1) * deltaTime * speed;
                Translate(facing.x, facing.y);
            }
            if (IsKeyDown(KeyboardKey.KEY_S))
            {
                Vector3 facing = new Vector3(LocalTransform.m1, LocalTransform.m2, 1) * deltaTime * -speed;
                Translate(facing.x, facing.y);
            }
            if (IsKeyDown(KeyboardKey.KEY_Q))
            {
                turretObject.Rotate(-deltaTime);
            }
            if (IsKeyDown(KeyboardKey.KEY_E))
            {
                turretObject.Rotate(deltaTime);
            }
            //Vector3 face = new Vector3(LocalTransform.m1, LocalTransform.m2, 1) * deltaTime * speed;
            //Translate(face.x, face.y);
            Game.CurCenter.x = globalTransform.m7;
            Game.CurCenter.y = globalTransform.m8;
            base.OnUpdate(deltaTime);
        }
    }
}
