using Microsoft.Xna.Framework;

namespace MordSem1OOP.Scripts
{
    internal class Tile
    {
        private Vector2Int _gridPosition;
        private Vector2 _position;

        public Vector2Int GridPosition { get { return _gridPosition; } }
        public Vector2 Position { get { return _position; } }

        public Tile(int x, int y, Vector2 position)
        {
            _gridPosition = new Vector2Int(x, y);
            _position = position;
        }

        public Tile(Vector2Int gridPosition, Vector2 position)
        {
            _gridPosition = gridPosition;
            _position = position;
        }
    }
}
