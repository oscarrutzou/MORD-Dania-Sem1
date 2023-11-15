using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP;
using MordSem1OOP.Scripts.Interface;
using Mx2L.MonoDebugUI;
using SharpDX.Direct3D9;
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

        //public SpriteSheet(ContentManager content, string loadSheet, int spriteCount)
        //{
        //    LoadContent(content, loadSheet);
        //    _spriteCount = spriteCount;
        //    _dimension = new Rectangle();
        //    _dimension.Width = _sheet.Width;
        //    _dimension.Height = _sheet.Height / _spriteCount * (_index + 1);
        //    _origin = Vector2.Zero;
        //}

        public SpriteSheet(ContentManager content, string loadSheet, int spriteCount, Vector2 origin)
        {
            LoadContent(content, loadSheet);
            _spriteCount = spriteCount;
            _dimension = new Rectangle();
            _dimension.Width = _sheet.Width;
            _dimension.Height = _sheet.Height / _spriteCount * (_index + 1);
            _origin = origin;
        }

        public void LoadContent(ContentManager content, string loadSheet)
        {
            _sheet = content.Load<Texture2D>(loadSheet);
        }

        public void Draw(Vector2 position, float rotation, float scale)
        {
            GameWorld._spriteBatch.Draw(_sheet, position, _dimension, Color.White, rotation + Rotation, _origin, scale, SpriteEffects.None, 0);
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
