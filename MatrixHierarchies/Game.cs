using System.Diagnostics;
using static Raylib_cs.Color;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class Game
    {
        Tank tank;

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
            tank = new Tank();
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

            lastTime = currentTime;
        }
        public void Draw()
        {
            BeginDrawing();

            ClearBackground(WHITE);
            DrawText(fps.ToString(), 10, 10, 12, RED);

            tank.Draw();

            EndDrawing();
        }
    }
}
