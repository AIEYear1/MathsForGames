using Raylib_cs;
using System;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Background : SceneObject
    {
        readonly Vector2 tiling = new Vector2(Program.ScreenSpace.width / 20, Program.ScreenSpace.height / 20);
        Rectangle quad = new Rectangle(-Program.ScreenSpace.width / 2, -Program.ScreenSpace.height / 2, 
                                        Program.ScreenSpace.width * 2, Program.ScreenSpace.height * 2);

        public Background()
        {
            SetPosition(quad.x, quad.y);
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            quad.x = Position.x;
            quad.y = Position.y;

            float minX = MathF.Min(MathF.Abs(quad.x - Program.ScreenSpace.x),
                MathF.Abs((quad.x + quad.width) - (Program.ScreenSpace.x + Program.ScreenSpace.width)));
            float minY = MathF.Min(MathF.Abs(quad.y - Program.ScreenSpace.y),
                MathF.Abs((quad.y + quad.height) - (Program.ScreenSpace.y + Program.ScreenSpace.height)));

            if (minX < Program.ScreenSpace.width / tiling.x || minY < Program.ScreenSpace.height / tiling.y)
            {
                SetPosition(Program.Center.x - (quad.width / 2), Program.Center.y - (quad.height / 2));
            }
        }

        public override void OnDraw()
        {
            DrawTextureQuad(PreLoadedTextures.BackgroundTexture, tiling, Vector2.Zero, quad, Color.WHITE);
        }
    }
}
