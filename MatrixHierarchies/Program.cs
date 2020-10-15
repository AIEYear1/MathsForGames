using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Program
    {
        public static readonly Vector2 Center = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2);
        static void Main()
        {
            Game game = new Game();

            SetTargetFPS(60);
            InitWindow(640, 480, "Tanks for Eveything!");

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
