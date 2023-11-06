﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MordSem1OOP.Scripts.Interface
{
    public interface ISprite
    {
        int Width { get; }
        int Height { get; }
        Vector2 Origin { get; }
        Color Color { get; set; }

        void LoadContent(ContentManager content, string loadTexture);
        void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation);
        void SetOrigin(Vector2 origin);
        void SetOriginCenter();
    }
}