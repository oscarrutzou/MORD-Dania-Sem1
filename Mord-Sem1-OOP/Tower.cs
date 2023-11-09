using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;

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

        private Tower_Arrow tower_Arrow;
        private ContentManager content;
        private string projectileTexture;

        public GameObject Target { get; set; }
        public int ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
        public int ProjectileDmg { get => projectileDmg; set => projectileDmg = value; }
        public bool CanSpawnProjectiles { get => canSpawnProjectiles; set => canSpawnProjectiles = value; }

        public Tower(Vector2 position, float scale, Enemy enemyTarget, ContentManager content, string texture) : base(content, texture)
        {
            Position = position;
            Scale = scale;
            Target = enemyTarget;
            this.content = content;
            
            //Variables that the projectile need to get spawned
            ProjectileDmg = 10;
            ProjectileSpeed = 300;

            tower_Types = Tower_Types.Acher;
            canSpawnProjectiles = true;


        }

        public override void Update(GameTime gameTime)
        {
            //Calculate direction towards target
            direction = Target.Position - Position;
            direction.Normalize();

            // Calculate rotation towards target
            Rotation = (float)Math.Atan2(direction.Y, direction.X);

            spawnProjectileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!canSpawnProjectiles) return;

            if(spawnProjectileTimer >= 1)
            {
                tower_Arrow = new Tower_Arrow(
                    Position,
                    Scale,
                    Target,
                    ProjectileDmg,
                    ProjectileSpeed,
                    content,
                    "Placeholder\\Lasers\\laserBlue04");

                Global.gameObjectsToCreate.Add(tower_Arrow);
                spawnProjectileTimer = 0;
            }

            //C:\Users\oscar\GitHub\Dania\MORD-Dania-Sem1\Mord-Sem1-OOP\Content\Placeholder\Parts\beam6.png
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Primitives2D.DrawRectangle(spriteBatch, Position, Sprite.Rectangle, Color.Red, 1, Rotation); //Draws the collision box
        }
    }
}
