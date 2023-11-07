using Microsoft.Xna.Framework;

namespace MordSem1OOP.Scripts
{
    internal class Tile
    {
        private Vector2Int _gridPosition;
        private Vector2 _position;

        public Vector2Int GridPosition { get { return _gridPosition; } }
        public Vector2 Position { get { return _position; } }

        /// <summary>
        /// Initializes a new instance of Tile, with grid column and row position, and a world position.
        /// </summary>
        /// <param name="x">Grid column position</param>
        /// <param name="y">Grid row position</param>
        /// <param name="position">World position</param>
        public Tile(int x, int y, Vector2 position)
        {
            _gridPosition = new Vector2Int(x, y);
            _position = position;
        }

        /// <summary>
        /// Initializes a new instance of Tile, with grid column and row position, and a world position.
        /// </summary>
        /// <param name="gridPosition">Grid column and row position</param>
        /// <param name="position">World position</param>
        public Tile(Vector2Int gridPosition, Vector2 position)
        {
            _gridPosition = gridPosition;
            _position = position;
        }
    }
}
