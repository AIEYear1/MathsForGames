using Raylib_cs;
using System;
using System.Diagnostics;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    public enum GameState
    {
        START,
        PLAY,
        END
    }
    class Game
    {
        // Current state the game is in
        public static GameState currentState = GameState.START;

        #region Start
        PlayButton playButton;
        #endregion

        #region PLay
        public static Vector2 CurCenter = Program.Center;
        Tank player;
        Stopwatch stopwatch = new Stopwatch();

        long currentTime = 0;
        long lastTime = 0;
        float timer = 0;
        int fps = 1;
        int frames;

        float deltaTime = .005f;
        #endregion

        // Initialize all dependant variables
        public void Initialize()
        {
            // Start Objects
            playButton = new PlayButton(Program.Center - new Vector2(100, -100), 200, 100, Color.BLUE,
                                               new Vector2(10, 20), "PLAY", 70, Color.SKYBLUE);

            // Game Objects
            PreLoadedTextures.Initialize();
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;
            UI.Initialize();
            player = new Tank("tankBlue_outline.png", "barrelBlue.png", -90 * (float)(MathF.PI / 180.0f), Program.Center, 20);
            AmmoManager.Initialize(player);
            EnemyManager.Initialize(player);
        }

        public void ShutDown()
        {
        }

        public void Update()
        {
            switch (currentState)
            {
                case GameState.START:
                    StartUpdate();
                    break;
                case GameState.PLAY:
                    GameUpdate();
                    break;
                case GameState.END:
                    EndUpdate();
                    break;
            }
        }

        void StartUpdate()
        {
            playButton.Update();
        }

        void GameUpdate()
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

            AmmoManager.Update(deltaTime);

            CurCenter = Program.Center;
            lastTime = currentTime;
        }

        void EndUpdate()
        {
        }

        public void Draw()
        {
            switch (currentState)
            {
                case GameState.START:
                    StartDraw();
                    break;
                case GameState.PLAY:
                    GameDraw();
                    break;
                case GameState.END:
                    EndDraw();
                    break;
            }
        }

        void StartDraw()
        {
            DrawText("Insert Tank Pun Here", (int)(Program.ScreenSpace.width / 2) - 390, 110, 70, Color.BLUE);
            playButton.Draw();
        }
        void GameDraw()
        {
            AmmoManager.Draw();
            EnemyManager.Draw();

            player.Draw();

            UI.Draw(player);
        }
        void EndDraw()
        {
            DrawText("Game Over", (int)(Program.ScreenSpace.width / 2) - 190, 110, 70, Color.BLUE);
            DrawText("Enemies defeated " + Tank.enemiesDefeated.ToString("00") + "\t:\tWaves completed " + EnemyManager.waveNum.ToString("00"),
                     (int)(Program.ScreenSpace.width / 2) - 340, (int)(Program.ScreenSpace.width / 2) - 65, 30, Color.BLUE);
        }
    }
}
