using System;
using System.Collections.Generic;
using System.Text;
using static Raylib_cs.Raylib;
using Raylib_cs;

namespace MatrixHierarchies
{
    static class AmmoManager
    {
        public static List<AmmoPickup> ammoDrops = new List<AmmoPickup>();
        static readonly Bounds bounds = new Bounds(Program.Center, 5000, 500);

        public static void Initialize(Tank tank)
        {
            for (int x = 0; x < 20; x++)
            {
                ammoDrops.Add(new AmmoPickup(bounds.PointInBounds(), tank));
            }
        }

        public static void Update(float deltaTime)
        {
            for (int x = 0; x < ammoDrops.Count; x++)
            {
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
