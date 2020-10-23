namespace MatrixHierarchies
{
    class HealthPickup : SceneObject
    {
        Tank player;
        SpriteObject healthSprite = new SpriteObject();

        public HealthPickup(Vector2 position, Tank tank)
        {
            player = tank;

            healthSprite.PreLoad(ref PreLoadedTextures.HealthPickupTexture);
            healthSprite.SetPosition(-healthSprite.Width * 1.5f, -healthSprite.Height / 2);

            AddChild(healthSprite);

            SetPosition(position.x, position.y);
            collider = new BoxCollider(position, healthSprite.Width * 2, healthSprite.Height, 0);
        }

        public override void OnUpdate(float deltaTime)
        {
            if (Collider.Collision(collider, player.collider))
            {
                player.health.CountByValue(-5);
                UI.playerHealth.SetHealth(player.health.TimeRemaining / player.health.delay);
                PickupManager.DestroyHealth(this);

            }
            base.OnUpdate(deltaTime);
            collider.SetPosition(Position);
        }
    }
}
