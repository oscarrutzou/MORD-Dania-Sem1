using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts.Interface;
using System;

namespace MordSem1OOP.Scripts
{
    internal class Sprite : ISprite
    {
        #region Fields
        private Texture2D _texture;
        private float _rotation;
        private Vector2 _origin;
        private Color _color = Color.White;
        private float scale = 1f;

        public Rectangle Rectangle
        {
            get
            {
                Vector2 topLeft = Vector2.Zero - _origin;
                return new Rectangle((int)topLeft.X, (int)topLeft.Y, (int)topLeft.X + Width, (int)topLeft.Y + Height);
            }
        }
        #endregion

        #region Propterties
        public int Width => _texture.Width;
        public int Height => _texture.Height;
        public Vector2 Origin => _origin;
        public Color Color { get => _color; set => _color = value; }
        public float Rotation { get => _rotation; set { _rotation = value; } }
        public float Scale { get => scale; set => scale = value; }
        #endregion

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            SetOriginCenter();
        }

        {
        public Sprite(Texture2D texture, bool orginCenter)
            _texture = texture;
            if (orginCenter)
        }
            }
            {
                SetOriginCenter();
        #region Methods
        public void Draw(Vector2 position, float rotation)
        {
            GameWorld._spriteBatch.Draw(_texture, position, null, _color, rotation, _origin, Scale, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, float rotation, float scale)
        {
            GameWorld._spriteBatch.Draw(_texture, position, null, _color, rotation + _rotation, _origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Sets the origin point of the sprite.
        /// </summary>
        /// <param name="origin"></param>
        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
        }

        /// <summary>
        /// Sets the origin to the center of the sprite.
        /// </summary>
        public void SetOriginCenter()
        {
            SetOrigin(new Vector2(_texture.Width / 2, _texture.Height / 2));
        }
        #endregion
    }
}
