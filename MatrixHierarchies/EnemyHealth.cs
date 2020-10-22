using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixHierarchies
{
    class EnemyHealth : SceneObject
    {
        SimpleSpriteObject backBar, healthBar;

        public float Height
        {
            get => backBar.height;
        }

        public EnemyHealth(Vector2 position, float width, float height)
        {
            backBar = new SimpleSpriteObject(new Vector2(-width / 2, height / 2), width, height, Color.RED);
            healthBar = new SimpleSpriteObject(new Vector2(-width / 2, height / 2), width, height, Color.GREEN);

            AddChild(backBar);
            AddChild(healthBar);

            SetPosition(position.x, position.y);
        }

        public void SetHealth(float value)
        {
            healthBar.width = backBar.width * value;
        }
    }
}
