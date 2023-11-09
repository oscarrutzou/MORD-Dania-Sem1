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
            SceneData tempSceneData = Global.activeScene.sceneData;

            _path = new Path(
                new Waypoint(new Vector2(50, 50), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(100, 50), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(100, 150), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(300, 150), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(300, 75), new Vector2Int(1, 1)),
                new Waypoint(new Vector2(500, 75), new Vector2Int(1, 1))
                );

            _path.ConnectWaypoints();
            currWaypoint = _path.GetWaypoint(0);

            FollowPathEnemy fpEnemy = new FollowPathEnemy(EnemyType.Fast, currWaypoint.Position, content);
            fpEnemy.SetDestination(currWaypoint);
            fpEnemy.AddToDebugInfo();
            DebugInfo.AddCount("arrival");

            tempSceneData.gameObjects.Add(fpEnemy);
            tempSceneData.enemies.Add(fpEnemy);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); //Handles the GameObject list

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); //Draws all elements in the GameObject list
        }
    }
}
