using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Gui
{
    internal class BuildGui : Gui
    {
        private int _selectionIndex;
        private TileGrid _tileGrid;

        private int tempCost;

        public BuildGui(TileGrid tileGrid)
        {
            _tileGrid = tileGrid;
        }

        public override void Update()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                Build();
        }

        public void Build()
        {
            _selectionIndex = 0;

            Vector2 position = InputManager.mousePosition;


            if (!_tileGrid.IsTileAvailable(position, out Vector2Int gridPosition))
                return;

            Tower tower = GetTower(_selectionIndex);

            if (tower is null)
                return;

            SceneData tempSceneData = Global.activeScene.sceneData;
            tempSceneData.gameObjects.Add(tower);
            tempSceneData.towers.Add(tower);

            _tileGrid.Insert(tower, gridPosition);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = InputManager.mousePosition;

            if (!_tileGrid.IsTileAvailable(position, out Vector2Int gridPosition))
                return;

            Rectangle tileRect = new Rectangle(
                            (int)(gridPosition.X * _tileGrid.TileSize + _tileGrid.TileSize / 4),
                            (int)(gridPosition.Y * _tileGrid.TileSize + _tileGrid.TileSize / 4),
                            (int)(_tileGrid.TileSize / 2),
                            (int)(_tileGrid.TileSize / 2)
                        );

            Primitives2D.DrawSolidRectangle(spriteBatch, tileRect, 0, Color.Red);
        }

        private Tower GetTower(int index)
        {
            switch (index)
            {
                case 0: return CreateTower();
            }
            return null;
        }

        private Tower CreateTower()
        {
            Tower acherTower = new Tower(.5f, 300f, GlobalTextures.Textures[TextureNames.Tower_Arrow]);
            return acherTower;
        }
    }
}
