using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using MordSem1OOP.Scripts.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.SceneScripts
{
    internal sealed class TileGridTest : Scene
    {
        private TileGrid _tileGrid;
        private BuildGui _buildGui;
        public TileGridTest(ContentManager content) : base(content) { }

        public override void Initialize()
        {
            _tileGrid = new TileGrid(Vector2.Zero, 40, 18, 10);
            _buildGui = new BuildGui(_tileGrid);

            int x = 0;
            int y = 3;

            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(x, y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(x, ++y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(x, ++y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(x, ++y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));
            _tileGrid.Insert(EnviromentTile.TileType.Path, new Vector2Int(++x, y));

            x = 0;
            y = 4;


            Tower CreateTower()
            {
                Tower acherTower = new Tower(.5f, GlobalTextures.Textures[TextureNames.Tower_Archer]);
                GameWorld.Instantiate(acherTower);
                return acherTower;
            }

            Enemy enemy1 = new Enemy(EnemyType.Strong, new Vector2(0, 100));
            Global.activeScene.sceneData.gameObjects.Add(enemy1);

            _tileGrid.Insert(CreateTower(), new Vector2Int(x, y));

            _tileGrid.Insert(CreateTower(), new Vector2Int(++x, y));
            _tileGrid.Insert(CreateTower(), new Vector2Int(++x, y));
            x++;
            y++;
            _tileGrid.Insert(CreateTower(), new Vector2Int(x, ++y));
            _tileGrid.Insert(CreateTower(), new Vector2Int(x, ++y));
            _tileGrid.Insert(CreateTower(), new Vector2Int(++x, y));
            x++;
            _tileGrid.Insert(CreateTower(), new Vector2Int(++x, y));
            _tileGrid.Insert(CreateTower(), new Vector2Int(++x, y));
        }

        public override void Update(GameTime gameTime)
        {
            _buildGui.Update();
            base.Update(gameTime); //Handles the GameObject list
        }

        public override void Draw(){
            for (int x = 0; x < _tileGrid.ColumnCount; x++)
            {
                for (int y = 0; y < _tileGrid.RowCount; y++)
                {
                    if (!_tileGrid.GetTile(new Vector2Int(x, y), out Tile tile))
                        continue;

                    if (tile is EnviromentTile)
                    {
                        Color color = Color.Gray;

                        Rectangle tileRect = new Rectangle(
                            (int)(tile.Position.X - _tileGrid.TileSize / 4),
                            (int)(tile.Position.Y - _tileGrid.TileSize / 4),
                            (int)(_tileGrid.TileSize / 2),
                            (int)(_tileGrid.TileSize / 2)
                        );

                        Primitives2D.DrawSolidRectangle(GameWorld._spriteBatch, tileRect, 0, color);
                    }
                }
            }

            // Primitives2D.DrawRectangle(spriteBatch, new Rectangle(0,0,40,40), 0, Color.Blue);
            // Primitives2D.DrawRectangle(spriteBatch, new Rectangle(_tileGrid.Dimension.X, _tileGrid.Dimension.Y, 40, 40), 0, Color.Blue);


            Primitives2D.DrawRectangle(GameWorld._spriteBatch, Vector2.Zero, _tileGrid.Dimension, Color.Red, 1, 0);

            for (int i = 0; i < _tileGrid.ColumnCount; i++)
            {
                float xPos = i * _tileGrid.TileSize;
                Vector2 top = new Vector2(xPos, _tileGrid.Dimension.Top);
                Vector2 bottom = new Vector2(xPos, _tileGrid.Dimension.Bottom);
                Primitives2D.DrawLine(GameWorld._spriteBatch, top, bottom, Color.Red, 1);
            }

            for (int i = 0; i < _tileGrid.RowCount; i++)
            {
                float yPos = i * _tileGrid.TileSize;
                Vector2 left = new Vector2(_tileGrid.Dimension.Left, yPos);
                Vector2 right = new Vector2(_tileGrid.Dimension.Right, yPos);
                Primitives2D.DrawLine(GameWorld._spriteBatch, left, right, Color.Red, 1);
            }

            _buildGui.Draw();

            base.Draw(); //Draws all elements in the GameObject list
        }
    }
}
