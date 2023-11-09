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
    public class FollowPathEnemy : Enemy
    {
        public FollowPathEnemy(EnemyType enemyType, Vector2 position) : base(enemyType, position)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (_waypoint is not null)
                Primitives2D.DrawLine(spriteBatch, Position, _waypoint.Position, Color.Magenta, 1);
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            if (_waypoint is not null)
                MoveToWaypoint(gameTime);
        }

        public void AddToDebugInfo()
        {
            DebugInfo.AddString("destination", DebugGetDestination);
        }

        public string DebugGetDestination()
        {
            return _waypoint.Position.ToString();
        }
    }
}
