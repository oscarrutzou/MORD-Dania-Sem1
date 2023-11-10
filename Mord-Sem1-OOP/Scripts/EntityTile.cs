using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    internal class EntityTile : Tile
    {
        GameObject _gameObject;

        public EntityTile(GameObject gameObject, int x, int y, Vector2 position) : base(new Vector2Int(x, y), position)
        {
            _gameObject = gameObject;
        }
        public EntityTile(GameObject gameObject, Vector2Int gridPosition, Vector2 position) : base(gridPosition, position)
        {
            _gameObject = gameObject;
        }
    }
}
