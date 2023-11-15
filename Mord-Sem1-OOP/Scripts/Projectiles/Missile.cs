using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public class Missile : Projectile
    {

        private MissileLauncher missileLauncher;
        /// <summary>
        /// Its the position that dosen't move with the target position from the tower.
        /// </summary>
        public Vector2 FixedTargetPosition { get; set; }

        public Missile(MissileLauncher tower, Texture2D texture) : base(tower, texture)
        {
            missileLauncher = tower;
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
                if (!enemy.IsRemoved && Vector2.Distance(this.Position, enemy.Position) <= missileLauncher.MissileRadius)
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

            Sprite radiusRing = new Sprite(GlobalTextures.Textures[TextureNames.TowerEffect_MissileRadiusRing]);

            GameWorld._spriteBatch.Draw(GlobalTextures.Textures[TextureNames.TowerEffect_MissileRadiusRing],
                             Position,
                             null,
                             Color.Red * 0.4f,
                             Rotation,
                             radiusRing.Origin,
                             1f,
                             SpriteEffects.None,
                             0);
        }

    }
}
