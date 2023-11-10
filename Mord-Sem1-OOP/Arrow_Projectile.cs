using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Reflection.Metadata;


namespace MordSem1OOP
{
    public class Arrow_Projectile : Projectile
    {

        public Arrow_Projectile(Tower tower, Vector2 position, Texture2D texture) : base(tower, position, texture)
        {
            

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnCollision()
        {
            base.OnCollision();
        }
    }
}
