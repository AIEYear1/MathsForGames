using Raylib_cs;
using System;
using System.Diagnostics;
using System.IO;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    public enum GameStage
    {
        START,
        PLAY,
        END,
        TEST
    }
    class Game
    {
        ////////// TODO: Develop excuses to use new Color Lerping /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ColorRGB.Lerp(Color, Color, increment);

        // Current state the game is in
        public static GameStage currentStage = GameStage.START;
        // File name for saving and loading Highscore
        const string SaveName = "Highscore.save";

        #region Start
        PlayButton playButton;
        string prevHighScore;
        #endregion

        #region Play
        public static Vector2 CurCenter = Program.Center;
        public static int fps = 1;

        Tank player;
        Background background = new Background();
        Stopwatch stopwatch = new Stopwatch();

        long currentTime = 0;
        long lastTime = 0;
        float timer = 0;
        int frames;

        float deltaTime = .005f;
        #endregion

        #region End
        InputField HighScoreName;
        #endregion

        // Initialize all dependant variables
        public void Initialize()
        {
            // Start Objects
            playButton = new PlayButton(Program.Center - new Vector2(100, -100), 200, 100, Color.BLUE, 
                new Vector2(10, 20), "PLAY", 70, Color.SKYBLUE, 20);
            LoadHighScore();

            // Game Objects
            PreLoadedTextures.Initialize();
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            player = new Tank(@"Textures\tankBlue_outline.png", @"Textures\barrelBlue.png", -90 * (float)(MathF.PI / 180.0f), Program.Center, 20);

            UI.Initialize();
            PickupManager.Initialize(player);
            EnemyManager.Initialize(player);

            // End Objects
            HighScoreName = new InputField(Program.Center - new Vector2(150, -160), 300, 120, Color.BLUE, 
                new Vector2(30, 30), "NAME", 70, Color.SKYBLUE, 25, 5);
        }
        void LoadHighScore()
        {
            prevHighScore = "                   Highscore";

            if (File.Exists(SaveName))
            {
                string[] saveDat = File.ReadAllText(SaveName).Split('\t');
                prevHighScore += $": {saveDat[0]}\n" +
                                  "Enemies defeated " + saveDat[1] + "\t:\tWaves completed " + saveDat[2];
            }
        }

        public void ShutDown()
        {
            // If no name don't attempt to save the score
            if (!HighScoreName.HasInput)
                return;

            // If there is a highscore
            if (File.Exists(SaveName))
            {
                string[] prevData = File.ReadAllText(SaveName).Split('\t');
                int highTest;

                int.TryParse(prevData[2], out highTest);

                if (highTest > EnemyManager.waveNum)
                    return;

                int.TryParse(prevData[1], out highTest);

                if (highTest >= Tank.enemiesDefeated)
                    return;

                File.Delete(SaveName);
            }

            // If the current score is the best score save it
            File.AppendAllText(SaveName, $"{HighScoreName.OutString}\t{Tank.enemiesDefeated.ToString("000")}\t{EnemyManager.waveNum.ToString("00")}");
        }

        #region Update
        public void Update()
        {
            switch (currentStage)
            {
                case GameStage.TEST:
                    TestUpdate();
                    break;
                case GameStage.START:
                    StartUpdate();
                    break;
                case GameStage.PLAY:
                    GameUpdate();
                    break;
                case GameStage.END:
                    EndUpdate();
                    break;
            }
        }

        void TestUpdate()
        {
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

            background.Update(deltaTime);
            EnemyManager.Update(deltaTime);
            PickupManager.Update(deltaTime);
            SmokeManager.Update(deltaTime);

            CurCenter = Program.Center;
            lastTime = currentTime;
        }

        void EndUpdate()
        {
            HighScoreName.Update();
        }
        #endregion

        #region Draw
        public void Draw()
        {
            BeginDrawing();
            ClearBackground(Color.WHITE);
            switch (currentStage)
            {
                case GameStage.TEST:
                    TestDraw();
                    break;
                case GameStage.START:
                    StartDraw();
                    break;
                case GameStage.PLAY:
                    GameDraw();
                    break;
                case GameStage.END:
                    EndDraw();
                    break;
            }
            EndDrawing();

        }

        void TestDraw()
        {
        }

        void StartDraw()
        {
            DrawText("Insert Tank Pun Here", (int)(Program.ScreenSpace.width / 2) - 390, 110, 70, Color.DARKBLUE);
            DrawText("     WASD to move, Q and E to rotate turret, Space to shoot\n" +
                     "Blue line points to Ammo, Pink line to Medkits, and Red to Enemies",
                     (int)(Program.ScreenSpace.width / 2) - 400, (int)(Program.ScreenSpace.width / 2) - 150, 25, Color.BLUE);
            playButton.Draw();
            DrawText(prevHighScore, (int)Program.Center.x - 350, (int)Program.Center.y + 300, 30, Color.BLUE);
        }
        void GameDraw()
        {
            background.Draw();

            PickupManager.Draw();
            EnemyManager.Draw();
            SmokeManager.Draw();

            player.Draw();

            UI.Draw(player);
        }
        void EndDraw()
        {
            DrawText("Game Over", (int)(Program.ScreenSpace.width / 2) - 190, 110, 70, Color.BLUE);
            DrawText("Enemies defeated " + Tank.enemiesDefeated.ToString("000") + "\t:\tWaves completed " + EnemyManager.waveNum.ToString("00"),
                     (int)(Program.ScreenSpace.width / 2) - 350, (int)(Program.ScreenSpace.width / 2) - 65, 30, Color.BLUE);
            HighScoreName.Draw();
        }
        #endregion
    }
}
