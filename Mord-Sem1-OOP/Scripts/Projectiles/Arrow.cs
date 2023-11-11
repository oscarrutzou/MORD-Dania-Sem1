﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Reflection.Metadata;


namespace MordSem1OOP
{
    public class Arrow : Projectile
    {
    
        public Arrow(Tower tower, Texture2D texture) : base(tower, texture)
        {
            

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

            OnCollisionBox();

        }

        protected void MoveWithFixedDistance(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += direction * Speed * deltaTime;

            // Update the distance traveled
            DistanceTraveled += Speed * deltaTime;

            // Check if the projectile has traveled more than 500px
            if (DistanceTraveled > MaxProjectileCanTravel)
            {
                IsRemoved = true;
            }
        }

        public override void OnCollisionBox()
        {
            if (Target != null && !Target.IsRemoved && Collision.IsCollidingBox(this, Target))
            {
                IsRemoved = true; // Delete this object

                Target.TakeDamage(Damage); // Damage target enemy with the damage amount from the tower
            }
        }
    }
}
