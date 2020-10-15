﻿using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Program
    {
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
