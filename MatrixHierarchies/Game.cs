using System;
using System.Diagnostics;
using Raylib_cs;
using static Raylib_cs.Color;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Game
    {
        public static Vector2 CurCenter = Program.Center;
        Tank tank;
        DebugButtonCircle button;
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
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;
            GenerateObjects();
        }
        void GenerateObjects()
        {
            tank = new Tank("tankBlue_outline.png", "barrelBlue.png", -90 * (float)(MathF.PI / 180.0f), Program.Center);
            barrel.Load("barrelGreen_up.png");
            barrel.SetPosition(Program.Center.x + 160, Program.Center.y - 150);
            button = new DebugButtonCircle(Vector2.Zero, 300, tank);
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

            tank.Update(deltaTime);
            barrel.Update(deltaTime);
            button.Update(deltaTime);
            CurCenter = Program.Center;
            lastTime = currentTime;
        }
        public void Draw()
        {
            BeginDrawing();

            ClearBackground(WHITE);
            DrawText(fps.ToString(), 10, 10, 12, RED);

            button.Draw();
            tank.Draw();
            barrel.Draw();

            EndDrawing();
        }
    }
}
