using System;
using System.Diagnostics;

namespace MatrixHierarchies
{
    struct Bounds
    {
        public Vector2 position;
        public int maxRadius, minRadius;

        public Bounds(Vector2 pos, int maxRad, int minRad)
        {
            Debug.Assert(minRad < maxRad, "Minumum radius cannot be greater than or equal to Maximum radius");

            position = pos;
            maxRadius = maxRad;
            minRadius = minRad;
        }

        public Vector2 PointInBounds()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            Vector2 toReturn = new Vector2(rand.Next(-100, 101) / 100f, rand.Next(-100, 101) / 100f);

            toReturn *= rand.Next(minRadius, maxRadius);

            return toReturn + position;
        }
    }
}
