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
        // Track the distance traveled
        private float distanceTraveled = 0;
        public int Damage { get; set; }
        public int MaxProjectileCanTravel { get; set; }
        public ProjectileTypes Type { get; set; }
        public Enemy Target { get; set; }

        /// <summary>
        /// Makes a single arrow for the tower
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="enemyTarget">The target the arrow should hit</param>
        /// <param name="content">This is for calling the GameObject contructer that sets the sprite</param>
        /// <param name="texture">This is for calling the GameObject contructer that sets the sprite</param>
        public Projectile(Vector2 position, float scale, Enemy enemyTarget, int damage, int speed, int maxProjectileCanTravel, string texture) 
                : base(texture)
        {
            Position = position;
            Scale = scale;
            Target = enemyTarget;

            Damage = damage;
            Speed = speed;
            MaxProjectileCanTravel = maxProjectileCanTravel;
            Type = ProjectileTypes.Arrow;
        }

        public override void Update(GameTime gameTime)
        {
            //Calculate direction towards target
            if (Target != null && !Target.IsRemoved)
            {
                direction = Target.Position - Position;
                direction.Normalize();

                // Calculate rotation towards target
                RotateTowardsWithOffset(Target.Position);
            }

            // Always move, regardless of whether there's a target
            MoveWithFixedDistance(gameTime);

            OnCollision();
        }

        protected void MoveWithFixedDistance(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += direction * Speed * deltaTime;

            // Update the distance traveled
            distanceTraveled += Speed * deltaTime;

            // Check if the projectile has traveled more than 500px
            if (distanceTraveled > MaxProjectileCanTravel)
            {
                IsRemoved = true;
            }
        }

        public override void OnCollision()
        {
            if (Target != null && !Target.IsRemoved && Collision.IsCollidingBox(this, Target))
            {
                //Delete this object
                IsRemoved = true;
                Target.Damaged(Damage);
                //Target.IsRemoved = true;
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
