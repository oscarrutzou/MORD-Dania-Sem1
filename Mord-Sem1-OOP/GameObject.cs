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
        public bool IsRemoved { get; set; }

        #endregion

        #region Constructors

        public GameObject()
        {
            //This default constructor does nothing but must be present because it is called elsewhere.
            //Does not need to be fixed immediately, fix when you have extra time and are bored.
        }

        protected GameObject(string texture)
        {
            Sprite = new Sprite(texture);
        }

        protected GameObject(Texture2D texture)
        {
            Sprite = new Sprite(texture);
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
        public virtual void Draw()
        {
            Sprite.Draw(Position, Rotation, Scale);
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

        /// <summary>
        /// Makes this GameObject move towards a target destination
        /// </summary>
        /// <param name="destination">The point in space which this GameObject will move towards</param>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        /// <returns></returns>
        protected bool AlternativeMove(Vector2 destination, GameTime gameTime)
        {
            return AlternativeMove(destination, gameTime, out _);
        }

        /// <summary>
        /// This makes the GameObject move towards a target position.
        /// </summary>
        /// <param name="destination">The position to move towards.</param>
        /// <param name="gameTime">This is used to make the movement speed independent of framerate</param>
        /// <param name="distanceTravelled"></param>
        /// <returns>Returns true if the GameObject has reached its destination.</returns>
        protected bool AlternativeMove(Vector2 destination, GameTime gameTime, out float distanceTravelled)
        {
            distanceTravelled = 0f;

            if (Position == destination)
                return true;

            Vector2 direction = destination - Position;
            direction.Normalize();

            // Calculate rotation towards target
            RotateTowardsWithOffset(destination);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 newPosition = Position + direction * Speed * deltaTime;

            float distanceToDestination = Vector2.Distance(Position, destination);
            float distanceToNewPosition = Vector2.Distance(Position, newPosition);

            //Setting the position
            if (distanceToNewPosition >= distanceToDestination)
            {
                Position = destination;
                distanceTravelled = distanceToDestination;
                return true;
            }
            else
            {
                Position = newPosition;
                distanceTravelled = distanceToNewPosition;
                return false;
            }
        }

        public virtual void OnCollisionBox() { }
        public virtual void OnCollisionCircle() { }


        /// <summary>
        /// Makes this GameObject look at a point in space, with the offset so the sprite should be pointing up
        /// </summary>
        /// <param name="target">The point to look at</param>
        public void RotateTowardsWithOffset(Vector2 target) //IMPORTANT: REMOVE OFFSET THING WHEN WE HAVE OUR OWN SPRITES!
        {
            if (Position == target) return;

            Vector2 dir = target - Position;
            Rotation = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.PiOver2;
        }

        /// <summary>
        /// Makes this GameObject look at a point in space, with the offset so the sprite should be pointing right
        /// </summary>
        /// <param name="target">The point to look at</param>
        public void RotateTowardsWithoutOffSet(Vector2 target)
        {
            if (Position == target) return;

            Vector2 dir = target - Position;
            Rotation = (float)Math.Atan2(dir.Y, dir.X);
        }

        #endregion
    }
}
