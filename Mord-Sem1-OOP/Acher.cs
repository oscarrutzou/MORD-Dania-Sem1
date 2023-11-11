using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public class Acher : Tower
    {
        public Acher(Vector2 position, float scale, float radius, Texture2D texture) : base(position, scale, radius, texture)
        {
            //Variables that the projectile need to get spawned
            ProjectileDmg = 10;
            ProjectileSpeed = 200;
            MaxProjectileCanTravel = 500;
            ProjectileTimer = 0.4f;
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void CreateProjectile()
        {
            Arrow tower_Arrow = new Arrow(
                    this,
                    GlobalTextures.Textures[TextureNames.Projectile_Arrow]);

            Global.gameWorld.Instantiate(tower_Arrow);
        }


    }
}
