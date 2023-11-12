using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MordSem1OOP
{
    internal class AnimatedCounter : GameObject
    {
        public static Texture2D numberPillarSprite; // For storing numberPillarSprite.jpg (is in Content-folder)
        private Rectangle[] rectanglePositions; // For 6 positions for numberPillarSprite
        private int[] yPositions; // For refering each box y-positions as their number inside
        private Rectangle sourceRectangle; // Limits the view to one numberBox
        private int startValue = 0;
        private int targetValue; // Target value to animate towards
        private float speed = 250; // Animation speed
        private int rectangleHeight = 40;


        /// <summary>
        /// (Constructor) Defines rectanglePositions for numberPillarSprite * 6
        /// </summary>
        /// <param name="startingPosition"> example: new Vector2(100, 100) </param>
        public AnimatedCounter(Vector2 startingPosition)
        {
            // Define rectanglePositions for 6 numberPillarSprite's
            rectanglePositions = new Rectangle[6];
            sourceRectangle = new Rectangle();
            yPositions = new int[6];


            for (int i = 0; i < 6; i++)
            {
                // Parameters for Vector 2 = x position, y position ("0" means it doesn't change)
                rectanglePositions[i] = new Rectangle(numberPillarSprite.Width * i, (int)startingPosition.Y, numberPillarSprite.Width, numberPillarSprite.Height / 9);

                // Limits the view of numberPillarSprite to one numberBox
                sourceRectangle = new Rectangle(0, 0, numberPillarSprite.Width, rectangleHeight);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            numberPillarSprite = content.Load<Texture2D>("numberPillarSprite");
        }


        public override void Update(GameTime gameTime)
        {
            // Move the first number pillar upward based on the elapsed time and speed
            yPositions[0] += (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);
            sourceRectangle.Y = (int)yPositions[0];
            // Check if the first number pillar has reached the bottom
            if (yPositions[0] > numberPillarSprite.Height)
            {
                // If yes, loop it to the bottom of the screen
                yPositions[0] = GraphicsDeviceManager.DefaultBackBufferHeight;
                yPositions[0] = 40;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw each numberPillarSprite
            foreach (var rectangle in rectanglePositions)
            {
                spriteBatch.Draw(numberPillarSprite, rectangle, sourceRectangle, Color.White);
            }
        }

        /// 
        ///
    }
}
