using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    internal abstract class GameObject: Transform
    {
        #region Fields
        protected Texture2D sprite;
        protected Texture2D[] sprites; //Used to store the animation frames of the sprite

        protected float animationSpeed = 10; //Animation frames per second
        private float animationTime;
        private int currentIndex;

        protected float speed;
        protected Vector2 direction;
        #endregion


        #region Properties
        private Texture2D CurrentSprite
        {
            get
            {
                if (sprites != null) return sprites[(int)animationTime];
                return sprite;
            }
        }
        protected Vector2 SpriteSize
        {
            get
            {
                return new Vector2(CurrentSprite.Width * Scale, CurrentSprite.Height * Scale);
            }
        }
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)(Position.X - sprite.Width / 2 * Scale),
                    (int)(Position.Y - sprite.Height / 2 * Scale),
                    (int)(sprite.Width * Scale),
                    (int)(sprite.Height * Scale)
                    );
            }
        }
        #endregion


        #region Methods

        public abstract void LoadContent(ContentManager content);

        /// <summary>
        /// Update is called every frame
        /// </summary>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Changes the current sprite to another sprite in the sprites array over time. The animationSpeed field is sprites changed per second.
        /// </summary>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        protected void Animate(GameTime gameTime)
        {
            animationTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentIndex = (int)(animationTime * animationSpeed);
            sprite = sprites[currentIndex];

            //Reset
            if (currentIndex >= sprites.Length - 1)
            {
                currentIndex = 0;
                animationTime = 0;
            }
        }

        /// <summary>
        /// Draws the current sprite to the screen, using the Position, Rotation, Scale and origin point.
        /// </summary>
        /// <param name="spriteBatch">Contains the required draw method</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(CurrentSprite.Width / 2, CurrentSprite.Height / 2);
            spriteBatch.Draw(sprite, Position, null, Color.White, Rotation, origin, Scale, SpriteEffects.None, 0);
        }


        /// <summary>
        /// Changes the Position field based on the direction specified by the direction field, by the amount of the speed field.
        /// </summary>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += direction * speed * deltaTime * 100;
        }
        

        /// <summary>
        /// Uses the basic rectangle collision algorithm to check if there is a collision
        /// </summary>
        /// <param name="other">The other GameObject, that this GameObject checks for a collision with</param>
        /// <returns>returns true if there is an intersection</returns>
        public bool IsColliding(GameObject other)
        {
            if (this == other) return false;

            return CollisionBox.Intersects(other.CollisionBox);
        }


        /// <summary>
        /// OnCollision is called every frame while two GameObjects overlap.
        /// </summary>
        /// <param name="other"> The GameObject which this object is currently overlapping with. </param>
        public virtual void OnCollision(GameObject other) { }


        #endregion
    }
}
