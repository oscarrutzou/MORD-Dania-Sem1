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
        // Add more texture names here
    }


    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> Textures;

        public static SpriteFont arialFont;

        public static void LoadContent(ContentManager content)
        {
            Textures = new Dictionary<TextureNames, Texture2D>
        {
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

            // Add more textures here
        };
            arialFont = content.Load<SpriteFont>("Fonts\\Arial");
        }


    }
}
