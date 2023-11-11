using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MordSem1OOP
{
    public class MissileLauncher : Tower
    {
        /// <summary>
        /// Radius of missile
        /// </summary>
        public int MissileRadius { get; set; }

        public MissileLauncher(Vector2 position, float scale, float radius, Texture2D texture) : base(position, scale, radius, texture)
        {
            //Variables that the projectile need to get spawned
            ProjectileDmg = 100;
            ProjectileSpeed = 200;
            MaxProjectileCanTravel = 500;
            ProjectileTimer = 2f;
            MissileRadius = 50;
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void LevelUpTower()
        {
            if (TowerLevel <= TowerMaxLevel)
            {
                TowerLevel++;
                TowerLevelMultiplier *= (1 + LevelIncrementalMultiplier);
                ProjectileDmg *= (int)TowerLevelMultiplier;
                
            }
        }


        protected override void CreateProjectile()
        {
            Missile tower_Missile= new Missile(
                    this,
                    GlobalTextures.Textures[TextureNames.Projectile_Missile]);

            Global.gameWorld.Instantiate(tower_Missile);
        }


    }
}
