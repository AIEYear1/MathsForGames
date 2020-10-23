using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    public enum GameState
    {
        Start,
        Play,
        End
    }
    class Program
    {
        public static readonly Rectangle ScreenSpace = new Rectangle(0, 0, 1000, 800);
        public static readonly Vector2 Center = new Vector2(ScreenSpace.width / 2, ScreenSpace.height / 2);

        public static GameState state = GameState.Start;

        static void Main()
        {
            PlayButton playButton = new PlayButton(Program.Center - new Vector2(100, -100), 200, 100, Color.BLUE,
                                                   new Vector2(10, 20), "PLAY", 70, Color.SKYBLUE);

            Game game = new Game();

            SetTargetFPS(60);
            InitWindow((int)ScreenSpace.width, (int)ScreenSpace.height, "Tanks for Eveything!");

            game.Init();

            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.WHITE);

                switch (state)
                {
                    case GameState.Start:
                        DrawText("Insert Tank Pun Here", (int)(Program.ScreenSpace.width / 2) - 390, 110, 70, Color.BLUE);
                        playButton.Update();
                        playButton.Draw();
                        break;
                    case GameState.Play:
                        game.Update();
                        game.Draw();
                        break;
                    case GameState.End:
                        DrawText("Game Over", (int)(Program.ScreenSpace.width / 2) - 200, 110, 70, Color.BLUE);
                        DrawText("Enemies defeated " + Tank.enemiesDefeated.ToString("00") + "\t:\tWaves completed " + EnemyManager.wave.ToString("00"),
                                 (int)(Program.ScreenSpace.width / 2) - 250, (int)(Program.ScreenSpace.width / 2) - 65, 30, Color.BLUE);
                        break;
                }

                EndDrawing();
            }

            game.ShutDown();
            CloseWindow();
        }
    }
}
