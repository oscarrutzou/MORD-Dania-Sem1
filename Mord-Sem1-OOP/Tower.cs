using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MordSem1OOP
{
    public enum Tower_Types
    {
        Acher,
        Laser
    }

    public class Tower : GameObject
    {
        private Tower_Types tower_Types;
        private int projectileDmg;
        private int projectileSpeed;
        private float spawnProjectileTimer;
        private bool canSpawnProjectiles;
        private int maxProjectileCanTravel;
        private Projectile tower_Arrow;

        private float towerLevelMultiplier = 1;

        public List<Enemy> enemiesInRadius {  get; private set; }

        public float Radius { get; set; }
        public Enemy Target { get; set; }
        public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
        public int ProjectileDmg { get => projectileDmg; set => projectileDmg = value; }
        public bool CanSpawnProjectiles { get => canSpawnProjectiles; set => canSpawnProjectiles = value; }
        public int MaxProjectileCanTravel { get => maxProjectileCanTravel; set => maxProjectileCanTravel = value; }

        public Tower(Vector2 position, float scale, float radius, string texture) : base(texture)
        {
            Position = position;
            Scale = scale;
            Radius = radius;
            
            //Variables that the projectile need to get spawned
            ProjectileDmg = 20;
            ProjectileSpeed = 300;
            MaxProjectileCanTravel = 500;

            tower_Types = Tower_Types.Acher;
            canSpawnProjectiles = true;
            enemiesInRadius = new List<Enemy>();

        }

        public override void Update(GameTime gameTime)
        {
            CheckEnemiesInRadius();

            if (enemiesInRadius == null || Target == null) return; //Cant shoot if there are no enemies


            //Calculate direction towards target
            direction = Target.Position - Position;
            direction.Normalize();

            // Calculate rotation towards target
            RotateTowardsWithoutOffSet(Target.Position);

            spawnProjectileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!canSpawnProjectiles) return;

            if(spawnProjectileTimer >= 0.2)
            {
                tower_Arrow = new Arrow(
                    this,
                    Position, //Tag raduis af tower sprite sammen med scale og placer den ved siden (Ville dog ikke være perfect)
                              //This can be changed based on the sprite of the tower, since the projectile shouldn't spawn in the towers orgin point
                    "Placeholder\\Lasers\\laserBlue04"); 

                Global.activeScene.sceneData.gameObjectsToAdd.Add(tower_Arrow);
                spawnProjectileTimer = 0;
            }
        }

        private void CheckEnemiesInRadius()
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

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
