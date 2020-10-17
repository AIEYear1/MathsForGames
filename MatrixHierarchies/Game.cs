using System;
using System.Diagnostics;
using static Raylib_cs.Color;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Game
    {
        public static Vector2 CurCenter = Program.Center;
        Tank tank;
        DebugButtonCircle button;
        DebugButtonCircle button1;
        DebugButton button2;
        DebugButton button3;
        DebugButton button4;
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
            button1 = new DebugButtonCircle(Vector2.Right * Program.ScreenSpace.width, 20, tank);
            button2 = new DebugButton(new Raylib_cs.Rectangle(0, Program.ScreenSpace.height, 40, 40), tank);
            button3 = new DebugButton(new Raylib_cs.Rectangle(Program.ScreenSpace.width, Program.ScreenSpace.height, 300, 300), tank);
            button4 = new DebugButton(new Raylib_cs.Rectangle(Program.ScreenSpace.width, Program.Center.y, 300, 40), tank);
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
            button1.Update(deltaTime);
            button2.Update(deltaTime);
            button3.Update(deltaTime);
            button4.Update(deltaTime);
            CurCenter = Program.Center;
            lastTime = currentTime;
        }
        public void Draw()
        {
            BeginDrawing();

            ClearBackground(WHITE);
            DrawText(fps.ToString(), 10, 10, 12, RED);

            button.Draw();
            button3.Draw();
            button4.Draw();
            tank.Draw();
            barrel.Draw();
            button1.Draw();
            button2.Draw();

            EndDrawing();
        }
    }
}
