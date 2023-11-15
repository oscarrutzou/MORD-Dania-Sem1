﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using Spaceship.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MordSem1OOP
{
    public class CannonTurret : Tower
    {
        SpriteSheet sheet;

        /// <summary>
        /// Radius of missile
        /// </summary>
        public int MissileRadius { get; set; }
        public CannonTurret(Vector2 position, float scale, Texture2D texture) : base(position, scale, texture)
        {
            sheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Cannon_Turret_Sheet], 3, true);
            sheet.Rotation = 1.5708f;
            Scale = 1.2f;
            //Variables that the projectile need to get spawned
            ProjectileDmg = 50;
            ProjectileSpeed = 200;
            MaxProjectileCanTravel = 500;
            ProjectileTimer = 2f;

            MissileRadius = 68; // En fjerde del 1/4 af ring sprite
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            sheet.Draw(Position, Rotation, Scale);
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
            ExplosiveShell tower_Missile= new ExplosiveShell(
                    this,
                    GlobalTextures.Textures[TextureNames.Projectile_Missile]);

            GameWorld.Instantiate(tower_Missile);
        }


    }
}