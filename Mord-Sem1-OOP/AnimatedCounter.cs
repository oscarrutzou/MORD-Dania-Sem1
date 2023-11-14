using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MordSem1OOP
{
    internal class AnimatedCounter : GameObject
    {
        #region fields
        public static Texture2D numberPillarSprite; // For storing numberPillarSprite.jpg (is in Content-folder)
        private Rectangle[] rectangleNumberPillarPositions; // For 6 positions for numberPillarSprite
        private int[] yPositions; // For refering each box y-positions seperately - and animating numberPillarSprite
        private Rectangle sourceRectangle; // Limits the view to one numberBox
        private int[] currentValue;
        private int[] targetValue; // Target value to animate towards
        private float speed = 600; // Animation speed
        private int rectangleHeight = 40;

        private int[] loopCount = new int[6];
        private static Random random = new Random();

        private static int resultNumber = 003456;
        private static string numberString = resultNumber.ToString();

        // Determine how many leading zeros are needed
        private int leadingZeros = Math.Max(0, 6 - numberString.Length);

        private int setLoopAmount = random.Next(3, 6); // For animation
        #endregion

        /// <summary>
        /// (Constructor) Defines rectangleNumberPillarPositions for numberPillarSprite * 6
        /// </summary>
        /// <param name="startingPosition"> example: new Vector2(100, 100) </param>
        public AnimatedCounter(Vector2 startingPosition)
        {
            // Define rectangleNumberPillarPositions for 6 numberPillarSprite's
            rectangleNumberPillarPositions = new Rectangle[6];
            sourceRectangle = new Rectangle(1, 0, numberPillarSprite.Width, rectangleHeight);
            yPositions = new int[6];
            currentValue = new int[6];
            targetValue = new int[6];

            TranslateResultNumberToArray();

            for (int i = 0; i < 6; i++)
            {
                rectangleNumberPillarPositions[i] = new Rectangle(numberPillarSprite.Width * i, (int)startingPosition.Y, numberPillarSprite.Width, numberPillarSprite.Height / 9);
            }
        }
        public void Animation(GameTime gameTime)
        {
            // loops from 0 to the number of numberPillars (their rectangle) 
            for (int i = 0; i < rectangleNumberPillarPositions.Length; i++)
            {
                // skips rest of loop if loopCount has reached setLoopAmount
                if (loopCount[i] >= setLoopAmount)
                {continue; }

                int newYPosition = yPositions[i] + (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);
                int min = targetValue[i] * rectangleHeight;
                int max = (targetValue[i] + 1) * rectangleHeight;

                // If yPositions is less than min or above max
                if (!InRange(yPositions[i], min, max))
                {
                    // Handle wrap-around if newYPosition exceeds the sprite height
                    if (newYPosition > numberPillarSprite.Height - rectangleHeight)
                    {
                        newYPosition = newYPosition - numberPillarSprite.Height + rectangleHeight;
                    }

                    if (InRange(newYPosition, min, max))
                    {
                        if (loopCount[i] == setLoopAmount - 1)
                        {
                            // = targetValue[i] * 40 (rectangleHeight)
                            newYPosition = GetIndexPosition(targetValue[i]);
                        }
                        loopCount[i]++;
                    }
                }

                // Animates each numberPillar - updates for each "speed * gameTime.ElapsedGameTime.TotalSeconds" (frame?)
                yPositions[i] = newYPosition;
                sourceRectangle.Y = (int)yPositions[i];
                currentValue[i] = (int)(yPositions[i] / rectangleHeight);

                // Check if the first number pillar has reached the bottom
                if (yPositions[i] > numberPillarSprite.Height)
                {
                    // If yes, loop it to the bottom of the screen

                    yPositions[i] -= numberPillarSprite.Height;
                }
            }
        }
        #region other
        private int GetIndexPosition(int index)
        {
            return index * rectangleHeight;
        }

        /// <summary>
        /// Returns true if value is between or equals to min and max
        /// </summary>
        /// <param name="value">check if this int is between or equal to min and max</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public bool InRange(int value, int min, int max)
        {
            return min <= value && value <= max;
        }

        private void HandleInput(GameTime gameTime)
        { 
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Space))
            {
                currentValue = new int[6];
                yPositions = new int[6];
                loopCount = new int[6];
                //Animation(gameTime);
            }
        }

        public void TranslateResultNumberToArray()
        {
            // Assigns the 0's to the left of the number (if leadingZeroes is more than 0 (meaning: resultNumber has at least one "0"))
            for (int i = 0; i < leadingZeros; i++)
            {
                targetValue[i] = 0;
            }
            // Assigns the remaining digits to the array
            for (int i = leadingZeros; i < targetValue.Length; i++)
            {
                targetValue[i] = int.Parse(numberString[i - leadingZeros].ToString());
            }
        }
        #endregion
        #region standard stuff

        public override void LoadContent(ContentManager content)
        {
            numberPillarSprite = content.Load<Texture2D>("numberPillarSprite");
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Animation(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < rectangleNumberPillarPositions.Length; i++)
            {
                sourceRectangle.Y = yPositions[i];
                spriteBatch.Draw(numberPillarSprite, rectangleNumberPillarPositions[i], sourceRectangle, Color.White);
            }
        }

        #endregion
    }
}
