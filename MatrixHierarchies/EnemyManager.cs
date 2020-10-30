using System;
using System.Collections.Generic;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{

    static class EnemyManager
    {
        // Spawn enums
        public enum SpawnStage
        {
            WAITFORWAVEEND,
            PREPAREWAVE,
            SPAWNENEMIES
        }

        // Enemies currently in the scene
        public static List<AI> curEnemies = new List<AI>();
        static readonly Bounds spawnBounds = new Bounds(Program.Center, 10000, 1000);

        // Not 100% certain this is necessary but better to keep it in
        public static bool Paused = true;

        public static SpawnStage curStage = SpawnStage.WAITFORWAVEEND;

        public static int waveNum, totalEnemiesForWave, currentNumberOfEnemies;

        // For Radar
        public static Vector2 nearestEnemy = Program.Center;

        static List<SubWave> Wave = new List<SubWave>();

        // used For pause mechanic
        static float pauseTime = 0;
        static bool gotTimeAtPause = false;

        // "Grace" period at the start of each wave
        static Timer waitTimer;

        // Player to attack
        static Tank player;

        /// <summary>
        /// Initializes the enemy manager
        /// </summary>
        /// <param name="tank">Thing for the AI to attack</param>
        public static void Initialize(Tank tank)
        {
            waitTimer = new Timer(10);
            player = tank;
        }

        /// <summary>
        /// Manages all the Ai and their spawning
        /// </summary>
        public static void Update(float deltaTime)
        {
            // Pause stuff for timing
            if (Paused && !gotTimeAtPause)
            {
                pauseTime = (float)GetTime();
                gotTimeAtPause = true;
            }
            if (!Paused && gotTimeAtPause)
            {
                pauseTime = (float)GetTime() - pauseTime;
                gotTimeAtPause = false;

                if (waveNum > 0)
                {
                    foreach (SubWave sw in Wave)
                        sw.timestamp += pauseTime;
                }
            }

            if (Paused)
                return;

            // Spawning
            if (curStage == SpawnStage.WAITFORWAVEEND)
                Wait();
            else if (curStage == SpawnStage.PREPAREWAVE)
                StartSpawn();
            else if (curStage == SpawnStage.SPAWNENEMIES)
                Play();

            // Update enemies
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

                for (int y = 0; y < curEnemies.Count; y++)
                {
                    if (x != y)
                        curEnemies[y].Push(curEnemies[x]);
                }
            }
        }

        /// <summary>
        /// Draws the enmies
        /// </summary>
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
                curStage = SpawnStage.PREPAREWAVE;
        }

        /// <summary>
        /// Prepare the next wave for spawning
        /// </summary>
        static void StartSpawn()
        {
            waveNum++;
            totalEnemiesForWave = currentNumberOfEnemies;

            // Adding enemies to subwaves
            Wave.Add(new SubWave(waveNum, 0f, 0.5f, 6 + (int)(2.0f * (waveNum - 1))));
            if (waveNum > 6)
                for (int i = 0; i < waveNum / 6; i++)
                    Wave.Add(new SubWave(waveNum, (i * 3) + 2, 0.75f, 1 + (int)(waveNum / 3)));
            if (waveNum > 12)
                for (int i = 0; i < (waveNum / 6) - 1; i++)
                    Wave.Add(new SubWave(waveNum, (i * 3) + 3, 1f, 1 + (int)(waveNum / 3)));
            if (waveNum > 24)
                for (int i = 0; i < (waveNum / 6) - 2; i++)
                    Wave.Add(new SubWave(waveNum, (i * 3) + 4, 1f, 1 + (int)(waveNum / 3)));

            // Preping for end of wave
            foreach (SubWave sw in Wave)
            {
                sw.ResetTimestamp(); //We need this so that the timestamps are correct
                totalEnemiesForWave += sw.amount;
            }

            currentNumberOfEnemies = totalEnemiesForWave;
            curStage = SpawnStage.SPAWNENEMIES;
        }

        /// <summary>
        /// Spawn the enemies
        /// </summary>
        static void Play()
        {
            // Spawns the enmy when it is time
            for (int i = 0; i < Wave.Count; i++)
            {
                if (Wave[i].IsTime())
                {
                    if (Wave[i].Spawn())
                    {
                        curEnemies.Add(new AI(-90 * (float)(MathF.PI / 180.0f), spawnBounds.PointInBounds(), 3, 1000, 300, player));
                    }
                }
                if (Wave[i].IsDone()) // Once all enemies in the subwave have spawned
                    Wave.Remove(Wave[i]);
            }

            // Once all waves have spawned all their enemies
            if (Wave.Count == 0)
            {
                Console.WriteLine("spawning finished"); //Signal the end of the wave spawning
                curStage = SpawnStage.WAITFORWAVEEND;
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
