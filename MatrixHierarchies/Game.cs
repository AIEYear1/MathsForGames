using System;
using System.Diagnostics;
using static Raylib_cs.Color;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Game
    {
        public static Vector2 CurCenter = Program.Center;

        Tank player;
        SpriteObject barrel = new SpriteObject();

        Stopwatch stopwatch = new Stopwatch();

        long currentTime = 0;
        long lastTime = 0;
        float timer = 0;
        int fps = 1;
        int frames;

        float deltaTime = .005f;

        public void Init()
        {
            PreLoadedTextures.Initialize();
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;
            UI.Initialize();
            GenerateObjects();
        }
        void GenerateObjects()
        {
            player = new Tank("tankBlue_outline.png", "barrelBlue.png", -90 * (float)(MathF.PI / 180.0f), Program.Center, 20);
            barrel.Load("barrelGreen_up.png");
            barrel.SetPosition(Program.Center.x + 160, Program.Center.y - 150);
            AmmoManager.Initialize(player);

            EnemyManager.Initialize(player);
        }

        public void ShutDown()
        {
        }

        public void Update()
        {
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;

            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            player.Update(deltaTime);

            EnemyManager.Update(deltaTime);

            barrel.Update(deltaTime);
            AmmoManager.Update(deltaTime);

            CurCenter = Program.Center;
            lastTime = currentTime;
        }
        public void Draw()
        {
            BeginDrawing();

            ClearBackground(WHITE);
            DrawText(fps.ToString(), 10, 10, 12, RED);

            AmmoManager.Draw();

            EnemyManager.Draw();

            player.Draw();
            barrel.Draw();

            UI.Draw(player);

            EndDrawing();
        }
    }
}
