using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixHierarchies
{
    class AmmoPickup : SceneObject
    {
        Tank player;
        SpriteObject ammo1 = new SpriteObject();
        SpriteObject ammo2 = new SpriteObject();
        SpriteObject ammo3 = new SpriteObject();

        public AmmoPickup(Vector2 position, Tank tank)
        {
            player = tank;

            ammo1.PreLoad(ref PreLoadedTextures.ammoTexture);
            ammo2.PreLoad(ref PreLoadedTextures.ammoTexture);
            ammo3.PreLoad(ref PreLoadedTextures.ammoTexture);

            ammo1.SetPosition(-ammo1.Width * 1.5f, -ammo1.Height / 2);
            ammo2.SetPosition(-ammo2.Width / 2, -ammo2.Height / 2);
            ammo3.SetPosition(ammo2.Width / 2, -ammo3.Height / 2);

            AddChild(ammo1);
            AddChild(ammo2);
            AddChild(ammo3);

            SetPosition(position.x, position.y);
            collider = new BoxCollider(position, ammo1.Width * 2, ammo1.Height, 0);
        }

        public override void OnUpdate(float deltaTime)
        {
            if(Collider.Collision(collider, player.collider))
            {
                player.ammoCount.Reset();
                AmmoManager.DestroyAmmo(this);

            }
            base.OnUpdate(deltaTime);
            collider.SetPosition(Position);
        }

        //public override void OnDraw()
        //{
        //    collider.Debug();
        //}
    }
}
