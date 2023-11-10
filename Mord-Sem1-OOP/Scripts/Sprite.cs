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
        private Vector2 _origin;
        private Color _color = Color.White;

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
        #endregion

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            SetOriginCenter();
        }

        public Sprite(string texture)
        {
            LoadContent(texture);
        }

        #region Methods
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, float scale)
        {
            spriteBatch.Draw(_texture, position, null, _color, rotation, _origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Loader sprite to the object
        /// </summary>
        /// <param name="content"></param>
        /// <param name="texture"></param>
        public void LoadContent(string texture)
        {
            _texture = Global.gameWorld.Content.Load<Texture2D>(texture);
            SetOriginCenter();
        }

        /// <summary>
        /// Sets the orgin point
        /// </summary>
        /// <param name="origin"></param>
        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
        }

        /// <summary>
        /// Sets the orgin to the center of the sprite
        /// </summary>
        public void SetOriginCenter()
        {
            SetOrigin(new Vector2(_texture.Width / 2, _texture.Height / 2));
        }
        #endregion
    }
}
