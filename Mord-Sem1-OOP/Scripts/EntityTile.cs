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
        private GameObject _gameObject;

        public GameObject GameObject { get => _gameObject; set => _gameObject = value; }

        public EntityTile(GameObject gameObject, int x, int y, Vector2 position) : base(new Vector2Int(x, y), position)
        {
            GameObject = gameObject;
        }
        public EntityTile(GameObject gameObject, Vector2Int gridPosition, Vector2 position) : base(gridPosition, position)
        {
            GameObject = gameObject;
        }
    }
}
