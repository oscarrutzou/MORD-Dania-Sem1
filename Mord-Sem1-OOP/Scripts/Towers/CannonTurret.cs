using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using Spaceship.Scripts;

namespace MordSem1OOP
{
    public class CannonTurret : Tower
    {
        public static int towerBuyAmount = 400;
        SpriteSheet sheet;
        private Sprite _flash;
        private bool _showFlash;
        private const int _flashDurationMs = 150;
        private int _flashTimerMs;
        /// <summary>
        /// Radius of missile
        /// </summary>
        public int MissileRadius { get; set; }
        public CannonTurret(Vector2 position, float scale, Texture2D texture) : base(position, scale, texture)
        {
            Sprite = sheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Cannon_Turret_Sheet], 3, true);
            sheet.Rotation = 1.5708f;
            _flash = new Sprite(GlobalTextures.Textures[TextureNames.Cannon_Turret_Flash]);
            _flash.Rotation = 1.5708f;
            Scale = 1.2f;
            //Variables that the projectile need to get spawned
            ProjectileDmg = 50;
            ProjectileSpeed = 200;
            MaxProjectileCanTravel = 500;
            ProjectileTimer = 2f;

            MissileRadius = 90; // Also the size of the missilie radius sprite

            //On Lvl up
            ProjectileExtraDmgOnLvlUp = 10;
            towerData.buyAmount = towerBuyAmount;
            towerData.buyTowerUpgradeAmount = 100; 
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
            ExplosiveShell shell = new ExplosiveShell(
                    this,
                    GlobalTextures.Textures[TextureNames.Shell]);
            shell.Sprite.Rotation = 3.14159f;

            GameWorld.Instantiate(shell);
        }


    }
}
