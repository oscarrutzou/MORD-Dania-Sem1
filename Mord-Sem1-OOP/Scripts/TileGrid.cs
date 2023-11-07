using Microsoft.Xna.Framework;
using MordSem1OOP.Scripts.Interface;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    internal class TileGrid
    {
        private Vector2 _position;
        private Tile[,] _tiles;
        private int _columnCount;
        private int _rowCount;
        private float _tileSize;

        private Rectangle Dimension
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _columnCount * (int)_tileSize, _rowCount * (int)_tileSize);
            }
        }

        /// <summary>
        /// Initialize a grid with a position, tile size, column and row counts of tiles.
        /// </summary>
        /// <param name="position">Position of the grid</param>
        /// <param name="tileSize">Size of each square tile</param>
        /// <param name="columnCount">Column count of tiles</param>
        /// <param name="rowCount">Row count of tiles</param>
        public TileGrid(Vector2 position, float tileSize, int columnCount, int rowCount)
        {
            _position = position;
            _tiles = new Tile[columnCount, rowCount];
            _tileSize = tileSize;
            _columnCount = columnCount;
            _rowCount = rowCount;
        }

        /// <summary>
        /// Return a boolean on if a tile exists on the Vector2 position and if is inside the grid. out Tile can be null.
        /// </summary>
        public bool GetTile(Vector2 point, out Tile tile)
        {
            tile = null;

            Vector2Int gridPosition = GetTilePosition(point);

            if (gridPosition.X < 0 || gridPosition.X >= _columnCount)
                return false;

            if (gridPosition.Y < 0 || gridPosition.Y >= _rowCount)
                return false;

            tile = _tiles[gridPosition.X, gridPosition.Y];
            return tile != null;
        }

        /// <summary>
        /// Return a boolean on if a tile exists on the grid position. out Tile can be null.
        /// </summary>
        /// <param name="gridPosition">Column and row position</param>
        public bool GetTile(Vector2Int gridPosition, out Tile tile)
        {
            tile = null;

            if (!IsTilePositionInsideGrid(gridPosition))
                return false;

            tile = _tiles[gridPosition.X, gridPosition.Y];
            return tile != null;
        }

        /// <summary>
        /// Checks if a tile position is inside grid.
        /// </summary>
        /// <param name="gridPosition">Column and row position</param>
        public bool IsTilePositionInsideGrid(Vector2Int gridPosition)
        {
            if (gridPosition.X < 0 || gridPosition.X >= _columnCount)
                return false;

            if (gridPosition.Y < 0 || gridPosition.Y >= _rowCount)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if a tile position is empty and inside the grid.
        /// </summary>
        /// <param name="gridPosition">Column and row position</param>
        public bool IsTileAvailable(Vector2 point, out Vector2Int gridPosition)
        {
            gridPosition = new Vector2Int((int)MathF.Floor(point.X), (int)MathF.Floor(point.Y));
            return !GetTile(gridPosition, out _);
        }

        /// <summary>
        /// Gets the tile position of point. Note that the tile position can be out of bounds.
        /// </summary>
        /// <returns>Column and row position</returns>
        public Vector2Int GetTilePosition(Vector2 point)
        {
            point += _position;
            point /= _tileSize;

            return new Vector2Int((int)MathF.Floor(point.X), (int)MathF.Floor(point.Y));
        }

        /// <summary>
        /// Inserts a tile or replace existing one.
        /// </summary>
        /// <param name="gridPosition">Column and row position</param>
        public bool InsertTile(Tile tile, Vector2Int gridPosition)
        {
            if (!IsTilePositionInsideGrid(gridPosition))
                return false;

            _tiles[gridPosition.X, gridPosition.Y] = tile;
            return true;
        }
    }
}
