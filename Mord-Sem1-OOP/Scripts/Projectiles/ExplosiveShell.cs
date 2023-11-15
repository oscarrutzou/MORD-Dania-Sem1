using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;

namespace MordSem1OOP
{
    internal class ExplosiveShell : Projectile
    {
        private CannonTurret cannonTurret;
        /// <summary>
        /// Its the position that dosen't move with the target position from the tower.
        /// </summary>
        public Vector2 FixedTargetPosition { get; set; }

        public ExplosiveShell(CannonTurret tower, Texture2D texture) : base(tower, texture)
        {
            cannonTurret = tower;
            FixedTargetPosition = tower.Target.Position;
        }

        public override void Update(GameTime gameTime)
        {
            // Move towards the target position
            direction = FixedTargetPosition - Position;
            direction.Normalize();

            // Calculate rotation towards target
            RotateTowardsWithOffset(FixedTargetPosition);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += direction * Speed * deltaTime;

            // If the missile has reached the target position, make it explode
            if (Vector2.Distance(Position, FixedTargetPosition) <= Speed * deltaTime)
            {
                OnCollisionCircle();
            }
        }

        public override void OnCollisionCircle()
        {
            foreach (Enemy enemy in Global.activeScene.sceneData.enemies)
            {
                if (!enemy.IsRemoved && Vector2.Distance(this.Position, enemy.Position) <= cannonTurret.MissileRadius)
                {
                    enemy.TakeDamage(Damage);

                    if (enemy.IsRemoved)
                    {
                        Tower.towerData.towerKills++;
                    }
                }
            }

            // After exploding and damaging enemies, remove the missile
            IsRemoved = true;
        }


        public override void Draw()
        {
            base.Draw();

            Sprite radiusRing = new Sprite(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing]);

            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.TowerEffect_RadiusRing],
                             Position,
                             null,
                             Color.Red,
                             Rotation,
                             radiusRing.Origin,
                             0.5f,
                             SpriteEffects.None,
                             0);
        }
    }
}
