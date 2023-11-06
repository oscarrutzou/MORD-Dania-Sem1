using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts.Interface;
using System;

namespace MordSem1OOP.Scripts
{
    internal class Sprite : ISprite
    {
        private Texture2D _texture;
        private Vector2 _origin;
        private Color _color;

        public int Width => _texture.Width;
        public int Height => _texture.Height;
        public Vector2 Origin => _origin;
        public Color Color { get => _color; set => _color = value; }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public Sprite(ContentManager content, string texture)
        {
            LoadContent(content, texture);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, float scale)
        {
            spriteBatch.Draw(_texture, position, null, _color, rotation, _origin, scale, SpriteEffects.None, 0);
        }

        public void LoadContent(ContentManager content, string texture)
        {
            _texture = content.Load<Texture2D>(texture);
        }

        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
        }

        public void SetOriginCenter()
        {
            SetOrigin(new Vector2(_texture.Width / 2, _texture.Height / 2));
        }
    }
}
