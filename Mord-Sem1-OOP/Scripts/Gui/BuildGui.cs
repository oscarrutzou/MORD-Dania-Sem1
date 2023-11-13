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
        private Tower _selectedTower;
        public BuildGui(TileGrid tileGrid)
        {
            _tileGrid = tileGrid;
        }

        public override void Update()
        {
            _selectedTower = GetTower(_selectionIndex);
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

            

            if (_selectedTower is null)
                return;

            if (!CanAffordTower()) return;

            _selectedTower.towerData.BuyTower(); //Deducts the money from the sceneStats
            GameWorld.Instantiate(_selectedTower);
            _tileGrid.Insert(_selectedTower, gridPosition);


        }

        public override void Draw()
        {
            Vector2 position = InputManager.mousePosition;

            bool canPlaceOnGrid = _tileGrid.IsTileAvailable(position, out Vector2Int gridPosition);

            if (!canPlaceOnGrid)
                return;

            Rectangle tileRect = new Rectangle(
                            (int)(gridPosition.X * _tileGrid.TileSize + _tileGrid.TileSize / 4),
                            (int)(gridPosition.Y * _tileGrid.TileSize + _tileGrid.TileSize / 4),
                            (int)(_tileGrid.TileSize / 2),
                            (int)(_tileGrid.TileSize / 2)
                        );


            Primitives2D.DrawSolidRectangle(GameWorld._spriteBatch, tileRect, 0, CanAffordTower() ? Color.Green: Color.Red);
        }

        public bool CanAffordTower()
        {
            return Global.activeScene.sceneData.sceneStats.money >= _selectedTower.towerData.CalculateBuyAmount();
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
            Tower archerTower = new Tower(.5f, GlobalTextures.Textures[TextureNames.Tower_Archer]);
            return archerTower;
        }
    }
}
