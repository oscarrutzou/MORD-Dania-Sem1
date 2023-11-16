using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MordSem1OOP
{
    //Call GlobalTexturs like this "GlobalTextures.Textures[TextureNames.Arrow_Projectile]"
    public enum TextureNames
    {
        //Placeholder sprites
        Projectile_Arrow,
        Projectile_Missile,
        Tower_Archer,
        Tower_MissileLauncher,
        TowerEffect_MissileRadiusRing,
        TowerEffect_RadiusRing,
        Enemy_Black1,
        Enemy_Blue1,
        Enemy_Green1,
        Enemy_Red1,
        Pixel,
        GuiBasicButton,
        GuiBasicTowerStats,

        //Non placeholder enemies and turrets
        Enemy_Normal_Sheet,
        Enemy_Strong_Sheet,
        Enemy_Fast_Sheet,
        Gun_Turret_Sheet,
        Gun_Turret_Flash,
        Cannon_Turret_Sheet,
        Cannon_Turret_Flash,
        Bullet,
        Shell,
        Placement,

        GameTitle,
        DeathTitle,

        // Add more texture names here
        NumberPillarSprite
    }


    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> Textures;

        public static SpriteFont defaultFont;

        public static void LoadContent(ContentManager content)
        {
            Textures = new Dictionary<TextureNames, Texture2D>
        {
            //Placeholder sprites
                { TextureNames.Projectile_Arrow, content.Load<Texture2D>("Placeholder\\Lasers\\laserBlue04") },
                { TextureNames.Projectile_Missile, content.Load<Texture2D>("Placeholder\\Lasers\\laserRed08") },
                { TextureNames.Tower_Archer, content.Load<Texture2D>("Placeholder\\Parts\\beam6") },
                { TextureNames.Tower_MissileLauncher, content.Load<Texture2D>("Placeholder\\Parts\\beam0") },
                { TextureNames.TowerEffect_RadiusRing, content.Load<Texture2D>("Placeholder\\SpriteRing") },
                { TextureNames.TowerEffect_MissileRadiusRing, content.Load<Texture2D>("Placeholder\\MissileRadius") },
                { TextureNames.Enemy_Black1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyBlack1") },
                { TextureNames.Enemy_Blue1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyBlue1") },
                { TextureNames.Enemy_Green1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyGreen1") },
                { TextureNames.Enemy_Red1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyRed1") },
                { TextureNames.Pixel, content.Load<Texture2D>("Misc/pixel") },
                { TextureNames.GuiBasicButton, content.Load<Texture2D>("Gui\\ButtonSprite") },
                { TextureNames.GuiBasicTowerStats, content.Load<Texture2D>("Gui\\TowerStatsUI") },

            //Non placeholder enemies and turrets
                { TextureNames.Enemy_Normal_Sheet, content.Load<Texture2D>("Enemies/EnemyNormal") },
                { TextureNames.Enemy_Strong_Sheet, content.Load<Texture2D>("Enemies/EnemyStrong") },
                { TextureNames.Enemy_Fast_Sheet, content.Load<Texture2D>("Enemies/EnemyFast") },
                { TextureNames.Gun_Turret_Sheet, content.Load<Texture2D>("Turrets/TurretNormal") },
                { TextureNames.Gun_Turret_Flash, content.Load<Texture2D>("Turrets/TurretNormal_Mask") },
                { TextureNames.Cannon_Turret_Sheet, content.Load<Texture2D>("Turrets/TurretBig") },
                { TextureNames.Cannon_Turret_Flash, content.Load<Texture2D>("Turrets/TurretBig_Mask") },
                { TextureNames.Bullet, content.Load<Texture2D>("Projectiles/bullet") },
                { TextureNames.Shell, content.Load<Texture2D>("Projectiles/shell") },
                { TextureNames.GameTitle, content.Load<Texture2D>("GameTitle") },
                { TextureNames.DeathTitle, content.Load<Texture2D>("deathImage") },
                { TextureNames.NumberPillarSprite, content.Load<Texture2D>("numberPillarSprite") },
                { TextureNames.Placement, content.Load<Texture2D>("Misc/placementFrame") },

        };
            defaultFont = content.Load<SpriteFont>("Fonts\\Arial");
        }


    }
}
