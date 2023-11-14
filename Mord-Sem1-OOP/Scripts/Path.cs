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
            int size = 5;
            foreach (Waypoint waypoint in _waypoints)
            {
                Rectangle rectangle = new Rectangle((int)waypoint.Position.X - size, (int)waypoint.Position.Y - size, size * 2, size * 2);
                Primitives2D.DrawSolidRectangle(spriteBatch, rectangle, 0, Color.Magenta);
                if (waypoint.GetNextWaypoint(out Waypoint next))
                {
                    Primitives2D.DrawLine(spriteBatch, waypoint.Position, next.Position, Color.Magenta, 1);
                }
            }
        }
    }
}
