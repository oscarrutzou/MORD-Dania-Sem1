using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;

namespace MordSem1OOP
{
    internal class AnimatedCounter
    {
        #region fields
        public static Texture2D numberPillarSprite; // For storing numberPillarSprite.jpg (is in Content-folder)
        private NumberPillar[] numberPillars; // Stores each numberPillar as objects for defining setLoopAmount individually (order of animation from lowest setLoopAmount to highest)
        private Rectangle[] rectangleNumberPillarPositions; // For 6 positions for numberPillarSprite
        private int[] yPositions; // For refering each box y-positions seperately - and animating numberPillarSprite
        private Rectangle sourceRectangle; // Limits the view to one numberBox
        private int sourceRectangleHeight = 40;
        private int[] currentValue; // Why do you need currentValue? - used for
        private int[] targetValue; // Target value to animate towards
        private float speed = 600; // Animation speed

        private int[] loopCount = new int[6];
        public static Random random = new Random();
        public static int setLoopAmount = random.Next(3, 6); // For animation
        public static int[] setLoopAmountsForEachNumberPillar;

        private static int resultNumber = 9453;
        private static string numberString = resultNumber.ToString();
        private int leadingZeros = Math.Max(0, 6 - numberString.Length); // Determine how many leading zeros are needed
        #endregion

        /// <summary>
        /// (Constructor) Defines rectangleNumberPillarPositions for numberPillarSprite * 6
        /// </summary>
        /// <param name="startingPosition"> example: new Vector2(100, 100) </param>
        public AnimatedCounter(Vector2 startingPosition)
        {
            // Define rectangleNumberPillarPositions for 6 numberPillarSprite's
            numberPillarSprite = GlobalTextures.Textures[TextureNames.NumberPillarSprite];
            rectangleNumberPillarPositions = new Rectangle[6];
            sourceRectangle = new Rectangle(1, 0, numberPillarSprite.Width, sourceRectangleHeight);
            yPositions = new int[6];
            currentValue = new int[6];
            targetValue = new int[6];
            numberPillars = new NumberPillar[6];
            setLoopAmountsForEachNumberPillar = new int[6];

            TranslateResultNumberToArray();
            SetLoopAmountForEachNumberPillar();

            for (int i = 0; i < 6; i++)
            {
                // Why divide by 10? - The higher the number, the less height of the presentation (get's resized). Coordinates x (first sprite's width times i(0) = 0) and y + (resizable) width, height
                rectangleNumberPillarPositions[i] = new Rectangle(numberPillarSprite.Width * i + (int)startingPosition.X, (int)startingPosition.Y, numberPillarSprite.Width, numberPillarSprite.Height / 10);
            } 

        }
        public void Animation(GameTime gameTime)
        {
            // loops from 0 to the number of numberPillars (their rectangle) 
            for (int i = 0; i < rectangleNumberPillarPositions.Length; i++)
            {
                // TODO: change to skip each one by one instead of entire. (skips rest of loop if loopCount has reached setLoopAmount)  
                if (loopCount[i] >= setLoopAmountsForEachNumberPillar[i])
                {continue; }

                int newYPosition = yPositions[i] + (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);
                int min = targetValue[i] * sourceRectangleHeight;
                int max = (targetValue[i] + 1) * sourceRectangleHeight;

                // If yPositions is less than min or above max
                if (!InRange(yPositions[i], min, max))
                {
                    // Handle wrap-around if newYPosition exceeds the sprite height
                    if (newYPosition > numberPillarSprite.Height - sourceRectangleHeight)
                    {
                        newYPosition = newYPosition - numberPillarSprite.Height + sourceRectangleHeight;
                    }

                    if (InRange(newYPosition, min, max))
                    {
                        if (loopCount[i] == setLoopAmountsForEachNumberPillar[i] - 1)
                        {
                            // = targetValue[i] * 40 (sourceRectangleHeight)
                            newYPosition = GetIndexPosition(targetValue[i]);
                        }
                        loopCount[i]++;
                    }
                }

                // Animates each numberPillar - updates for each "speed * gameTime.ElapsedGameTime.TotalSeconds" (frame?)
                yPositions[i] = newYPosition;
                sourceRectangle.Y = (int)yPositions[i];
                currentValue[i] = (int)(yPositions[i] / sourceRectangleHeight);

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
            return index * sourceRectangleHeight;
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
        public void SetLoopAmountForEachNumberPillar()
        {
            int loopAmount = random.Next(0, 7);
            int addedLoopAmount = 3;
            // Assigns the 0's to the left of the number (if leadingZeroes is more than 0 (meaning: resultNumber has at least one "0"))
            for (int i = 0; i < leadingZeros; i++)
            {
                setLoopAmountsForEachNumberPillar[i] = loopAmount;
                loopAmount = random.Next(loopAmount + 1, loopAmount + addedLoopAmount);
            }   
            // Assigns the remaining digits to the array
            for (int i = targetValue.Length - 1; i >= leadingZeros; i--)
            {
                setLoopAmountsForEachNumberPillar[i] = loopAmount;
                loopAmount = random.Next(loopAmount + 1, loopAmount + addedLoopAmount);
            }
        }
        #endregion
        #region standard stuff

        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Animation(gameTime);
        }

        public void Draw()
        {
            Draw(Vector2.Zero);
        }

        public void Draw(Vector2 position)
        {
            SpriteBatch spriteBatch = GameWorld._spriteBatch;

            for (int i = 0; i < rectangleNumberPillarPositions.Length; i++)
            {
                Vector2 countPosition = rectangleNumberPillarPositions[i].Location.ToVector2() + position;
                sourceRectangle.Y = yPositions[i];
                spriteBatch.Draw(numberPillarSprite, countPosition, sourceRectangle, Color.White);
            }
        }

        #endregion
    }
}
