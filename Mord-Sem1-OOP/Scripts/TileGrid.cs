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
        #region Fields
        private Vector2 _position;
        private Tile[,] _tiles;
        private int _columnCount;
        private int _rowCount;
        private float _tileSize;
        #endregion

        /// <summary>
        /// Dimension of the grid
        /// </summary>
        public Rectangle Dimension
        {
            get
            {
                return new Rectangle(
                    (int)_position.X, 
                    (int)_position.Y, 
                    _columnCount * (int)_tileSize, 
                    _rowCount * (int)_tileSize);
            }
        }
        
        public Vector2 Position { get { return _position; } }
        public int ColumnCount { get { return _columnCount; } }
        public int RowCount { get { return _rowCount; } }
        public float TileSize { get { return _tileSize; } }

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

            Vector2Int gridPosition = GetTileGridPosition(point);

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
        public Vector2Int GetTileGridPosition(Vector2 point)
        {
            point += _position;
            point /= _tileSize;

            return new Vector2Int((int)MathF.Floor(point.X), (int)MathF.Floor(point.Y));
        }

        public Vector2 GetTileWorldPosition(Vector2Int gridPosition)
        {
            Vector2 worldPosition = Vector2.Zero;
            worldPosition.X = gridPosition.X * TileSize + TileSize / 2 + _position.X;
            worldPosition.Y = gridPosition.Y * TileSize + TileSize / 2 + _position.Y;

            return worldPosition;
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

        public bool Insert(Tower tower, int x, int y)
        {
            return Insert(tower, new Vector2Int(x, y), out _);
        }

        public bool Insert(Tower tower, int x, int y, out Tile tile)
        {
            return Insert(tower, new Vector2Int(x, y), out tile);
        }

        public bool Insert(Tower tower, Vector2Int gridPosition)
        {
            return Insert(tower, gridPosition, out _);
        }

        public bool Insert(Tower tower, Vector2Int gridPosition, out Tile tile)
        {
            tile = null;

            if (!IsTilePositionInsideGrid(gridPosition))
                return false;

            Vector2 worldPosition = GetTileWorldPosition(gridPosition);
            tile = new EntityTile(tower, gridPosition, worldPosition);
            tower.Position = worldPosition;

            _tiles[gridPosition.X, gridPosition.Y] = tile;
            return true;
        }

        public bool Insert(EnviromentTile.TileType enviromentTile, int x, int y)
        {
            return Insert(enviromentTile, new Vector2Int(x, y), out _);
        }

        public bool Insert(EnviromentTile.TileType enviromentTile, int x, int y, out Tile tile)
        {
            return Insert(enviromentTile, new Vector2Int(x, y), out tile);
        }

        public bool Insert(EnviromentTile.TileType enviromentTile, Vector2Int gridPosition)
        {
            return Insert(enviromentTile, gridPosition, out _);
        }

        public bool Insert(EnviromentTile.TileType enviromentTile, Vector2Int gridPosition, out Tile tile)
        {
            tile = null;

            if (!IsTilePositionInsideGrid(gridPosition))
                return false;

            Vector2 worldPosition = GetTileWorldPosition(gridPosition);
            tile = new EnviromentTile(enviromentTile, gridPosition, worldPosition);

            _tiles[gridPosition.X, gridPosition.Y] = tile;
            return true;
        }
    }
}
