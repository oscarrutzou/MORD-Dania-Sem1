using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Waves
{
    public static class WaveManager
    {
        private static Wave[] waves; //Stores all waves.
        private static int currentWave; //Index of the current wave.
        public static bool AllWavesCleared => (currentWave >= waves.Length);


        static bool b = true;
        public static void Update(GameTime gameTime)
        {
            waves[currentWave].Update(gameTime);

            //Check if the wave is done spawning enemies
            if (waves[currentWave].IsDone is true)
            {
                
                if(Global.activeScene.sceneData.enemies.Count == 0) //Global EnemyCount is 0
                    StartNextWave(); //BUG: This doesn't work because sceneData.enemies does not get updated when an enemy is removed.



                //THIS IS A TEST, REMOVE IT WHEN SCENEDATA IS CAPABLE OF REMOVING ENTRIES FROM ITS ENEMIES LIST!
                bool a = true;
                foreach (GameObject go in Global.activeScene.sceneData.gameObjects) { if (go is Enemy) a = false; };
                if (a && b) { b = false; StartNextWave(); }


     
            }
        }

        public static void StartNextWave()
        {
            if (currentWave < waves.Length - 1)
            {
                currentWave++;
                waves[currentWave].Begin();
            }
        }

        /// <summary>
        /// Starts the wave
        /// </summary>
        /// <param name="wave">Which wave to start</param>
        public static void Begin(int wave) => waves[wave].Begin();

        //Add your waves here
        public static void CreateWaves() 
        {
            Wave wave1 = new Wave();
            wave1.AddPhase(new EnemyBatch(EnemyType.Normal, 5, 0.5f, 2.5f));

            Wave wave2 = new Wave();
            wave2.AddPhase(new EnemyBatch(EnemyType.Normal, 2, 0.5f, 0f));
            wave2.AddPhase(new EnemyBatch(EnemyType.Strong, 3, 1, 3f));


            waves = new Wave[]
            {
                wave1,
                wave2,
            };
        }
    }
}
