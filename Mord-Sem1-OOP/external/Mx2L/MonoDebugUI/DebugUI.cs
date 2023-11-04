// MonoDebugUI made by Mx2L, aka. Michael M. Lukassen.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Mx2L.MonoDebugUI
{
    public class DebugUI
    {
        private Vector2 _position;
        private SpriteFont _font;
        private int _rowSpacing;
        private Color _color;
        private List<string> _identifiers;

        public DebugUI(Vector2 position, SpriteFont font, int rowSpacing, Color color, params string[] identifiers)
        : this(position, font, rowSpacing, color, identifiers.ToList()) { }
        public DebugUI(Vector2 position, SpriteFont font, int rowSpacing, Color color, IEnumerable<string> identifiers)
        {
            _position = position;
            _font = font;
            _rowSpacing = rowSpacing;
            _color = color;
            _identifiers = identifiers is List<string> ? (List<string>)identifiers : identifiers.ToList();
        }

        public void DrawAll(SpriteBatch spriteBatch)
        { DrawAll(spriteBatch, _rowSpacing, _color); }

        public void DrawAll(SpriteBatch spriteBatch, Color color)
        { DrawAll(spriteBatch, _rowSpacing, color); }

        public void DrawAll(SpriteBatch spriteBatch, int rowSpacing)
        { DrawAll(spriteBatch, rowSpacing, _color); }

        public void DrawAll(SpriteBatch spriteBatch, int rowSpacing, Color color)
        {
            DebugInfo.DrawInfo(spriteBatch, _position, rowSpacing, _font, color, _identifiers);
        }

        public void Add(string identifier)
        { _identifiers.Add(identifier); }

        public void Remove(string identier)
        { _identifiers.Remove(identier); }
    }
}
