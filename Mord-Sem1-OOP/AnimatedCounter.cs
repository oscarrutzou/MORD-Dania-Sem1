using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MordSem1OOP
{
    internal class AnimatedCounter : GameObject
    {
        public static Texture2D numberPillarSprite; // For storing numberPillarSprite.jpg (is in Content-folder)
        private Rectangle[] rectanglePositions; // For 6 positions for numberPillarSprite
        private int[] yPositions; // For refering each box y-positions as their number inside
        private Rectangle sourceRectangle; // Limits the view to one numberBox
        private int[] currentValue;
        private int[] targetValue; // Target value to animate towards
        private float speed = 300; // Animation speed
        private int rectangleHeight = 40;

        private float elapsedTime = 0f;
        private int loopCount = 0;
        private float delayTime;
        private Random random = new Random();


        /// <summary>
        /// (Constructor) Defines rectanglePositions for numberPillarSprite * 6
        /// </summary>
        /// <param name="startingPosition"> example: new Vector2(100, 100) </param>
        public AnimatedCounter(Vector2 startingPosition)
        {
            // Define rectanglePositions for 6 numberPillarSprite's
            rectanglePositions = new Rectangle[6];
            sourceRectangle = new Rectangle(1, 0, numberPillarSprite.Width, rectangleHeight);
            yPositions = new int[6];
            currentValue = new int[6];
            targetValue = new int[] { 0, 0, 4, 5, 6, 7 };

            for (int i = 0; i < 6; i++)
            {
                // Parameters for Vector 2 = x position, y position ("0" means it doesn't change)
                rectanglePositions[i] = new Rectangle(numberPillarSprite.Width * i, (int)startingPosition.Y, numberPillarSprite.Width, numberPillarSprite.Height / 9);

                // Limits the view of numberPillarSprite to one numberBox
                
            }
        }

        private int GetIndexPosition(int index)
        {
            return index * rectangleHeight;
        }

        public override void LoadContent(ContentManager content)
        {
            numberPillarSprite = content.Load<Texture2D>("numberPillarSprite");
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Animation(gameTime);
            //delayTime = random.Next(3, 8);
            //bool loop = true;
            //elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (elapsedTime >= delayTime)
            //{
            //    loop = false;
            //    elapsedTime = 0f;
            //    delayTime = (float)random.NextDouble() * (8-3) + 3;
            //}

            
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < rectanglePositions.Length; i++)
            {
                sourceRectangle.Y = yPositions[i];
                spriteBatch.Draw(numberPillarSprite, rectanglePositions[i], sourceRectangle, Color.White);
            }
        }

        public void Animation(GameTime gameTime)
        {
            for (int i = 0; i < rectanglePositions.Length; i++)
            {
                // Move the first number pillar upward based on the elapsed time and speed

                //if (currentValue[i] == targetValue[i])
                //    continue;

                if (loopCount >= 10 && currentValue[i] == targetValue[i])
                {
                    continue;
                }

                yPositions[i] += (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);
                sourceRectangle.Y = (int)yPositions[0];
                currentValue[i] = (int)(yPositions[i] / rectangleHeight);

                // Check if the first number pillar has reached the bottom
                if (yPositions[i] > numberPillarSprite.Height - 40)
                {
                    // If yes, loop it to the bottom of the screen
                    yPositions[i] = GraphicsDeviceManager.DefaultBackBufferHeight;
                    loopCount++;
                    yPositions[i] = 0;
                }
            }
        }

        private void HandleInput(GameTime gameTime)
        { 
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Space))
            {
                Animation(gameTime);
            }
        }

        /// 
        ///
    }
}
