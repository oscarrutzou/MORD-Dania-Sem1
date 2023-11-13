using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Waves
{
    public class EnemyBatch
    {

        SceneData data; //This is used to load the enemies into the list of GameObjects, as well as access the elapsed time between each update.
        private EnemyType type;
        private int count;
        private float interval;

        private bool canSend = false; //This is enabled by Send(), allowing the update to perform its logic
        private float timer; //This is used in LocalUpdate() to spawn enemies at the appropriate interval.


        /// <summary>
        /// Creates a batch/group of enemies to be spawned during a wave.
        /// </summary>
        /// <param name="type">The type of enemy to be spawned from this batch</param>
        /// <param name="count">How many of these enemies to spawn</param>
        /// <param name="interval">The time between each spawn</param>
        public EnemyBatch(EnemyType type, int count, float interval)
        {
            data = Global.activeScene.sceneData;

            this.type = type;
            this.count = count;
            this.interval = interval;
        }

        /// <summary>
        /// Runs until the count of enemies has been exhausted.
        /// This method is run by a separate thread, so it doesn't halt the program while waiting.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (!canSend || isDone()) return;



            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > interval)
            {
                //Spawning the enemy
                Enemy enemy = new Enemy(type, new Vector2(100, 150));
                data.gameObjectsToAdd.Add(enemy);
                
                count--;
                timer = 0;
            }
        }




        /// <summary>
        /// Makes this batch spawn its enemies, with the set type, count and interval.
        /// </summary>
        public void Send()
        {
            canSend = true;
        }

        /// <summary>
        /// Checks if the EnemyBatch has spawned all its enemies.
        /// </summary>
        public bool isDone()
        {
            if (count <= 0) return true;
            return false;
        }


    }
}
