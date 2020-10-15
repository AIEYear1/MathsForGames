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

        readonly float speed = 250, rotationSpeed = 90 * (MathF.PI / 180), turretRotSpeed = 120 * (MathF.PI / 180);
        readonly float movementTick = 5, rotationTick = 2 * (MathF.PI / 180);
        float curSpeed = 0, curRot = 0, curTurretRot = 0;

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
            RotateBody(deltaTime);

            MoveBody(deltaTime);

            RotateTurret(deltaTime);

            Game.CurCenter.x = Position.x;
            Game.CurCenter.y = Position.y;
            base.OnUpdate(deltaTime);
        }

        void MoveBody(float deltaTime)
        {
            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                curSpeed = MathF.Min(curSpeed + movementTick, speed);
            }
            else if (IsKeyDown(KeyboardKey.KEY_S))
            {
                curSpeed = MathF.Max(curSpeed - movementTick, -speed);
            }
            else
            {
                if (MathF.Abs(curSpeed) < movementTick)
                {
                    curSpeed = 0;
                }
                else if (curSpeed < 0)
                {
                    curSpeed += movementTick;
                }
                else if (curSpeed > 0)
                {
                    curSpeed -= movementTick;
                }
            }

            if (curSpeed != 0)
            {
                Vector3 facing = new Vector3(LocalTransform.m1, LocalTransform.m2, 1);
                facing *= deltaTime * curSpeed;
                Translate(facing.x, facing.y);
            }
        }
        void RotateBody(float deltaTime)
        {
            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                curRot = MathF.Max(curRot - rotationTick, -rotationSpeed);
            }
            else if (IsKeyDown(KeyboardKey.KEY_D))
            {
                curRot = MathF.Min(curRot + rotationTick, rotationSpeed);
            }
            else
            {
                if (MathF.Abs(curRot) < rotationTick)
                {
                    curRot = 0;
                }
                else if (curRot < 0)
                {
                    curRot += rotationTick;
                }
                else if (curRot > 0)
                {
                    curRot -= rotationTick;
                }
            }

            if (curRot != 0)
            {
                Rotate(curRot * deltaTime);
            }
        }

        void RotateTurret(float deltaTime)
        {
            if (IsKeyDown(KeyboardKey.KEY_Q))
            {
                curTurretRot = MathF.Max(curTurretRot - rotationTick, -turretRotSpeed);
            }
            else if (IsKeyDown(KeyboardKey.KEY_E))
            {
                curTurretRot = MathF.Min(curTurretRot + rotationTick, turretRotSpeed);
            }
            else
            {
                if (MathF.Abs(curTurretRot) < rotationTick)
                {
                    curTurretRot = 0;
                }
                else if (curTurretRot < 0)
                {
                    curTurretRot += rotationTick;
                }
                else if (curTurretRot > 0)
                {
                    curTurretRot -= rotationTick;
                }
            }

            if (curTurretRot != 0)
            {
                turretObject.Rotate(curTurretRot * deltaTime);
            }
        }
    }
}
