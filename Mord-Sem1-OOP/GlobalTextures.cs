using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        TowerEffect_RadiusRing,
        Enemy_Black1,
        Enemy_Blue1,
        Enemy_Green1,
        Enemy_Red1,
        Pixel,
        GuiBasicButton,
        GuiBasicTowerStats,

        //Non placeholder enemies and turrets
        EnemyNormalSheet,
        EnemyStrongSheet,
        EnemyFastSheet,
        TurretNormalSheet,
        TurretNormalMask,
        TurretBigSheet,
        TurretBigMask,

        GameTitle,

        // Add more texture names here
        NumberPillarSprite
    }


    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> Textures;

        public static SpriteFont arialFont;

        public static void LoadContent(ContentManager content)
        {
            Textures = new Dictionary<TextureNames, Texture2D>
        {
            //Placeholder sprites
                { TextureNames.Projectile_Arrow, content.Load<Texture2D>("Placeholder\\Lasers\\laserBlue04") },
                { TextureNames.Projectile_Missile, content.Load<Texture2D>("Placeholder\\Lasers\\laserRed08") },
                { TextureNames.Tower_Archer, content.Load<Texture2D>("Placeholder\\Parts\\beam6") },
                { TextureNames.Tower_MissileLauncher, content.Load<Texture2D>("Placeholder\\Parts\\beam0") },
                { TextureNames.TowerEffect_RadiusRing, content.Load<Texture2D>("Placeholder\\ring") },
                { TextureNames.Enemy_Black1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyBlack1") },
                { TextureNames.Enemy_Blue1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyBlue1") },
                { TextureNames.Enemy_Green1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyGreen1") },
                { TextureNames.Enemy_Red1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyRed1") },
                { TextureNames.Pixel, content.Load<Texture2D>("Misc/pixel") },
                { TextureNames.GuiBasicButton, content.Load<Texture2D>("Placeholder\\UI\\buttonBlue") },
                { TextureNames.GuiBasicTowerStats, content.Load<Texture2D>("Placeholder\\UI\\BasicTestTowerStats") },

            //Non placeholder enemies and turrets
                { TextureNames.EnemyNormalSheet, content.Load<Texture2D>("Enemies/EnemyNormal") },
                { TextureNames.EnemyStrongSheet, content.Load<Texture2D>("Enemies/EnemyStrong") },
                { TextureNames.EnemyFastSheet, content.Load<Texture2D>("Enemies/EnemyFast") },
                { TextureNames.TurretNormalSheet, content.Load<Texture2D>("Turrets/TurretNormal") },
                { TextureNames.TurretNormalMask, content.Load<Texture2D>("Turrets/TurretNormal_Mask") },
                { TextureNames.TurretBigSheet, content.Load<Texture2D>("Turrets/TurretBig") },
                { TextureNames.TurretBigMask, content.Load<Texture2D>("Turrets/TurretBig_Mask") },
                
                { TextureNames.GameTitle, content.Load<Texture2D>("GameTitle") },
                { TextureNames.NumberPillarSprite, content.Load<Texture2D>("numberPillarSprite") },

        };
            arialFont = content.Load<SpriteFont>("Fonts\\Arial");
        }


    }
}
