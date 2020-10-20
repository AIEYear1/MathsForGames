using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Program
    {
        public static readonly Rectangle ScreenSpace = new Rectangle(0, 0, 640, 480);
        public static readonly Vector2 Center = new Vector2(ScreenSpace.width / 2, ScreenSpace.height / 2);
        static void Main()
        {
            Game game = new Game();

            SetTargetFPS(60);
            InitWindow((int)ScreenSpace.width, (int)ScreenSpace.height, "Tanks for Eveything!");

            game.Init();

            while (!WindowShouldClose())
            {
                game.Update();
                game.Draw();
            }

            game.ShutDown();
            CloseWindow();
        }
    }
}
