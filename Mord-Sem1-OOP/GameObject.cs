using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using MordSem1OOP.Scripts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public abstract class GameObject
    {
        #region Fields
        private Vector2 position;
        private float rotation;
        private float scale = 1;

        private ISprite sprite;

        private float speed;
        protected Vector2 direction;
        #endregion

        #region Properties
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

        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public float Scale { get => scale; set => scale = value; }
        protected float Speed { get => speed; set => speed = value; }
        public ISprite Sprite { get => sprite; set => sprite = value; }

        public GameObject()
        {
            //This default constructor does nothing but must be present because it is called elsewhere.
            //Does not need to be fixed immediately, fix when you have extra time and are bored.
        }

        protected GameObject(ContentManager content, string texture)
        {
            Sprite = new Sprite(content,texture);
        }

        #endregion

        #region Methods
        

        /// <summary>
        /// Update is called every frame
        /// </summary>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the sprite (with ISprite) to the screen, using the Position, Rotation, Scale and origin point.
        /// </summary>
        /// <param name="spriteBatch">Contains the required draw method</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position, Rotation, Scale);
        }

        /// <summary>
        /// Used to change the position of a GameObject over time, change is specified by the direction and speed.
        /// </summary>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += direction * Speed * deltaTime;
        }

        public virtual void OnCollision() { }

        #endregion
    }
}
