﻿using Microsoft.Xna.Framework;
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
        private EnemyType type;
        private int count;
        private float interval;
        private float duration;
        private Waypoint waypoint;
        private int level;

        private bool active = false; //This is enabled by Send(), allowing the update to perform its logic
        private float timer; //This is used in LocalUpdate() to spawn enemies at the appropriate interval.

        public float Duration { get => duration; }
        #endregion

        public EnemyBatch(EnemyType type, int count, float interval, float duration, Waypoint waypoint) : this(type, count, interval, duration, waypoint, 0) { }

        /// <summary>
        /// Creates a batch/group of enemies to be spawned during a wave.
        /// </summary>
        /// <param name="type">The type of enemy to be spawned from this batch</param>
        /// <param name="count">How many of these enemies to spawn</param>
        /// <param name="interval">The time between each spawn</param>
        public EnemyBatch(EnemyType type, int count, float interval, float duration, Waypoint waypoint, int level)
        {
            this.type = type;
            this.count = count;
            this.interval = interval;
            this.duration = duration;
            this.waypoint = waypoint;
            this.level = level;
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
            Enemy enemy = new Enemy(type, waypoint.Position, waypoint, level);
            GameWorld.Instantiate(enemy);
        }


        /// <summary>
        /// Makes this batch spawn its enemies, with the set type, count and interval.
        /// </summary>
        public void Send()
        {
            active = true;
            WaveManager.batchCount++;
        }

        /// <summary>
        /// Checks if the EnemyBatch has spawned all its enemies.
        /// </summary>
        public bool isDone() => (count <= 0);
    }
}
