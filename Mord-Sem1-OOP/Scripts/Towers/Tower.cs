using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using MordSem1OOP.Scripts.Towers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MordSem1OOP
{

    public class Tower : GameObject
    {
        #region Fields
        public TowerData towerData;

        //Fields for the projectile of the tower, if it has any
        private int projectileDmg;
        private int projectileSpeed;
        private float spawnProjectileTimer;
        private bool isCooldown;
        private bool hasEnemyInRadius;


        private float projectileTimer;

        private bool canSpawnProjectiles;


        private int maxProjectileCanTravel;



        private float towerLevelMultiplier = 1f;
        private float levelIncrementalMultiplier = 0.2f;

        public List<Enemy> enemiesInRadius {  get; private set; }
        #endregion

        #region Prop
        /// <summary>
        /// Shoot radius of the tower. How far it can shoot.
        /// </summary>
        public float Radius { get; set; }
        public Enemy Target { get; set; }

        public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
        public int ProjectileDmg { get => projectileDmg; set => projectileDmg = value; }
        public bool CanSpawnProjectiles { get => canSpawnProjectiles; set => canSpawnProjectiles = value; }

        /// <summary>
        /// How long the projectile can travel before it deletes itself
        /// </summary>
        public int MaxProjectileCanTravel { get => maxProjectileCanTravel; set => maxProjectileCanTravel = value; }

        /// <summary>
        /// This float determens how long the tower should take before shooting again
        /// </summary>
        public float ProjectileTimer { get => projectileTimer; set => projectileTimer = value; }


        public int TowerLevel { get => towerData.towerLevel; set => towerData.towerLevel = value; }
        public int TowerMaxLevel { get => towerData.towerMaxLevel; set => towerData.towerMaxLevel = value; }

        public float TowerLevelMultiplier { get => towerLevelMultiplier; set => towerLevelMultiplier = value; }
        public float LevelIncrementalMultiplier { get => levelIncrementalMultiplier; set => levelIncrementalMultiplier = value; }
        public float SpawnProjectileTimer { get => spawnProjectileTimer; private set => spawnProjectileTimer = value; }
        #endregion

        public Tower(Vector2 position, float scale, Texture2D texture) : base(texture)
        {
            Position = position;
            Scale = scale;
            Radius = 128f;

            canSpawnProjectiles = true;
            enemiesInRadius = new List<Enemy>();

            SpawnProjectileTimer = ProjectileTimer;
            isCooldown = false;
            towerData = new TowerData();

        }

        /// <summary>
        /// Used for the test, properly delete after use since it dosen't shoot. Maybe its the position or something:)
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="texture"></param>
        public Tower(float scale, Texture2D texture) : this(Vector2.Zero, scale, texture) { }

        public override void Update(GameTime gameTime)
        {
            CheckEnemiesInTowerRadius();

            // Only increment SpawnProjectileTimer if it's less than ProjectileTimer
            if (SpawnProjectileTimer < ProjectileTimer)
            {
                SpawnProjectileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (enemiesInRadius == null || Target == null) return; //Cant shoot if there are no enemies


            //Calculate direction towards target
            direction = Target.Position - Position;
            direction.Normalize();

            // Calculate rotation towards target
            RotateTowardsWithoutOffSet(Target.Position);

            if (SpawnProjectileTimer >= ProjectileTimer)
            {
                // Reset isCooldown when the cooldown timer expires
                isCooldown = false;
                SpawnProjectileTimer = 0;
            }

            if (!canSpawnProjectiles || isCooldown) return;

            // Set isCooldown to true when a projectile is created
            isCooldown = true;
            SpawnProjectileTimer = 0;
            CreateProjectile();

            
        }

        /// <summary>
        /// The method that should be used in towers that makes the projectile
        /// This is not a abtract method since there could be towers that don't use projectiles
        /// </summary>
        protected virtual void CreateProjectile() { }


        private void CheckEnemiesInTowerRadius()
        {
            // Clear the list
            enemiesInRadius.Clear();

            foreach (Enemy enemy in Global.activeScene.sceneData.enemies)
            {
                if (!enemy.IsRemoved && Vector2.Distance(this.Position, enemy.Position) <= this.Radius)
                {
                    enemiesInRadius.Add(enemy);
                }
            }

            // If there was no enemy in the radius in the last frame and there are enemies now, and the tower is not in cooldown, reset the timer
            if (!hasEnemyInRadius && enemiesInRadius.Count > 0 && !isCooldown)
            {
                SpawnProjectileTimer = 0;
            }

            // Update hasEnemyInRadius for the next frame
            hasEnemyInRadius = enemiesInRadius.Count > 0;

            OrderEnemiesByDistanceTravled();

        }

        private void OrderEnemiesByDistanceTravled()
        {
            if (enemiesInRadius == null) return;

            Target = enemiesInRadius.OrderByDescending(enemy => ((Enemy)enemy).DistanceTraveled).FirstOrDefault();
        }

        public virtual void LevelUpTower()
        {
            if (TowerLevel <= TowerMaxLevel)
            {
                TowerLevel++;
                TowerLevelMultiplier *= (1 + LevelIncrementalMultiplier);
                ProjectileDmg *= (int)TowerLevelMultiplier;
            }
        }

        public override void Draw()
        {
            base.Draw();

            //Maybe use the Sprite script?
            //Texture2D circleTexture = CreateCircleTexture(spriteBatch.GraphicsDevice, (int)Radius);
            //Vector2 origin = new Vector2(circleTexture.Width / 2, circleTexture.Height / 2);

            Sprite radiusRing = new Sprite(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing]);

            Vector2 drawPosition = Position - radiusRing.Origin;

            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing], drawPosition, Color.Red);

            Primitives2D.DrawRectangle(GameWorld._spriteBatch, Position, Sprite.Rectangle, Color.Red, 1, Rotation); //Draws the collision box


            //Draw tower data
            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont, SpawnProjectileTimer.ToString(), new Vector2(Position.X + 30, Position.Y - 30), Color.Black);

            GameWorld._spriteBatch.DrawString(GlobalTextures.arialFont, towerData.towerKills.ToString(), new Vector2(Position.X + 30, Position.Y - 10), Color.Black);

        }



    }
}
//GameWorld._spriteBatch.DrawString(arialFont, SpawnProjectileTimer.ToString(), new Vector2(10, 10), Color.Black);
//GameWorld._spriteBatch.DrawString(arialFont, towerData.towerKills.ToString(), new Vector2(0, 0), Color.Black);