using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spaceship.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Towers
{
    public class GunTurret : Tower
    {
        SpriteSheet sheet;

        public GunTurret(Vector2 position, float scale, Texture2D texture) : base(position, scale, texture)
        {
            sheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Gun_Turret_Sheet], 2, true);
            sheet.Rotation = 1.5708f;
            Scale = 1.20f;
            //Variables that the projectile need to get spawned
            ProjectileDmg = 20;
            ProjectileSpeed = 200;
            MaxProjectileCanTravel = 500;
            ProjectileTimer = 0.4f;

            //On Lvl up
            ProjectileExtraDmgOnLvlUp = 5;
            towerData.buyAmount = 200;
            towerData.buyTowerUpgradeAmount = 50;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            sheet.Draw(Position, Rotation, Scale);
        }

        protected override void CreateProjectile()
        {
            Arrow tower_Arrow = new Arrow(
                    this,
                    GlobalTextures.Textures[TextureNames.Projectile_Arrow]);

            

            GameWorld.Instantiate(tower_Arrow);
        }


    }
}
