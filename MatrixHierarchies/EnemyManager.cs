﻿using System;
using System.Collections.Generic;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    public enum SpawnStage
    {
        Wait,
        Start,
        Play
    }

    static class EnemyManager
    {
        public static List<AI> curEnemies = new List<AI>();
        static readonly Bounds spawnBounds = new Bounds(Program.Center, 10000, 1000);

        public static bool Paused = true;
        public static List<float> nextEnemies = new List<float>();

        public static SpawnStage curStage = SpawnStage.Wait;

        public static int wave, totalEnemiesForWave, currentNumberOfEnemies;

        public static Vector2 nearestEnemy = Program.Center;

        static List<SubWave> Wave = new List<SubWave>();

        static float pauseTime = 0;
        static bool gotTimeAtPause = false;
        static Timer waitTimer;

        static Tank player;

        public static void Initialize(Tank tank)
        {
            waitTimer = new Timer(10);
            player = tank;
        }

        public static void Update(float deltaTime)
        {
            if (Paused && !gotTimeAtPause)
            {
                pauseTime = (float)GetTime();
                gotTimeAtPause = true;
            }

            if (!Paused && gotTimeAtPause)
            {
                pauseTime = (float)GetTime() - pauseTime;
                gotTimeAtPause = false;

                if (wave > 0)
                {
                    foreach (SubWave sw in Wave)
                        sw.timestamp += pauseTime;
                }
            }

            if (Paused)
                return;

            if (curStage == SpawnStage.Wait)
                Wait();
            else if (curStage == SpawnStage.Start)
                StartSpawn();
            else if (curStage == SpawnStage.Play)
                Play();

            float distFromEnemy = float.MaxValue;
            for (int x = 0; x < curEnemies.Count; x++)
            {
                float tmpDist = Program.Center.Distance(curEnemies[x].Position);
                if (tmpDist < distFromEnemy)
                {
                    distFromEnemy = tmpDist;
                    nearestEnemy = curEnemies[x].Position;
                }

                curEnemies[x].Update(deltaTime);
            }
        }

        public static void Draw()
        {
            for (int x = 0; x < curEnemies.Count; x++)
            {
                curEnemies[x].Draw();
            }
        }

        /// <summary>
        /// Wait for the next wave
        /// </summary>
        static void Wait()
        {
            if (waitTimer.Check() && currentNumberOfEnemies <= totalEnemiesForWave / 2)
                curStage = SpawnStage.Start;
        }

        /// <summary>
        /// Prepare the next wave for spawning
        /// </summary>
        static void StartSpawn()
        {
            wave++;
            totalEnemiesForWave = currentNumberOfEnemies;

            Wave.Add(new SubWave(wave, 0f, 0.5f, 6 + (int)(2.0f * (wave - 1))));
            if (wave > 6)
                for (int i = 0; i < wave / 6; i++)
                    Wave.Add(new SubWave(wave, (i * 3) + 2, 0.75f, 1 + (int)(wave / 3)));
            if (wave > 12)
                for (int i = 0; i < (wave / 6) - 1; i++)
                    Wave.Add(new SubWave(wave, (i * 3) + 3, 1f, 1 + (int)(wave / 3)));
            if (wave > 24)
                for (int i = 0; i < (wave / 6) - 2; i++)
                    Wave.Add(new SubWave(wave, (i * 3) + 3, 1f, 1 + (int)(wave / 3)));

            foreach (SubWave sw in Wave)
            {
                sw.ResetTimestamp(); //We need this so that the timestamps are correct
                totalEnemiesForWave += sw.amount;
                nextEnemies.AddRange(sw.GetEnemies());
            }

            currentNumberOfEnemies = totalEnemiesForWave;
            curStage = SpawnStage.Play;
        }

        /// <summary>
        /// Spawn the enemies
        /// </summary>
        static void Play()
        {
            for (int i = 0; i < Wave.Count; i++)
            {
                if (Wave[i].IsTime())
                {
                    if (Wave[i].Spawn())
                    {
                        curEnemies.Add(new AI("tankRed_outline.png", "barrelRed.png", -90 * (float)(MathF.PI / 180.0f), spawnBounds.PointInBounds(), 3, 1000, 300, player));
                        nextEnemies.RemoveAt(0); //Lambdas yay
                    }
                }
                if (Wave[i].IsDone())
                    Wave.Remove(Wave[i]);
            }

            if (Wave.Count == 0)
            {
                Console.WriteLine("wave finished"); //Signal the end of the wave
                curStage = SpawnStage.Wait;

                nextEnemies.Clear();
            }
        }

        public static void RemoveEnemy(AI enemy)
        {
            curEnemies.Remove(enemy);
            currentNumberOfEnemies--;
            Tank.enemiesDefeated++;
        }
    }
}
