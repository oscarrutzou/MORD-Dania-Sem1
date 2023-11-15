using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Reflection.Metadata;

namespace MordSem1OOP
{

    public class Projectile : GameObject
    {
        /// <summary>
        /// Track the distance traveled for projectiles that need to be deleted if they don't hit anything
        /// </summary>
        private float distanceTraveled = 0;

        public int Damage { get; set; }
        public int MaxProjectileCanTravel { get; set; }
        public Enemy Target { get; set; }
        public float DistanceTraveled { get => distanceTraveled; set => distanceTraveled = value; }

        public Tower Tower { get; set; }

        /// <summary>
        /// Makes a single projectile for the tower
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="enemyTarget">The target the arrow should hit</param>
        /// <param name="content">This is for calling the GameObject contructer that sets the sprite</param>
        /// <param name="texture">This is for calling the GameObject contructer that sets the sprite</param>
        public Projectile(Tower tower, Texture2D texture) : base(texture)
        {
            Tower = tower;
            Scale = tower.Scale;
            Target = tower.Target;
            Damage = tower.ProjectileDmg;

            Speed = tower.ProjectileSpeed;
            MaxProjectileCanTravel = tower.MaxProjectileCanTravel;

            SetCorrectProjectilePosition();
        }

        public override void Update(GameTime gameTime)
        {

        }


        protected void SetCorrectProjectilePosition()
        {
            // Offset from the center of the tower to the right side of the tower sprite
            Vector2 offset = new Vector2(Tower.Sprite.Width / 2, 0);

            // Add half the height of the projectile sprite to the offset
            offset.X += Sprite.Height / 2; //Should use the width when it has a proper texture that faces right

            // Rotate the offset by the same amount as the tower
            float cos = (float)Math.Cos(Tower.Rotation);
            float sin = (float)Math.Sin(Tower.Rotation);
            Vector2 rotatedOffset = new Vector2(
                offset.X * cos - offset.Y * sin,
                offset.X * sin + offset.Y * cos);

            // Add the rotated offset to the tower's position
            Position = Tower.Position + rotatedOffset;
        }


        public override void Draw()
        {
            base.Draw();
            Primitives2D.DrawLine(GameWorld._spriteBatch, Position, Target.Position, Color.Red, 1); //Draws the debug line from current position to the target position
            Primitives2D.DrawRectangle(GameWorld._spriteBatch, Position, Sprite.Rectangle, Color.Red, 1, Rotation); //Draws the collision box
        }

    }
}
