using Microsoft.Xna.Framework;
using Mx2L.MonoDebugUI;

namespace MordSem1OOP.Scripts
{
    public class Waypoint
    {
        private Waypoint _nextWaypoint;
        private Vector2 _position;
        private Vector2Int _gridPosition;

        public Vector2 Position { get => _position; }
        public Vector2 NextWaypointPosition
        {
            get
            {
                if (_nextWaypoint is null)
                    return Position;

                return _nextWaypoint.Position;
            }
        }
        public Vector2Int Vector2Int { get => _gridPosition; }

        public Waypoint(Vector2 position, Vector2Int gridPosition)
        {
            _position = position;
            _gridPosition = gridPosition;
        }

        public void SetNextWaypoint(Waypoint waypoint)
        {
            _nextWaypoint = waypoint;
        }

        public bool GetNextWaypoint(out Waypoint waypoint)
        {
            if (_nextWaypoint is null)
            {
                waypoint = null;
                return false;
            }

            waypoint = _nextWaypoint;
            return true;
        }

        public virtual void Arrived(Enemy enemy) {}
    }
}
