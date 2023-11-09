using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public class WaveManager
    {
        private List<Wave> waves; // List to store all waves.
        private int currentWaveIndex; // Index of the current wave.
        private TimeSpan waveDuration; // Duration of each wave.
        private TimeSpan waveTimer; // Timer to track the time in the current wave.
        private bool isWaveActive;  // Flag to indicate if a wave is currently active.

        public WaveManager(TimeSpan waveDuration)
        {
            this.waveDuration = waveDuration;
            waves = new List<Wave>();
            currentWaveIndex = 0;
            waveTimer = TimeSpan.Zero;
            isWaveActive = false;
        }

        public bool IsWaveComplete => currentWaveIndex >= waves.Count;

        public void Update(GameTime gameTime, Vector2[] waypoints)
        {
            if (!IsWaveComplete)
            {
                if (!isWaveActive)
                {
                    StartNextWave();
                }

                // Updating waveTimer based on gametid
                waveTimer += gameTime.ElapsedGameTime;

                // Checking if waveDuration is expired
                if (waveTimer >= waveDuration)
                {
                    isWaveActive = false;
                    currentWaveIndex++;
                    waveTimer = TimeSpan.Zero; // reset timer for the next wave.
                }
            }
        }

        public void StartNextWave()
        {
            if (currentWaveIndex < waves.Count)
            {
                isWaveActive = true;
                waveTimer = TimeSpan.Zero;
                // Optionally, you can trigger any events or actions to indicate the start of a new wave here.
            }
        }

        public void AddWave(Wave wave)
        {
            waves.Add(wave);
        }
    }
}
