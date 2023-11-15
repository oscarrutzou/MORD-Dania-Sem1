using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP;
using MordSem1OOP.Scripts.Interface;
using System;

namespace Spaceship.Scripts
{
    internal class SpriteSheet : ISprite
    {
        private Texture2D _sheet;
        private int _index;
        private int _spriteCount;
        private Rectangle _dimension;
        private Vector2 _origin;
        private Color _color = Color.White;
        public float _rotation;


        public int SetIndex { set { SetIndex = Math.Clamp(value, 0, _spriteCount); } }

        public Rectangle Rectangle
        {
            get
            {
                return _dimension;
            }
        }

        public int Width => _dimension.Width;
        public int Height => _dimension.Height;
        public float Rotation { get => _rotation; set { _rotation = value; } }
        public Vector2 Origin => _origin;
        public Color Color { get => _color; set => _color = value; }

        public SpriteSheet(Texture2D sheet, int spriteCount, bool setOriginCenter)
        {
            _sheet = sheet;
            _spriteCount = spriteCount;
            _dimension = new Rectangle();
            _dimension.Width = _sheet.Width;
            _dimension.Height = _sheet.Height / _spriteCount * (_index + 1);
            if (setOriginCenter)
                SetOriginCenter();
        }

        public void Draw(Vector2 position, float rotation, float scale)
        {
            Rectangle dimension = _dimension;
            dimension.Y = _index * _dimension.Height;
            GameWorld._spriteBatch.Draw(_sheet, position, dimension, _color, rotation + Rotation, _origin, scale, SpriteEffects.None, 0);
        }

        public void DrawIndex(int index, Vector2 position, float rotation, float scale)
        {
            Rectangle dimension = _dimension;
            dimension.Y = index * _dimension.Height;
            GameWorld._spriteBatch.Draw(_sheet, position, dimension, _color, rotation + Rotation, _origin, scale, SpriteEffects.None, 0);
        }

        public void NextIndex()
        {
            _index++;
            if (_index > _spriteCount - 1)
                _index = 0;
        }

        public void SetOrigin(Vector2 origin)
        {
            _origin = origin;
        }

        public void SetOriginCenter()
        {
            SetOrigin(new Vector2(_sheet.Width / 2, _dimension.Height / 2));
        }

        public Vector2 GetOrigin()
        {
            return _origin;
        }
    }
}
