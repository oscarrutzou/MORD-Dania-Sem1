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
        public static Wave[] waves; //Stores all waves.
        public static int currentWave; //Index of the current wave.
        public static int batchCount;
        public static bool AllWavesCleared => (currentWave >= waves.Length);
        private static Waypoint defaultSpawnPoint;


        //static bool b = true;
        public static void Update(GameTime gameTime)
        {
            foreach (Wave wave in waves)
            {
                wave.Update(gameTime);
            }

            //Check if the wave is done spawning enemies
            //int enemyCount = Global.activeScene.sceneData.enemies.Count;
            //if (waves[currentWave].IsDone is true && enemyCount is 0)
            //        StartNextWave(); 
        }


        public static void StartNextWave()
        {
            if (currentWave < waves.Length)
            {
                Begin(currentWave);
                currentWave++;
                return;
            }
        }

        public static string DebugWaveCount()
        {
            return currentWave.ToString();
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

        public static void CreateLargeWaves()
        {
            List<Wave> list = new List<Wave>();

            for (int level = 0; level < 100; level++)
            {
                Wave wave = new Wave();
                list.Add(wave);

                int NormCount = 15;
                float NormSpawnRate = .6f;
                float duration = NormSpawnRate * NormCount;
                float delay = 0;

                wave.AddPhase(new EnemyBatch(EnemyType.Normal, NormCount + level * 5, NormSpawnRate, duration + 3, defaultSpawnPoint, level));

                NormCount = 10;
                duration = NormSpawnRate * NormCount;
                wave.AddPhase(new EnemyBatch(EnemyType.Normal, NormCount + level * 5, NormSpawnRate, duration + 2, defaultSpawnPoint, level));
                wave.AddPhase(new EnemyBatch(EnemyType.Normal, NormCount + level * 3, NormSpawnRate, duration + 2, defaultSpawnPoint, level + 1));

                wave = new Wave();
                list.Add(wave);

                int StrongCount = 6;
                float StrongSpawnRate = 2f;
                wave.AddPhase(new EnemyBatch(EnemyType.Strong, StrongCount + level * 3, StrongSpawnRate, duration + delay, defaultSpawnPoint, level));
                duration = StrongCount * StrongSpawnRate;

                NormCount = 15;
                wave.AddPhase(new EnemyBatch(EnemyType.Normal, NormCount + level * 5, NormSpawnRate, duration / 2, defaultSpawnPoint, level));
                duration = NormSpawnRate * NormCount;

                wave = new Wave();
                list.Add(wave);

                StrongCount = 9;
                StrongSpawnRate = 2f;
                wave.AddPhase(new EnemyBatch(EnemyType.Strong, StrongCount + level * 3, StrongSpawnRate, duration + delay * 2, defaultSpawnPoint, level));
                duration = StrongCount * StrongSpawnRate;

                int FastCount = 15;
                float FastSpawnRate = 1f;
                wave.AddPhase(new EnemyBatch(EnemyType.Fast, FastCount + level * 7, FastSpawnRate, duration + delay, defaultSpawnPoint, level));
                duration = FastCount * FastSpawnRate;

                wave = new Wave();
                list.Add(wave);

                NormCount = 25;
                wave.AddPhase(new EnemyBatch(EnemyType.Normal, NormCount + level * 5, NormSpawnRate, duration / 2, defaultSpawnPoint, level));
                duration = NormSpawnRate * NormCount;

                FastCount = 25;
                FastSpawnRate = 1f;
                wave.AddPhase(new EnemyBatch(EnemyType.Fast, FastCount + level * 7, FastSpawnRate, duration / 2, defaultSpawnPoint, level));
                duration = FastCount * FastSpawnRate;

                StrongCount = 15;
                StrongSpawnRate = 2f;
                wave.AddPhase(new EnemyBatch(EnemyType.Strong, StrongCount + level * 3, StrongSpawnRate, duration + delay, defaultSpawnPoint, level));
                duration = StrongCount * StrongSpawnRate;

            }

            waves = list.ToArray();
        }

        public static void SetDefaultSpawnPoint(Waypoint waypoint)
        {
            defaultSpawnPoint = waypoint;
        }
    }
}
