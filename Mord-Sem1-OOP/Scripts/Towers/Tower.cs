using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MordSem1OOP
{

    public class Tower : GameObject
    {
        #region Fields
        private int projectileDmg;
        private int projectileSpeed;
        private float spawnProjectileTimer;

        /// <summary>
        /// This float determens how long the tower should take before shooting again
        /// </summary>
        private float projectileTimer;

        private bool canSpawnProjectiles;

        /// <summary>
        /// How long before the projectile deletes itself
        /// </summary>
        private int maxProjectileCanTravel;


        private int towerLevel = 1;
        private int towerMaxLevel = 5;
        private float towerLevelMultiplier = 1f;
        private float levelIncrementalMultiplier = 0.2f;

        public List<Enemy> enemiesInRadius {  get; private set; }
        #endregion

        #region Prop
        public float Radius { get; set; }
        public Enemy Target { get; set; }
        public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
        public int ProjectileDmg { get => projectileDmg; set => projectileDmg = value; }
        public bool CanSpawnProjectiles { get => canSpawnProjectiles; set => canSpawnProjectiles = value; }
        public int MaxProjectileCanTravel { get => maxProjectileCanTravel; set => maxProjectileCanTravel = value; }
        public float ProjectileTimer { get => projectileTimer; set => projectileTimer = value; }


        public int TowerLevel { get => towerLevel; set => towerLevel = value; }
        public int TowerMaxLevel { get => towerMaxLevel; set => towerMaxLevel = value; }
        public float TowerLevelMultiplier { get => towerLevelMultiplier; set => towerLevelMultiplier = value; }
        public float LevelIncrementalMultiplier { get => levelIncrementalMultiplier; set => levelIncrementalMultiplier = value; }
        #endregion

        public Tower(Vector2 position, float scale, float radius, Texture2D texture) : base(texture)
        {
            Position = position;
            Scale = scale;
            Radius = radius;

            canSpawnProjectiles = true;
            enemiesInRadius = new List<Enemy>();

        }

        /// <summary>
        /// Used for the test, properly delete after use:)
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="radius"></param>
        /// <param name="texture"></param>
        public Tower(float scale, float radius, Texture2D texture) : this(Vector2.Zero, scale, radius, texture) { }

        public override void Update(GameTime gameTime)
        {
            CheckEnemiesInTowerRadius();

            if (enemiesInRadius == null || Target == null) return; //Cant shoot if there are no enemies


            //Calculate direction towards target
            direction = Target.Position - Position;
            direction.Normalize();

            // Calculate rotation towards target
            RotateTowardsWithoutOffSet(Target.Position);

            spawnProjectileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!canSpawnProjectiles) return;

            if(spawnProjectileTimer >= ProjectileTimer)
            {
                CreateProjectile();

                spawnProjectileTimer = 0;
            }
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //Maybe use the Sprite script?
            Texture2D circleTexture = CreateCircleTexture(spriteBatch.GraphicsDevice, (int)Radius);
            Vector2 origin = new Vector2(circleTexture.Width / 2, circleTexture.Height / 2);

            spriteBatch.Draw(circleTexture, Position, null, Color.Red * 0.5f, 0, origin, 1, SpriteEffects.None, 0);

            Primitives2D.DrawRectangle(spriteBatch, Position, Sprite.Rectangle, Color.Red, 1, Rotation); //Draws the collision box
        }

        private Texture2D CreateCircleTexture(GraphicsDevice graphicsDevice, int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(graphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }

    }
}
