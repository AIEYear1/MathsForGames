﻿namespace MatrixHierarchies
{
    class CircleCollider : Collider
    {
        float radius = 0;

        public float Radius
        {
            get => radius;
        }


        public CircleCollider(Vector2 position, float radius) : base(position)
        {
            this.radius = radius;
        }

        public void SetPosition(Vector2 pos)
        {
            position = pos;
        }
    }
}
