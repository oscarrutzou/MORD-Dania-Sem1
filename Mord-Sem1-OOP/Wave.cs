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
        private List<Enemy> enemiesInWave = new List<Enemy>();
        private int currentWaypointIndex = 0;
        private bool isWaveComplete = false;

        public List<Enemy> EnemiesInWave { get { return enemiesInWave; } }

        public bool IsWaveComplete { get {  return isWaveComplete; } }

        public Wave()
        {
            InitializeEnemies();
        }

        private void InitializeEnemies()
        {
            //enemiesInWave.Add(new Enemy(EnemyType.Normal));
            //enemiesInWave.Add(new Enemy(EnemyType.Fast));
            //enemiesInWave.Add(new Enemy(EnemyType.Strong));

        }

        public void Update(GameTime gameTime, Microsoft.Xna.Framework.Vector2[] waypoints)
        {
            if (currentWaypointIndex < waypoints.Length)
            {
                foreach (Enemy enemy in enemiesInWave)
                {
                    enemy.Update(gameTime);
                    enemy.Position = waypoints[currentWaypointIndex];
                }

                if (enemiesInWave.All(enemy => enemy.Position == waypoints[currentWaypointIndex]))
                {
                    currentWaypointIndex++;
                }
            }
            else
            {
                isWaveComplete = true;
            }
        }
    }
}
