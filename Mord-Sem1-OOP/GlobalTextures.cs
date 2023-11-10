﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        Tower_Arrow,
        Enemy_Black1,
        Enemy_Blue1,
        Enemy_Green1,
        Enemy_Red1,
        // Add more texture names here
    }


    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> Textures;
        
        public static void LoadContent(ContentManager content)
        {
            Textures = new Dictionary<TextureNames, Texture2D>
        {
            { TextureNames.Projectile_Arrow, content.Load<Texture2D>("Placeholder\\Lasers\\laserBlue04") },
            { TextureNames.Tower_Arrow, content.Load<Texture2D>("Placeholder\\Parts\\beam6") },
            { TextureNames.Enemy_Black1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyBlack1") },
            { TextureNames.Enemy_Blue1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyBlue1") },
            { TextureNames.Enemy_Green1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyGreen1") },
            { TextureNames.Enemy_Red1, content.Load<Texture2D>("Placeholder\\Enemies\\enemyRed1") },
            // Add more textures here
        };
        }
    }
}