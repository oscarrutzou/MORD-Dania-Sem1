﻿using Microsoft.Xna.Framework;
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
        public MissileLauncher(Vector2 position, float scale, Texture2D texture) : base(position, scale, texture)
        {
            //Variables that the projectile need to get spawned
            ProjectileDmg = 50;
            ProjectileSpeed = 200;
            MaxProjectileCanTravel = 500;
            ProjectileTimer = 2f;

            MissileRadius = 90; // En fjerde del 1/4 af ring sprite
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void CreateProjectile()
        {
            Missile tower_Missile= new Missile(
                    this,
                    GlobalTextures.Textures[TextureNames.Projectile_Missile]);

            GameWorld.Instantiate(tower_Missile);
        }


    }
}
