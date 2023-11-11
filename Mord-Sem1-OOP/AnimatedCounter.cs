using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MordSem1OOP
{
    internal class AnimatedCounter : GameObject
    {
        public static Texture2D numberPillarSprite; // for storing OOP-Counter-MedTal2.jpg (is in Content-folder)
        private Vector2[] positions; // for 6 positions for numberPillarSprite
        private int startValue = 0;
        private int targetValue; // Target value to animate towards
        private float speed = 50f; // Animation speed
        private Rectangle sourceRectangle;
        private Rectangle targetRectangle;


        /// <summary>
        /// (Constructor) Defines positions for numberPillarSprite * 6
        /// </summary>
        /// <param name="startingPosition"> example: new Vector2(100, 100) </param>

        public AnimatedCounter(Vector2 startingPosition)
        {
            // Define positions for 6 numberPillarSprite's
            positions = new Vector2[6];
            for (int i = 0; i < 6; i++)
            {
                // Parameters for Vector 2 = x position, y position ("0" means it doesn't change)
                positions[i] = startingPosition + new Vector2(i * numberPillarSprite.Width, 0);
            }

            // Limmits the view of numberPillarSprite to one numberBox
            sourceRectangle = new Rectangle(0, 0, numberPillarSprite.Width, 40);
        }

        public override void LoadContent(ContentManager content)
        {
            numberPillarSprite = content.Load<Texture2D>("OOP-Counter-MedTal2");
        }

        public override void Update(GameTime gameTime)
        {
            // Move the first number pillar upward based on the elapsed time and speed
            positions[0] += new Vector2(0, -speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Check if the first number pillar has reached the bottom
            if (positions[0].Y + sourceRectangle.Height < 0)
            {
                // If yes, loop it to the bottom of the screen
                positions[0].Y = GraphicsDeviceManager.DefaultBackBufferHeight;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw each numberPillarSprite
            foreach (var position in positions)
            {
                spriteBatch.Draw(numberPillarSprite, position, sourceRectangle, Color.White);
            }
        }

        /// 
        ///
    }
}
