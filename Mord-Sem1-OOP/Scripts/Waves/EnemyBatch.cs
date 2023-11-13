using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Waves
{
    public class EnemyBatch
    {
        #region Fields and Properties
        SceneData data; //This is used to load the enemies into the list of GameObjects.
        private EnemyType type;
        private int count;
        private float interval;
        private float duration;

        private bool active = false; //This is enabled by Send(), allowing the update to perform its logic
        private float timer; //This is used in LocalUpdate() to spawn enemies at the appropriate interval.

        public float Duration { get => duration; }
        #endregion

        /// <summary>
        /// Creates a batch/group of enemies to be spawned during a wave.
        /// </summary>
        /// <param name="type">The type of enemy to be spawned from this batch</param>
        /// <param name="count">How many of these enemies to spawn</param>
        /// <param name="interval">The time between each spawn</param>
        public EnemyBatch(EnemyType type, int count, float interval, float duration)
        {
            data = Global.activeScene.sceneData;

            this.type = type;
            this.count = count;
            this.interval = interval;
            this.duration = duration;
        }

        /// <summary>
        /// Runs until the count of enemies has been exhausted.
        /// This method is run by a separate thread, so it doesn't halt the program while waiting.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (!active) return;


            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > interval)
            {
                SpawnEnemy();
                count--;
                timer = 0;
            }
        }

        private void SpawnEnemy()
        {
            Enemy enemy = new Enemy(type, new Vector2(100, 150));
            data.gameObjectsToAdd.Add(enemy);
        }


        /// <summary>
        /// Makes this batch spawn its enemies, with the set type, count and interval.
        /// </summary>
        public void Send() => active = true;

        /// <summary>
        /// Checks if the EnemyBatch has spawned all its enemies.
        /// </summary>
        public bool isDone() => (count <= 0);
    }
}
