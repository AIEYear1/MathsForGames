﻿using Raylib_cs;
using System;
using System.Collections.Generic;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Tank : SceneObject
    {
        public static uint enemiesDefeated = 0;

        public List<Bullet> bullets = new List<Bullet>();

        public Timer ammoCount = new Timer(20);
        public Timer attackDelay = new Timer(1.5f);

        public Timer health;

        protected SpriteObject tankSprite = new SpriteObject();

        protected SpriteObject turretSprite = new SpriteObject();
        protected SceneObject turretObject = new SceneObject();

        protected readonly float speed = 350, rotationSpeed = 120 * (MathF.PI / 180), turretRotSpeed = 80 * (MathF.PI / 180);
        protected float curSpeed = 0, curRot = 0, curTurretRot = 0;

        public Tank(string tankSpriteFileName, string turretSpriteFileName, float rotation, Vector2 position, float hp)
        {
            tankSprite.Load(tankSpriteFileName);
            tankSprite.SetRotate(rotation);
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);
            turretSprite.Load(turretSpriteFileName);
            turretSprite.SetRotate(rotation);
            turretSprite.SetPosition(0, turretSprite.Width / 2.0f);

            turretObject.AddChild(turretSprite);
            AddChild(tankSprite);
            AddChild(turretObject);

            SetPosition(position.x, position.y);
            collider = new BoxCollider(position, tankSprite.Width, tankSprite.Height, rotation);

            attackDelay.Reset(attackDelay.delay);
            health = new Timer(hp);
        }

        public override void OnUpdate(float deltaTime)
        {
            RotateBody(deltaTime);

            MoveBody(deltaTime);

            RotateTurret(deltaTime);
            Game.CurCenter = Position;

            // Fire bullets
            if (IsKeyPressed(KeyboardKey.KEY_SPACE) & attackDelay.Check(false) && !ammoCount.IsComplete(false))
            {
                float rotation = MathF.Atan2(turretObject.GlobalTransform.m2, turretObject.GlobalTransform.m1);
                rotation = (rotation < 0) ? rotation + (2 * MathF.PI) : rotation;
                Vector2 bulletPos = Position + (new Vector2(turretObject.GlobalTransform.m1, turretObject.GlobalTransform.m2).Normalised() * turretSprite.Height);
                bullets.Add(new Bullet(ref PreLoadedTextures.enemyBulletTexture, 600, bulletPos, rotation, this));
                ammoCount.CountByValue(1);
                attackDelay.Reset();
            }

            for (int x = 0; x < bullets.Count; x++)
            {
                bullets[x].Update(deltaTime);

                if (x >= bullets.Count)
                    continue;

                // Collision deection for bullets against enemies
                AI tmpEnemy = null;
                float dist = float.MaxValue;
                for (int y = 0; y < EnemyManager.curEnemies.Count; y++)
                {
                    float tmpDist = bullets[x].Position.Distance(EnemyManager.curEnemies[y].Position);
                    if (tmpDist < dist)
                    {
                        dist = tmpDist;
                        tmpEnemy = EnemyManager.curEnemies[y];
                    }
                }

                if (tmpEnemy != null)
                {
                    bullets[x].CheckCollision(tmpEnemy);
                }
            }

            base.OnUpdate(deltaTime);
        }

        public override void OnDraw()
        {
            for (int x = 0; x < bullets.Count; x++)
            {
                bullets[x].Draw();
            }
        }

        public virtual void TakeDamage()
        {
            health.CountByValue(1);
            if (health.IsComplete(false))
            {
                Game.currentState = GameState.END;
                EnemyManager.Paused = true;
            }
            UI.playerHealth.SetHealth(health.TimeRemaining / health.delay);
        }

        void MoveBody(float deltaTime)
        {
            curSpeed = speed * Utils.GetAxis("Vertical", 5);
            if (curSpeed != 0)
            {
                Vector3 facing = new Vector3(LocalTransform.m1, LocalTransform.m2, 1);
                facing *= deltaTime * curSpeed;
                Translate(facing.x, facing.y);
            }
        }
        void RotateBody(float deltaTime)
        {
            curRot = rotationSpeed * Utils.GetAxis("Horizontal", 3);

            if (curRot != 0)
            {
                Rotate(curRot * deltaTime);
                collider.Rotate(curRot * deltaTime);
            }
        }

        void RotateTurret(float deltaTime)
        {
            curTurretRot = turretRotSpeed * Utils.GetAxis("Turret", 9f);

            if (curTurretRot != 0)
            {
                turretObject.Rotate(curTurretRot * deltaTime);
            }
        }
    }
}
