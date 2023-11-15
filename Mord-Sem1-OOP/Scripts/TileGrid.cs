using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts.Interface;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    public class TileGrid
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
            gridPosition = GetTileGridPosition(point);

            if (!IsTilePositionInsideGrid(gridPosition))
                return false;

            // gridPosition = new Vector2Int((int)MathF.Floor(point.X), (int)MathF.Floor(point.Y));
            return !GetTile(gridPosition, out _);
        }

        /// <summary>
        /// Gets the tile position of point. Note that the tile position can be out of bounds.
        /// </summary>
        /// <returns>Column and row position</returns>
        public Vector2Int GetTileGridPosition(Vector2 point)
        {
            point -= Position;
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

        public void RemoveTile(Vector2Int gridPosition)
        {
            _tiles[gridPosition.X, gridPosition.Y] = null;
        }

        public void DrawGrid(SpriteBatch spriteBatch)
        {
            Primitives2D.DrawRectangle(spriteBatch, Dimension, Color.Red, 1, 0);

            for (int i = 0; i < ColumnCount; i++)
            {
                float xPos = i * TileSize + Position.X;
                Vector2 top = new Vector2(xPos, Dimension.Top);
                Vector2 bottom = new Vector2(xPos, Dimension.Bottom);
                Primitives2D.DrawLine(spriteBatch, top, bottom, Color.Red, 1);
            }

            for (int i = 0; i < RowCount; i++)
            {
                float yPos = i * TileSize + Position.Y;
                Vector2 left = new Vector2(Dimension.Left, yPos);
                Vector2 right = new Vector2(Dimension.Right, yPos);
                Primitives2D.DrawLine(spriteBatch, left, right, Color.Red, 1);
            }
        }

        public void DrawPlacements(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < ColumnCount; x++)
            {
                for (int y = 0; y < RowCount; y++)
                {
                    if (!GetTile(new Vector2Int(x, y), out Tile tile))
                        continue;

                    if (tile is EnviromentTile)
                    {
                        EnviromentTile enviromentTile = (EnviromentTile)tile;

                        Color color = Color.Gray;

                        if (enviromentTile.Type == EnviromentTile.TileType.Path)
                            color = Color.DarkRed;

                        if (enviromentTile.Type == EnviromentTile.TileType.Blocked)
                            color = new Color(57, 57, 57);

                        Rectangle tileRect = new Rectangle(
                            (int)(tile.Position.X - TileSize / 4),
                            (int)(tile.Position.Y - TileSize / 4),
                            (int)(TileSize / 2),
                            (int)(TileSize / 2)
                        );

                        Primitives2D.DrawSolidRectangle(spriteBatch, tileRect, 0, color);
                    }
                }
            }
        }

        public void InsertFill(Tower tower, Rectangle rectangle)
        {
            InsertFill(tower, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void InsertFill(Tower tower, Vector2Int gridPositionStart, Vector2Int gridPositionEnd)
        {
            int x0 = gridPositionStart.X;
            int y0 = gridPositionStart.Y;
            int x1 = gridPositionEnd.X;
            int y1 = gridPositionEnd.Y;

            if (gridPositionStart.X < gridPositionEnd.X)
            {
                x0 = gridPositionEnd.X;
                x1 = gridPositionStart.X;
            }

            if (gridPositionStart.Y < gridPositionEnd.Y)
            {
                y0 = gridPositionEnd.Y;
                y1 = gridPositionStart.Y;
            }

            InsertFill(tower, x0, y0, x1, y1);
        }

        public void InsertFill(Tower tower, int x0, int y0, int x1, int y1)
        {
            for (int x = x0; x < x1; x++)
            {
                for (int y = y0; y < y1; y++)
                {
                    Insert(tower, x, y);
                }
            }
        }

        public void InsertFill(EnviromentTile.TileType enviromentTile, Rectangle rectangle)
        {
            InsertFill(enviromentTile, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void InsertFill(EnviromentTile.TileType enviromentTile, Vector2Int gridPositionStart, Vector2Int gridPositionEnd)
        {
            InsertFill(enviromentTile, gridPositionStart.X, gridPositionStart.Y, gridPositionEnd.X, gridPositionEnd.Y);
        }

        public void InsertFill(EnviromentTile.TileType enviromentTile, int x0, int y0, int x1, int y1)
        {
            if (x0 > x1)
            {
                int temp = x0;
                x0 = x1;
                x1 = temp;
            }

            if (y0 > y1)
            {
                int temp = y0;
                y0 = y1;
                y1 = temp;
            }

            for (int x = x0; x < x1 + 1; x++)
            {
                for (int y = y0; y < y1 + 1; y++)
                {
                    Insert(enviromentTile, x, y);
                }
            }
        }
    }
}
