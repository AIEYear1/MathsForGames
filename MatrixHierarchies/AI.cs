using System;
using static Raylib_cs.Raylib;
using Raylib_cs;
namespace MatrixHierarchies
{
    class AI : Tank
    {
        //public List<Bullet> bullets = new List<Bullet>();
        //public BoxCollider collider;

        //public Timer attackDelay = new Timer(1.5f);

        //SpriteObject tankSprite = new SpriteObject();

        //SpriteObject turretSprite = new SpriteObject();
        //SceneObject turretObject = new SceneObject();

        //readonly float speed = 250, rotationSpeed = 90 * (MathF.PI / 180), turretRotSpeed = 30 * (MathF.PI / 180);
        //float curSpeed = 0, curRot = 0, curTurretRot = 0;

        Bounds bounds;
        Tank player;

        Vector2 directionToTravel = Vector2.Zero;
        bool canFire = false;

        public AI(string tankSpriteFileName, string turretSpriteFileName, float rotation, Vector2 position, int maxRange, int idealRange, Tank tank) : base(tankSpriteFileName, turretSpriteFileName, rotation, position)
        {
            player = tank;

            bounds = new Bounds(position, maxRange, idealRange);
        }

        public override void OnUpdate(float deltaTime)
        {
            Move(deltaTime);

            //RotateTurret(deltaTime);

            if(canFire & attackDelay.Check(false))
            {
                float rotation = MathF.Atan2(turretObject.GlobalTransform.m2, turretObject.GlobalTransform.m1);
                rotation = (rotation < 0) ? rotation + (2 * MathF.PI) : rotation;
                Vector2 bulletPos = Position + (new Vector2(turretObject.GlobalTransform.m1, turretObject.GlobalTransform.m2).Normalised() * turretSprite.Height);
                bullets.Add(new Bullet("bulletBlue_outline.png", 600, bulletPos, rotation, this));
                attackDelay.Reset();
            }


            if (parent != null)
                return;
            Vector2 tmpVector = Program.Center - Game.CurCenter;
            Translate(tmpVector.x, tmpVector.y);
        }

        void Move(float deltaTime)
        {
            Vector2 desiredDirectionOfTravel = Vector2.Zero;
            if (Position.Distance(player.Position) > bounds.minRadius)
            {
                desiredDirectionOfTravel = player.Position - Position;
            }
            else if (Position.Distance(player.Position) > bounds.minRadius)
            {
                desiredDirectionOfTravel = Position - player.Position;
            }

            if (desiredDirectionOfTravel.Magnitude() == 0)
                return;

            desiredDirectionOfTravel.Normalize();

            Vector2 forwardDirection = new Vector2(LocalTransform.m1, LocalTransform.m2);
            Vector2 leftDirection = forwardDirection.GetPerpendicular();

            float forwardAngle = MathF.Atan2(desiredDirectionOfTravel.y, desiredDirectionOfTravel.x) - MathF.Atan2(forwardDirection.y, forwardDirection.x);
            float leftAngle = MathF.Atan2(desiredDirectionOfTravel.y, desiredDirectionOfTravel.x) - MathF.Atan2(leftDirection.y, leftDirection.x);

            bool moveForward = MathF.Abs(forwardAngle) < 0.5f * MathF.PI;
            bool rotateLeft = (MathF.Abs(leftAngle) < 0.5f * MathF.PI) == moveForward;
            bool shouldMove = MathF.Abs(forwardAngle) < 30 * (MathF.PI / 180);
            bool shouldRotate = MathF.Abs(forwardAngle) > 0.005f;
            test1 = moveForward;
            test2 = rotateLeft;
            test3 = shouldMove;
            test4 = shouldRotate;

////////////////////////////////////////////////////////////////////////////////////////////// Recode Rotate to lerp and avoid jitter, fix rotation problem ////////////////////////////////////////////
            if (shouldRotate)
            {
                //(angle >= rotateStep) ? rotateStep : angle
                Rotate(rotationSpeed * deltaTime * ((rotateLeft) ? -1 : 1));
                collider.Rotate(rotationSpeed * deltaTime * ((rotateLeft) ? -1 : 1));
            }
            if (shouldMove)
            {
                Vector3 facing = new Vector3(LocalTransform.m1, LocalTransform.m2, 1);
                facing *= deltaTime * speed * ((moveForward) ? 1 : -1);
                Translate(facing.x, facing.y);
            }
        }

        //void RotateTurret(float deltaTime)
        //{
        //    if (Position.Distance(player.Position) > bounds.maxRadius)
        //    {
        //        canFire = false;
        //        return;
        //    }

        //    Vector2 DesiredAngle = player.Position - Position;

        //    float angle = MathF.Atan2(DesiredAngle.y, DesiredAngle.x) - MathF.Atan2(turretObject.GlobalTransform.m2, turretObject.GlobalTransform.m1);
        //    float toRotate = MathF.Max(angle, 0);
        //    toRotate = MathF.Min(angle, 1);

        //    curTurretRot = turretRotSpeed * toRotate;

        //    canFire = MathF.Abs(angle) < 5 * (MathF.PI / 180);

        //    if (curTurretRot != 0)
        //    {
        //        turretObject.Rotate(curTurretRot * deltaTime);
        //    }
        //}
        bool test1;
        bool test2;
        bool test3;
        bool test4;
        public override void OnDraw()
        {
            DrawText(test1.ToString(), 5, (int)Program.ScreenSpace.height - 25, 20, Color.MAROON);
            DrawText(test2.ToString(), 65, (int)Program.ScreenSpace.height - 25, 20, Color.MAROON);
            DrawText(test3.ToString(), 125, (int)Program.ScreenSpace.height - 25, 20, Color.MAROON);
            DrawText(test4.ToString(), 185, (int)Program.ScreenSpace.height - 25, 20, Color.MAROON);
        }
    }
}
