using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    internal class WaypointManager
    {
        private Waypoint[] _waypoints;

        public WaypointManager(params Waypoint[] waypoints)
        {
            _waypoints = waypoints;
        }

        public WaypointManager ConnectWaypoints()
        {
            for (int i = 0; i < _waypoints.Length - 1; i++)
            {
                _waypoints[i].SetNextWaypoint(_waypoints[i + 1]);
            }

            return this;
        }

        public Waypoint GetWaypoint(int index)
        {
            return _waypoints[index];
        }
    }
}
