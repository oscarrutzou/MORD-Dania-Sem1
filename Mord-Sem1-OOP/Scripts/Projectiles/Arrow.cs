using Microsoft.Xna.Framework;
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
            base.Update(gameTime);
        }

        public override void OnCollisionBox()
        {
            base.OnCollisionBox();
        }
    }
}
