using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    internal class WaypointManager
    {
        Waypoint[] _waypoints;

        public WaypointManager(params Waypoint[] waypoints)
        {
            this._waypoints = waypoints;
        }

        public void ConnectWaypoints()
        {
            for (int i = 0; i < _waypoints.Length - 1; i++)
            {
                _waypoints[i].SetNextWaypoint(_waypoints[i + 1]);
            }
        }
    }
}
