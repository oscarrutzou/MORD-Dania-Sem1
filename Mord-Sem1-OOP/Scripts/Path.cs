using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    public class Path
    {
        private Waypoint[] _waypoints;

        public Path(params Waypoint[] waypoints)
        {
            _waypoints = waypoints;
        }

        public Path ConnectWaypoints()
        {
            for (int i = 0; i < _waypoints.Length - 1; i++)
            {
                _waypoints[i].SetNextWaypoint(_waypoints[i + 1]);
            }

            return this;
        }

        public Path ConnectWaypoints(int waypoint, int nextWaypoint)
        {
            _waypoints[waypoint].SetNextWaypoint(_waypoints[nextWaypoint]);
            return this;
        }

        public Waypoint GetWaypoint(int index)
        {
            return _waypoints[index];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _waypoints.Length - 1; i++)
            {
                Primitives2D.DrawLine(spriteBatch, _waypoints[i].Position, _waypoints[i + 1].Position, Color.Magenta, 1);
            }
        }
    }
}
