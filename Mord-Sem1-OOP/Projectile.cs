using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Reflection.Metadata;

namespace MordSem1OOP
{
    public enum ProjectileTypes
    {
        Arrow,
        Missile
    }

    public class Projectile : GameObject
    {
        public int Damage { get; set; }
        public ProjectileTypes Type { get; set; }
        public GameObject Target { get; set; }

        /// <summary>
        /// Makes a single arrow for the tower
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="enemyTarget">The target the arrow should hit</param>
        /// <param name="content">This is for calling the GameObject contructer that sets the sprite</param>
        /// <param name="texture">This is for calling the GameObject contructer that sets the sprite</param>
        public Projectile(Vector2 position, float scale, GameObject enemyTarget, int damage, int speed, string texture) 
                : base(texture)
        {
            Position = position;
            Scale = scale;
            Target = enemyTarget;

            Damage = damage;
            Speed = speed;
            Type = ProjectileTypes.Arrow;
            
        }   

        public override void Update(GameTime gameTime)
        {
            //Calculate direction towards target
            direction = Target.Position - Position;
            direction.Normalize();

            // Calculate rotation towards target
            RotateTowards(Target.Position);

            if (Target != null)
            {
                Move(gameTime);
            }
            else
            {
                //Move forward without changing directions
                //float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                //Position += direction * Speed * deltaTime;
            }

            OnCollision();
        }

        public override void OnCollision()
        {
            if (Collision.IsCollidingBox(this, Target))
            {
                //Delete this object
                IsRemoved = true;
                //Target.IsRemoved = true; //Skal ikke være her
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Primitives2D.DrawLine(spriteBatch, Position, Target.Position, Color.Red, 1); //Draws the debug line from current position to the target position
            Primitives2D.DrawRectangle(spriteBatch, Position, Sprite.Rectangle, Color.Red, 1, Rotation); //Draws the collision box
        }

    }
}
