using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MordSem1OOP.Scripts.Waves
{
    public class Wave
    {
        private List<EnemyBatch> batches;
        private List<float> phaseDurations;
        private float timer;
        private int currentBatch;



        private List<Enemy> enemiesInWave; // List to store all enemies in the wave.
        private bool isWaveComplete; // Flag to indicate if the wave is complete.

        public List<Enemy> EnemiesInWave => enemiesInWave;
        public bool IsWaveComplete => isWaveComplete;


        public Wave()
        {
            batches = new List<EnemyBatch>();

            phaseDurations = new List<float>();
            enemiesInWave = new List<Enemy>();
            isWaveComplete = false;
        }

        public void Update(GameTime gameTime)
        {
            //Stop updating the batches if the wave is done spawning
            if (IsWaveComplete) return; 

            batches[currentBatch].Update(gameTime);



            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Check if the phase has been active longer than the time until the next phase
            if (timer > phaseDurations[currentBatch])
            {
                timer = 0;
                currentBatch++;

                //If there are any more batches to be sent, send it. Otherwise tell the WaveManager that the wave is done.
                if (currentBatch > batches.Count - 1) isWaveComplete = true;
                else batches[currentBatch].Send();
            }
        }

        public void Begin()
        {
            timer = 0;
            currentBatch = 0;
            batches[0].Send();
        }

        //This isn't used anywhere
        public void Spawn(EnemyType enemyType, int rate, int count)
        {
            // Create and add enemies of the specified type to the wave.
            for (int i = 0; i < count; i++)
            {
                SceneData tempSceneData = Global.activeScene.sceneData;

                Enemy enemy1 = new Enemy(EnemyType.Strong, new Vector2(100, 50));
                tempSceneData.gameObjects.Add(enemy1);
                tempSceneData.enemies.Add(enemy1);
            }
        }


        /// <summary>
        /// Used to add a phase to the wave
        /// </summary>
        /// <param name="enemyBatch"></param>
        /// <param name="timeUntilNextPhase"></param>
        public void AddPhase(EnemyBatch enemyBatch, float timeUntilNextPhase)
        {
            batches.Add(enemyBatch);
            phaseDurations.Add(timeUntilNextPhase);
        }


    }
}
