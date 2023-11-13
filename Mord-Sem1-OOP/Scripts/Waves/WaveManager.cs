using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Waves
{
    public class WaveManager
    {
        private Wave[] waves; // List to store all waves.
        private int currentWaveIndex; // Index of the current wave.
        private TimeSpan waveDuration; // Duration of each wave.
        private TimeSpan waveTimer; // Timer to track the time in the current wave.
        private bool isWaveActive;  // Flag to indicate if a wave is currently active.

        public WaveManager()
        {
            CreateWaves();
            waves[0].Begin();
        }

        public bool IsWaveComplete => currentWaveIndex >= waves.Length;


        bool xaaaaa = false;
        public void Update(GameTime gameTime)
        {
            waves[currentWaveIndex].Update(gameTime);

            //Check if the wave is done spawning enemies
            if (waves[currentWaveIndex].IsWaveComplete == true)
            {
                //Check if there are still enemies left in the scene.
                if(Global.activeScene.sceneData.enemies.Count == 0)
                    StartNextWave();



                //THIS IS A TEST!
                int a = 0;
                foreach (Enemy e in Global.activeScene.sceneData.enemies)
                {
                    if (e.IsRemoved == true) a++;
                }

                if (a == 5 && xaaaaa == false)
                {
                    xaaaaa = true;
                    StartNextWave();
                }
                //REMOVE THIS ONCE SCENEDATA'S ENEMYLIST GETS UPDATED WHEN AN ENEMY DIES
            }

        }

        public void StartNextWave()
        {
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                waves[currentWaveIndex].Begin();
            }
        }


        //Add your waves here
        private void CreateWaves() 
        {
            Wave wave1 = new Wave();
            wave1.AddPhase(new EnemyBatch(EnemyType.Normal, 5, 0.5f), 2.5f);

            Wave wave2 = new Wave();
            wave2.AddPhase(new EnemyBatch(EnemyType.Normal, 2, 0.5f), 1f); //Bug, duration cannot be lower than interval, it skips to the next phase
            wave2.AddPhase(new EnemyBatch(EnemyType.Strong, 3, 1), 3);


            waves = new Wave[]
            {
                wave1,
                wave2,
            
            
            };
        }
    }
}
