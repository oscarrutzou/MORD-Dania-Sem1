using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MordSem1OOP
{
    internal class AnimatedCounter : GameObject
    {
        public static Texture2D numberPillar { get; private set; } // for storing OOP-Counter-MedTal2.jpg (is in Content-folder)
        private Vector2[] positions; // for 6 positions for numberPillar
        private int startValue = 0;
        private int targetValue; // Target value to animate towards
        private float transitionSpeed; // Speed between numbers

        /// <summary>
        /// (Constructor) Defines positions for numberPillar * 6
        /// </summary>
        /// <param name="numberPillar"> store OOP-Counter-MedTal2.jpg </param>
        /// <param name="startingPosition"> example: new Vector2(100, 100) </param>
        public AnimatedCounter(Texture2D numberPillar, Vector2 startingPosition)
        {
            // Define positions for 6 numberPillar's
            positions = new Vector2[6];
            for (int i = 0; i < 6; i++)
            {
                // Parameters for Vector 2 = x position, y position ("0" means it doesn't change)
                positions[i] = startingPosition + new Vector2(i * numberPillar.Width, 0);
            }
        }

        /// <summary>
        /// Constructor uden parametre
        /// </summary>
        public AnimatedCounter()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            numberPillar = content.Load<Texture2D>("OOP-Counter-MedTal2");
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw each numberPillar
            foreach (var position in positions)
            {
                spriteBatch.Draw(numberPillar, position, Color.White);
            }
        }

        /// 
        ///
    }
}
