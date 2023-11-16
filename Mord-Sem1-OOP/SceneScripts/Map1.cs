using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MordSem1OOP.Scripts;
using MordSem1OOP.Scripts.Waves;
using Mx2L.MonoDebugUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class Map1 : Scene
    {
        private Texture2D _map;
        private TileGrid _tileGrid;
        private BuildGui _buildGui;
        private StatsGui _statsGui;
        private Path _path;
        private bool _showDebug;
        private bool _debugBtnDown;

        public Map1(ContentManager content) : base(content)
        {
            _map = content.Load<Texture2D>("Maps/map1");
        }

        public override void Initialize()
        {
            sceneData.tileGrid = _tileGrid = new TileGrid(new Vector2(32, 64), 64, 32, 21);
            sceneData.buildGui = _buildGui = new BuildGui(_tileGrid);
            sceneData.statsGui = _statsGui = new StatsGui();
            BuildMap();
            DebugInfo.AddString("resolution", Global.gameWorld.DebugResolution);
            _buildGui.AddToDebug();

            WaveManager.SetDefaultSpawnPoint(_path.GetWaypoint(0));
            WaveManager.CreateLargeWaves();
            //WaveManager.CreateWaves(); //This method defines how many waves are in the game.
            

            Global.gameWorld.Fullscreen();
            //DebugInfo.AddString("wave", WaveManager.DebugBatchCount);
            //Global.gameWorld.WindowedScreen();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.O))
                _debugBtnDown = true;

            if (_debugBtnDown && Keyboard.GetState().IsKeyUp(Keys.O))
            {
                _debugBtnDown = false;
                _showDebug = !_showDebug;
            }

            _buildGui.Update(gameTime);
            WaveManager.Update(gameTime);
            base.Update(gameTime); //Handles the GameObject list
        }

        public override void Draw()
        {
            GameWorld._spriteBatch.Draw(_map, Vector2.Zero, Color.White);
            if (_showDebug)
            {
                _tileGrid.DrawGrid(GameWorld._spriteBatch);
                _tileGrid.DrawPlacements(GameWorld._spriteBatch);
            }
            _buildGui.Draw();
            base.Draw();
        }

        public override void DrawScene()
        {
            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: Global.gameWorld.Camera.GetMatrix());
            Draw();
            _statsGui.WorldDraw();
            //_path.Draw(GameWorld._spriteBatch);
            //DrawWorldSpaceMouse();
            GameWorld._spriteBatch.End();

            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            _statsGui.ScreenDraw();
            if (_showDebug)
            {
                DebugInfo.DrawAllInfo(GameWorld._spriteBatch, new Vector2(20, 70), 16, GlobalTextures.defaultFont, Color.Magenta);
            }
            //DrawScreenSpaceMouse();
            GameWorld._spriteBatch.End();
        }

        //private void DrawWorldSpaceMouse()
        //{
        //    int size = 5;
        //    Vector2 pos = InputManager.mousePosition;
        //    // Vector2 pos = Global.gameWorld.Camera.ScreenToWorld(InputManager.mousePosition);
        //    Rectangle rectangle = new Rectangle((int)pos.X - size, (int)pos.Y - size, size * 2, size * 2);
        //    Primitives2D.DrawSolidRectangle(GameWorld._spriteBatch, rectangle, 0, Color.Blue);
        //}

        //private void DrawScreenSpaceMouse()
        //{
        //    int size = 5;
        //    Vector2 pos = Mouse.GetState().Position.ToVector2();
        //    // Vector2 pos = Global.gameWorld.Camera.ScreenToWorld(InputManager.mousePosition);
        //    Rectangle rectangle = new Rectangle((int)pos.X - size, (int)pos.Y - size, size * 2, size * 2);
        //    Primitives2D.DrawSolidRectangle(GameWorld._spriteBatch, rectangle, 0, Color.Green);
        //}

        private void BuildMap()
        {
            // Blocked Tiles
            int[] coords = new int[] { 29, 13 , 31, 15, 18, 18, 18, 20, 0, 18, 16, 20, 19, 16, 31, 20, 7, 16, 15, 17, 0, 8, 6, 17, 0, 7, 2, 7, 0, 0, 2, 5, 3, 0, 4, 4, 5, 0, 10, 3, 20, 0, 31, 1 };
            for (int i = 0; i < coords.Length;)
            {
                _tileGrid.InsertFill(EnviromentTile.TileType.Blocked, coords[i++], coords[i++], coords[i++], coords[i++]);
            }

            // Path Tiles
            coords = new int[] { 0, 6, 17, 6, 17, 2, 13, 2, 13, 13, 9, 13, 9, 9, 20, 9, 20, 4, 29, 4, 29, 10, 23, 10, 23, 7, 26, 7, 26, 13, 17, 13, 17, 20 };
            for (int i = 0; i < coords.Length - 2; i += 2)
            {
                _tileGrid.InsertFill(EnviromentTile.TileType.Path, coords[i], coords[i + 1], coords[i + 2], coords[i + 3]);
            }

            // Path / Waypoints
            Waypoint[] waypoints = new Waypoint[coords.Length / 2];

            Vector2Int gridPosition = new Vector2Int(coords[coords.Length - 2], coords[coords.Length - 1]);
            Vector2 worldPosition = _tileGrid.GetTileWorldPosition(gridPosition);
            waypoints[waypoints.Length - 1] = new Endpoint(worldPosition, gridPosition);

            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                gridPosition = new Vector2Int(coords[i * 2], coords[i * 2 + 1]);
                worldPosition = _tileGrid.GetTileWorldPosition(gridPosition);

                waypoints[i] = new Waypoint(worldPosition, gridPosition);
                //_tileGrid.Insert(EnviromentTile.TileType.Blocked, coords[i++], coords[i++]);
            }

            _path = new Path(waypoints);
            _path.ConnectWaypoints();
        }
    }
}
