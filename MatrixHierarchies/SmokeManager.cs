using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MatrixHierarchies
{
    static class SmokeManager
    {
        static List<SmokeEffect> smokes = new List<SmokeEffect>();

        public static void Update(float deltaTime)
        {
            for(int x = 0; x < smokes.Count; x++)
            {
                smokes[x].Update(deltaTime);
            }
        }
        public static void Draw()
        {
            for (int x = 0; x < smokes.Count; x++)
            {
                smokes[x].Draw();
            }
        }

        public static void CreateSmoke(Vector2 position)
        {
            smokes.Add(new SmokeEffect(position, 3));
        }
        public static void RemoveSmoke(SmokeEffect smoke)
        {
            smokes.Remove(smoke);
        }
    }
}
