using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mx2L.MonoDebugUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Entity
{
    public class FollowPathEnemy_Test : Enemy
    {
        public FollowPathEnemy_Test(EnemyType enemyType, Vector2 position) : base(enemyType, position)
        {
        }

        public override void Draw()
        {
            base.Draw();
            if (_waypoint is not null)
                Primitives2D.DrawLine(GameWorld._spriteBatch, Position, _waypoint.Position, Color.Red, 1);
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        private new void Move(GameTime gameTime)
        {
            if (_waypoint is not null)
                MoveToWaypoint(gameTime);
        }

        public void AddToDebugInfo()
        {
            DebugInfo.AddString("destination", DebugGetDestination);
            DebugInfo.AddString("distanceTravelled", DebugGetDistanceTravelled);
        }

        public string DebugGetDestination()
        {
            if (_waypoint is null)
                return "";
            return _waypoint.Position.ToString();
        }

        public string DebugGetDistanceTravelled()
        {
            return ((int)DistanceTraveled).ToString();
        }
    }
}
