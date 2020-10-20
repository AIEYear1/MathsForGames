using System;

namespace MatrixHierarchies
{
    class Bullet : SceneObject
    {
        Tank source;
        BoxCollider collider;
        SpriteObject bulletSprite = new SpriteObject();
        float speed = 0;
        bool destroyed = false;
        Timer lifeTime = new Timer(1);

        public Bullet(string bulletSpriteFileName, float speed, Vector2 position, float rotation, Tank tank)
        {
            source = tank;
            bulletSprite.Load(bulletSpriteFileName);
            this.speed = speed;

            bulletSprite.SetRotate(90 * (float)(MathF.PI / 180.0f));
            bulletSprite.SetPosition(bulletSprite.Height / 2, -bulletSprite.Width / 2.0f);

            AddChild(bulletSprite);

            SetRotate(rotation);
            SetPosition(position.x, position.y);
            collider = new BoxCollider(position, bulletSprite.Width, bulletSprite.Height, rotation);
        }

        public override void OnUpdate(float deltaTime)
        {
            if (destroyed)
                return;

            if (lifeTime.Check())
            {
                Destroy();
                return;
            }

            Vector3 facing = new Vector3(LocalTransform.m1, LocalTransform.m2, 1);
            facing *= deltaTime * speed;
            Translate(facing.x, facing.y);

            // collision against enemies checks here

            base.OnUpdate(deltaTime);
        }

        //public override void OnDraw()
        //{
        //    DrawCircleV(Position, (bulletSprite.Width / 2) + (bulletSprite.Height / 2), Color.MAROON);
        //    DrawCircleV(bulletSprite.Position, 5, Color.MAGENTA);
        //}

        public void Destroy()
        {
            source.bullets.Remove(this);
            children.Remove(bulletSprite);
            destroyed = true;
        }
    }
}
