using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    public class EnviromentTile : Tile
    {
        private TileType _type;

        public TileType Type { get { return _type; } }

        public enum TileType
        {
            Path,
            Blocked
        }

        public EnviromentTile(TileType type, int x, int y, Vector2 position) : base(new Vector2Int(x, y), position)
        {
            _type = type;
        }

        public EnviromentTile(TileType type, Vector2Int gridPosition, Vector2 position) : base(gridPosition, position)
        {
            _type = type;
        }
    }
}
