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
        public int Speed { get; set; }
        public ProjectileTypes Type { get; set; }
        public GameObject Target { get; set; }


        public Tower_Arrow(Vector2 position, float rotation, float scale, Enemy enemyTarget)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Target = enemyTarget;

            Damage = 10;
            Speed = 100;
            Type = ProjectileTypes.Arrow;
            
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = content.Load<ISprite>("Placeholder\\Enemies\\enemyBlack1");
        }
        

        public override void Update(GameTime gameTime)
        {
            //Move toward target from current position
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
