using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MordSem1OOP.Scripts.Waves
{
    public class Wave
    {
        #region Fields and Properties

        private List<EnemyBatch> enemyBatches = new List<EnemyBatch>();
        private int currentBatch;
        private float timer;
        private bool isDone = false;
        private bool isActive = false;
        public bool IsDone => isDone;
        public bool IsActive => isActive;

        #endregion


        public void Update(GameTime gameTime)
        {
            if (IsActive is not true) return;

            //All batches in this wave that aren't done spawning their enemies, will be updated.
            foreach (EnemyBatch batch in enemyBatches)
                if(batch.isDone() is false)
                    batch.Update(gameTime);

            //Check if the phase has been active longer than the time until the next phase
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentBatch < enemyBatches.Count && timer > enemyBatches[currentBatch].Duration)
            {
                timer = 0;

                enemyBatches[currentBatch].Send();
                currentBatch++;
            }

            if (enemyBatches[enemyBatches.Count - 1].isDone())
            {
                isDone = true;
                isActive = false;
            }
        }

        /// <summary>
        /// Starts this wave
        /// </summary>
        public void Begin()
        {
            timer = 0;
            currentBatch = 0;
            enemyBatches[0].Send();
            isActive = true;
        }

        /// <summary>
        /// Used to add a phase to the wave
        /// </summary>
        /// <param name="enemyBatch"></param>
        /// <param name="timeUntilNextPhase"></param>
        public void AddPhase(EnemyBatch enemyBatch) => enemyBatches.Add(enemyBatch);
    }
}
