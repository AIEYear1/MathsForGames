using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Program
    {
        // Rectangle that represents what can be seen in screen
        public static readonly Rectangle ScreenSpace = new Rectangle(0, 0, 1000, 800);
        // Center of the screen
        public static readonly Vector2 Center = new Vector2(ScreenSpace.width / 2, ScreenSpace.height / 2);

        // Main program
        static void Main()
        {
            // Game Manager
            Game game = new Game();

            // Initialize everything
            SetTargetFPS(60);
            InitWindow((int)ScreenSpace.width, (int)ScreenSpace.height, "Tanks for Eveything!");

            game.Initialize();

            // Game Loop
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
