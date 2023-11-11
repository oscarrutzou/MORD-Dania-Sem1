using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
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
                    enemy.Damaged(Damage);
                }
            }

            // After exploding and damaging enemies, remove the missile
            IsRemoved = true;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //Maybe use the Sprite script?
            Texture2D circleTexture = CreateCircleTexture(spriteBatch.GraphicsDevice, (int)missileLauncher.MissileRadius);
            Vector2 origin = new Vector2(circleTexture.Width / 2, circleTexture.Height / 2);

            spriteBatch.Draw(circleTexture, Position, null, Color.Red * 0.5f, 0, origin, 1, SpriteEffects.None, 0);
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
