using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using MordSem1OOP.Scripts.Entity;
using Mx2L.MonoDebugUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class Scene1 : Scene
    {
        Path _path;
        Waypoint currWaypoint;

        public Scene1(ContentManager content) : base(content) { }

        public override void Initialize()
        {

            _path = new Path(
                new Waypoint(new Vector2(50, 50), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(100, 50), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(100, 150), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(300, 150), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(300, 75), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(500, 75), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(500, 250), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(350, 250), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(350, 400), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(600, 400), new Vector2Int(1, 1))
                );

            _path.ConnectWaypoints();
            currWaypoint = _path.GetWaypoint(0);

            FollowPathEnemy_Test fpEnemy = new FollowPathEnemy_Test(EnemyType.Fast, currWaypoint.Position);
            fpEnemy.SetDestination(currWaypoint);
            fpEnemy.AddToDebugInfo();
            DebugInfo.AddCount("waypointsReached");

            GameWorld.Instantiate(fpEnemy);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); //Handles the GameObject list
        }

        public override void Draw()
        {
            _path.Draw(GameWorld._spriteBatch);
            DebugInfo.DrawAllInfo(GameWorld._spriteBatch, Vector2.One * 10, 16, Global.gameWorld.Content.Load<SpriteFont>("Fonts/Arial"), Color.Black);
            base.Draw(); //Draws all elements in the GameObject list
        }
    }
}
