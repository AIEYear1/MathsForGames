using System.Collections.Generic;
using static Raylib_cs.Raylib;

namespace MatrixHierarchies
{
    class SubWave
    {
        public int wave;

        private bool isStarted = false;

        public int amount;

        private float time;
        private float spacing;
        public float timestamp;

        public SubWave(int wave, float time, float spacing, int amount)
        {
            this.wave = wave;
            this.time = time;
            this.spacing = spacing;
            this.amount = amount;

            this.timestamp = (float)GetTime();
        }

        /// <summary>
        /// Spawn the next enemy
        /// </summary>
        /// <returns>Returns true when its time to spawn another enemy</returns>
        public bool Spawn()
        {
            if (amount == 0) return false;
            if (timestamp + (spacing) < (float)GetTime())
            {
                timestamp = (float)GetTime();
                amount--;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Tell when it's time to spawn the next Subwave
        /// </summary>
        /// <returns>Returns true when next Subwave is ready to start spawning</returns>
        public bool IsTime()
        {
            if (isStarted) 
                return true;

            if (timestamp + (time) < (float)GetTime())
            {
                isStarted = true;
                timestamp = (float)GetTime() - spacing; //Minus spacing so they spawn right away instead of with a delay
                return true;
            }
            else 
                return false;
        }

        /// <summary>
        /// Tell when Subwave is done spawning
        /// </summary>
        /// <returns>Returns true when there are no more enemies to spawn in the Subwave</returns>
        public bool IsDone()
        {
            if (amount == 0) return true;
            else return false;
        }

        /// <summary>
        /// sets the times to spawn the enemies in the Subwave
        /// </summary>
        /// <returns>Returns a list of EnemTime to determine spawn time</returns>
        public List<float> GetEnemies()
        {
            List<float> g = new List<float>();
            for (int i = 0; i < amount; i++)
                g.Add(time + (i * spacing));

            return g;
        }

        /// <summary>
        /// resets the enemy timestamp
        /// </summary>
        public void ResetTimestamp()
        {
            timestamp = (float)GetTime();
        }
    }
}
