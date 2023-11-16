using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spaceship.Scripts;
using System.Windows.Forms;

namespace MordSem1OOP.Scripts.Towers
{
    public class GunTurret : Tower
    {
        SpriteSheet sheet;
        public static int towerBuyAmount = 200;
        private Sprite _flash;
        private bool _showFlash;
        private const int _flashDurationMs = 150;
        private int _flashTimerMs;
        public GunTurret(Vector2 position, float scale, Texture2D texture) : base(position, scale, texture)
        {
            Sprite = sheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Gun_Turret_Sheet], 2, true);
            sheet.Rotation = 1.5708f;
            _flash = new Sprite(GlobalTextures.Textures[TextureNames.Gun_Turret_Flash]);
            _flash.Rotation = 1.5708f;
            _flash.DepthLayer = .1f;
            Scale = 1.20f;

            //Variables that the projectile need to get spawned
            ProjectileDmg = 20;
            ProjectileSpeed = 200;
            MaxProjectileCanTravel = 500;
            ProjectileTimer = 0.4f;

            //On Lvl up
            ProjectileExtraDmgOnLvlUp = 5;
            towerData.buyAmount = towerBuyAmount;
            towerData.buyTowerUpgradeAmount = 50;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            FlashFade(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
            if (_showFlash)
                _flash.Draw(Position, Rotation, Scale);
        }

        protected override void Shoot()
        {
            base.Shoot();
            _showFlash = true;
            _flashTimerMs = 0;
        }

        private void FlashFade(GameTime gameTime)
        {
            _flashTimerMs += gameTime.ElapsedGameTime.Milliseconds;
            if (_flashTimerMs >= _flashDurationMs)
            {
                _flashTimerMs -= _flashDurationMs;
                _showFlash = false;
            }
        }

        protected override void CreateProjectile()
        {
            Arrow bullet = new Arrow(
                    this,
                    GlobalTextures.Textures[TextureNames.Bullet]);
            bullet.Sprite.Rotation = 3.14159f;

            GameWorld.Instantiate(bullet);
        }
    }
}
