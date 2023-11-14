﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class TileGridTest : Scene
    {

        public TileGridTest(ContentManager content) : base(content) { }

        public override void Initialize()
        {
            sceneData._tileGrid = new TileGrid(Vector2.Zero, 40, 18, 10);
            sceneData._buildGui = new BuildGui(sceneData._tileGrid);
            sceneData._statsGui = new StatsGui();

            int x = 0;
            int y = 3;

            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(x, y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(x, ++y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(x, ++y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(x, ++y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            sceneData._tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));


            
        }

        public override void Update(GameTime gameTime)
        {
            sceneData._buildGui.Update(gameTime);
            base.Update(gameTime); //Handles the GameObject list
        }

        /// <summary>
        /// DrawTileGrid
        /// </summary>
        public override void Draw()
        {
            for (int x = 0; x < sceneData._tileGrid.ColumnCount; x++)
            {
                for (int y = 0; y < sceneData._tileGrid.RowCount; y++)
                {
                    if (!sceneData._tileGrid.GetTile(new Vector2Int(x, y), out Tile tile))
                        continue;

                    if (tile is EnviromentTile)
                    {
                        Color color = Color.Gray;

                        Rectangle tileRect = new Rectangle(
                            (int)(tile.Position.X - sceneData._tileGrid.TileSize / 4),
                            (int)(tile.Position.Y - sceneData._tileGrid.TileSize / 4),
                            (int)(sceneData._tileGrid.TileSize / 2),
                            (int)(sceneData._tileGrid.TileSize / 2)
                        );

                        Primitives2D.DrawSolidRectangle(GameWorld._spriteBatch, tileRect, 0, color);
                    }
                }
            }

            Primitives2D.DrawRectangle(GameWorld._spriteBatch, Vector2.Zero, sceneData._tileGrid.Dimension, Color.Red, 1, 0);

            for (int i = 0; i < sceneData._tileGrid.ColumnCount; i++)
            {
                float xPos = i * sceneData._tileGrid.TileSize;
                Vector2 top = new Vector2(xPos, sceneData._tileGrid.Dimension.Top);
                Vector2 bottom = new Vector2(xPos, sceneData._tileGrid.Dimension.Bottom);
                Primitives2D.DrawLine(GameWorld._spriteBatch, top, bottom, Color.Red, 1);
            }

            for (int i = 0; i < sceneData._tileGrid.RowCount; i++)
            {
                float yPos = i * sceneData._tileGrid.TileSize;
                Vector2 left = new Vector2(sceneData._tileGrid.Dimension.Left, yPos);
                Vector2 right = new Vector2(sceneData._tileGrid.Dimension.Right, yPos);
                Primitives2D.DrawLine(GameWorld._spriteBatch, left, right, Color.Red, 1);
            }

            sceneData._buildGui.Draw();

            base.Draw(); //Draws all elements in the GameObject list
        }



        public override void DrawScene()
        {

            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: Global.gameWorld.Camera.GetMatrix());
            Draw();
            sceneData._statsGui.WorldDraw();
            GameWorld._spriteBatch.End();

            GameWorld._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            sceneData._statsGui.ScreenDraw();
            GameWorld._spriteBatch.End();
        }
    }
}
