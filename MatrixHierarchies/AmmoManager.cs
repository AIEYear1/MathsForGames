using System.Collections.Generic;

namespace MatrixHierarchies
{
    static class AmmoManager
    {
        public static List<AmmoPickup> ammoDrops = new List<AmmoPickup>();
        static readonly Bounds bounds = new Bounds(Program.Center, 5000, 500);

        public static Vector2 nearestAmmoDrop = Program.Center;

        public static void Initialize(Tank tank)
        {
            for (int x = 0; x < 20; x++)
            {
                ammoDrops.Add(new AmmoPickup(bounds.PointInBounds(), tank));
            }
        }

        public static void Update(float deltaTime)
        {
            float distFromAmmo = float.MaxValue;
            for (int x = 0; x < ammoDrops.Count; x++)
            {
                float tmpDist = Program.Center.Distance(ammoDrops[x].Position);
                if (tmpDist < distFromAmmo)
                {
                    distFromAmmo = tmpDist;
                    nearestAmmoDrop = ammoDrops[x].Position;
                }

                ammoDrops[x].Update(deltaTime);
            }
        }

        public static void Draw()
        {
            for (int x = 0; x < ammoDrops.Count; x++)
            {
                ammoDrops[x].Draw();
            }
        }

        public static void DestroyAmmo(AmmoPickup pickup)
        {
            pickup.Position = bounds.PointInBounds();
        }
    }
}
