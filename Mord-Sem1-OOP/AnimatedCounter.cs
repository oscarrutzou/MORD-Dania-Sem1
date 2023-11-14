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
        private float speed = 200; // Animation speed
        private int rectangleHeight = 40;

        private float elapsedTime = 0f;
        private int[] loopCount = new int[6];
        private float delayTime;
        private Random random = new Random();

        private static int resultNumber = 456;
        private string numberString = resultNumber.ToString();



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
            targetValue = new int[] { 9, 8, 4, 5, 6, 7 };

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

                //if (currentValue[i] != targetValue[i])
                //{
                //    //loopCount[i]++;
                //    continue;
                //}

                if (loopCount[i] >= 3)
                {
                    //loopCount[i] = 0;
                    continue; }

                int newYPosition = yPositions[i] + (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);

                //if (currentValue[i] != targetValue[i] && (int)(newYPosition / rectangleHeight) == targetValue[i])

                // Iterate through each character in the string and assign it to the corresponding position in the array
                for (int i2 = 0; i2 < Math.Min(numberString.Length, targetValue.Length); i2++)
                {
                    // Convert the character to an integer and assign it to the array
                    targetValue[i2] = int.Parse(numberString[i2].ToString());
                }
                int min = targetValue[i] * rectangleHeight;
                int max = (targetValue[i] + 1) * rectangleHeight;

                if (!InRange(yPositions[i], min, max))
                {
                    if (newYPosition > numberPillarSprite.Height - rectangleHeight)
                    {
                        newYPosition = newYPosition - numberPillarSprite.Height + rectangleHeight;
                    }

                    if (InRange(newYPosition, min, max))
                    {
                        if (loopCount[i] == 3 - 1)
                        {
                            newYPosition = GetIndexPosition(targetValue[i]);
                        }
                        loopCount[i]++;
                    }
                }


                yPositions[i] = newYPosition;
                //yPositions[i] += (int)(speed * gameTime.ElapsedGameTime.TotalSeconds);

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

        //int[] debugCurr = new int[6];
        //int[] debugNew = new int[6];
        //int[] debugMin = new int[6];
        //int[] debugMax = new int[6];
        //public void AddToDebug()
        //{
        //    DebugInfo.AddString("debugCurr", DebugMethod1);
        //    DebugInfo.AddString("debugNew", DebugMethod2);
        //    DebugInfo.AddString("debugMin", DebugMethod3);
        //    DebugInfo.AddString("debugMax", DebugMethod4);
        //    DebugInfo.AddString("debugBoolCur", DebugMethod5);
        //    DebugInfo.AddString("debugBoolNew", DebugMethod5);
        //}
        //public string DebugMethod1()
        //{
        //    return debugCurr[0].ToString();
        //}

        //public string DebugMethod2()
        //{
        //    return debugNew[0].ToString();
        //}

        //public string DebugMethod3()
        //{
        //    return debugMin[0].ToString();
        //}

        //public string DebugMethod4()
        //{
        //    return debugMax[0].ToString();
        //}

        //public string DebugMethod5()
        //{
        //    return $"{debugMin[0]} <= {debugCurr[0]} <= {debugMax[0]}" + " " + InRange(debugNew[0], debugMin[0], debugMax[0]);
        //}

        //public string DebugMethod6()
        //{
        //    return $"{debugMin[0]} <= {debugNew[0]} <= {debugMax[0]}" + " " + InRange(debugNew[0], debugMin[0], debugMax[0]);
        //}

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

        /// 
        ///
    }
}
