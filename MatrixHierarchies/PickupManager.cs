using System.Collections.Generic;

namespace MatrixHierarchies
{
    static class PickupManager
    {
        // Active pickup lists
        public static List<AmmoPickup> ammoDrops = new List<AmmoPickup>();
        public static List<HealthPickup> healthDrops = new List<HealthPickup>();

        static readonly Bounds bounds = new Bounds(Program.Center, 5000, 500);

        // For Radar
        public static Vector2 nearestAmmoDrop = Program.Center, nearestHealthDrop = Program.Center;

        /// <summary>
        /// Initializes all the pickups
        /// </summary>
        /// <param name="tank">thing that can interact with pickups</param>
        public static void Initialize(Tank tank)
        {
            for (int x = 0; x < 20; x++)
            {
                ammoDrops.Add(new AmmoPickup(bounds.PointInBounds(), tank));
                if (x % 2 == 1)
                    continue;
                healthDrops.Add(new HealthPickup(bounds.PointInBounds(), tank));
            }
        }

        /// <summary>
        /// Updates all of the pickups
        /// </summary>
        public static void Update(float deltaTime)
        {
            float distFromAmmo = float.MaxValue;
            float distFromHealth = float.MaxValue;
            for (int x = 0; x < ammoDrops.Count; x++)
            {
                // Determine which ammo pickup is the closest
                float tmpAmmoDist = Program.Center.Distance(ammoDrops[x].Position);
                if (tmpAmmoDist < distFromAmmo)
                {
                    distFromAmmo = tmpAmmoDist;
                    nearestAmmoDrop = ammoDrops[x].Position;
                }

                ammoDrops[x].Update(deltaTime);

                // Use if instead of for to increase efficiency
                if (x < healthDrops.Count)
                {
                    // Determine which health pickup is the closest
                    float tmpHealthDist = Program.Center.Distance(healthDrops[x].Position);
                    if (tmpHealthDist < distFromHealth)
                    {
                        distFromHealth = tmpHealthDist;
                        nearestHealthDrop = healthDrops[x].Position;
                    }

                    healthDrops[x].Update(deltaTime);
                }
            }
        }

        /// <summary>
        /// Draw all of the pickups
        /// </summary>
        public static void Draw()
        {
            for (int x = 0; x < ammoDrops.Count; x++)
            {
                ammoDrops[x].Draw();

                if (x < healthDrops.Count)
                {
                    healthDrops[x].Draw();
                }
            }
        }

        public static void DestroyAmmo(AmmoPickup pickup)
        {
            pickup.Position = bounds.PointInBounds();
        }
        public static void DestroyHealth(HealthPickup pickup)
        {
            pickup.Position = bounds.PointInBounds();
        }
    }
}
