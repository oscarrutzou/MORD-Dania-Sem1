using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
using Microsoft.Xna.Framework;

namespace MordSem1OOP
{
    public class Wave
    {
        private List<Enemy> enemiesInWave; // List to store all enemies in the wave.
        private int currentWaypointIndex; // Index of the current waypoint for enemy movement.
        private bool isWaveComplete; // Flag to indicate if the wave is complete.

        public List<Enemy> EnemiesInWave => enemiesInWave;
        public bool IsWaveComplete => isWaveComplete;

        public Wave()
        {
            enemiesInWave = new List<Enemy>();
            currentWaypointIndex = 0;
            isWaveComplete = false;
        }

        public void Spawn(EnemyType enemyType, int rate, int count)
        {
            for (int i = 0; i < count; i++)
            {
                enemiesInWave.Add(new Enemy(enemyType)); // Create and add enemies of the specified type to the wave.
            }
        }
    }
}
