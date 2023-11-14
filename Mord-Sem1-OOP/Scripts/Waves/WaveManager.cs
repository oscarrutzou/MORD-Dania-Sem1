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
        private static Waypoint defaultSpawnPoint;


        //static bool b = true;
        public static void Update(GameTime gameTime)
        {
            waves[currentWave].Update(gameTime);

            //Check if the wave is done spawning enemies
            int enemyCount = Global.activeScene.sceneData.enemies.Count;
            if (waves[currentWave].IsDone is true && enemyCount is 0)
                    StartNextWave(); 
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

        /// <summary>
        /// Used to generate every wave in the game.
        /// </summary>
        public static void CreateWaves() 
        {
            Wave wave1 = new Wave();
            wave1.AddPhase(new EnemyBatch(EnemyType.Normal, 5, 0.5f, 2.5f, defaultSpawnPoint));

            Wave wave2 = new Wave();
            wave2.AddPhase(new EnemyBatch(EnemyType.Normal, 2, 0.5f, 0f, defaultSpawnPoint));
            wave2.AddPhase(new EnemyBatch(EnemyType.Strong, 3, 1, 3f, defaultSpawnPoint));

            Wave wave3 = new Wave();
            wave3.AddPhase(new EnemyBatch(EnemyType.Fast, 5, 0.8f, 4f, defaultSpawnPoint));
            wave3.AddPhase(new EnemyBatch(EnemyType.Strong, 1, 0, 0f, defaultSpawnPoint));

            waves = new Wave[]
            {
                wave1,
                wave2,
                wave3,
            };
        }

        public static void SetDefaultSpawnPoint(Waypoint waypoint)
        {
            defaultSpawnPoint = waypoint;
        }
    }
}
