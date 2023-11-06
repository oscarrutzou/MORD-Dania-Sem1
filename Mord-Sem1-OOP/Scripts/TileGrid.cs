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

        public TileGrid(Vector2 position, float tileSize, int columnCount, int rowCount)
        {
            _position = position;
            _tiles = new Tile[columnCount, rowCount];
            _tileSize = tileSize;
            _columnCount = columnCount;
            _rowCount = rowCount;
        }

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

        public bool GetTile(Vector2Int gridPosition, out Tile tile)
        {
            tile = null;

            if (!IsPositionInsideGrid(gridPosition))
                return false;

            tile = _tiles[gridPosition.X, gridPosition.Y];
            return tile != null;
        }

        public bool IsPositionInsideGrid(Vector2Int gridPosition)
        {
            if (gridPosition.X < 0 || gridPosition.X >= _columnCount)
                return false;

            if (gridPosition.Y < 0 || gridPosition.Y >= _rowCount)
                return false;

            return true;
        }

        public bool IsTileAvailable(Vector2 point, out Vector2Int gridPosition)
        {
            gridPosition = new Vector2Int((int)MathF.Floor(point.X), (int)MathF.Floor(point.Y));
            return !GetTile(gridPosition, out _);
        }

        public Vector2Int GetTilePosition(Vector2 point)
        {
            point += _position;
            point /= _tileSize;

            return new Vector2Int((int)MathF.Floor(point.X), (int)MathF.Floor(point.Y));
        }

        public bool InsertTile(Tile tile, Vector2Int gridPosition)
        {
            if (!IsPositionInsideGrid(gridPosition))
                return false;

            _tiles[gridPosition.X, gridPosition.Y] = tile;
            return true;
        }
    }
}
