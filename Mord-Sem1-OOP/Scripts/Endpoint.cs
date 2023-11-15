using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    public class Endpoint : Waypoint
    {
        public Endpoint(Vector2 position, Vector2Int gridPosition) : base(position, gridPosition) {}

        public override void Arrived(Enemy enemy)
        {
            Global.activeScene.sceneData.sceneStats.Health -= enemy.Damage;
        }
    }
}
