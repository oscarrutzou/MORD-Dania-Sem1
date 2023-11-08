using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MordSem1OOP.Scripts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{

    public class Tower_Arrow : GameObject, IProjectile
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
        public Tower_Arrow(Vector2 position, float scale, Enemy enemyTarget, ContentManager content, string texture) : base(content, texture)
        {
            Position = position;
            Scale = scale;
            Target = enemyTarget;

            Damage = 10;
            Speed = 50;
            Type = ProjectileTypes.Arrow;
            
        }   

        public override void Update(GameTime gameTime)
        {
            //Calculate direction towards target
            direction = Target.Position - Position;
            direction.Normalize();

            // Calculate rotation towards target
            Rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2;

            Move(gameTime);
            OnCollision();
        }

        public override void OnCollision()
        {
            if (Collision.IsColliding(this, Target))
            {
                //Delete this object, add money and stuff
            }
        }
    }
}
