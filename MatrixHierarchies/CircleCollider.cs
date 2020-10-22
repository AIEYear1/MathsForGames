namespace MatrixHierarchies
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

        public override void SetPosition(Vector2 pos)
        {
            position = pos;
        }

        public override void Debug()
        {
            throw new System.NotImplementedException();
        }
        public override void Rotate(float radians)
        {
            throw new System.NotImplementedException();
        }
    }
}
