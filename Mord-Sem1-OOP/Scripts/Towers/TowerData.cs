using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts.Towers
{
    public class TowerData
    {
        public int towerKills;

        public int towerLevel = 1;
        public int towerMaxLevel = 5;

        public int moneyUsedOnTower;

        private int buyAmount = 100; //Amount in gold
        /// <summary>
        /// Can be used on the screen when displaying the towers data, to see how much it can be sold for.
        /// </summary>
        public int CalculateSellAmount()
        {
            return moneyUsedOnTower / 2;
        }

        /// <summary>
        /// Used to find out how much a tower cost. Also used when lvl up a tower
        /// </summary>
        /// <returns></returns>
        public int CalculateBuyAmount()
        {
            return buyAmount * towerLevel;
        }

        /// <summary>
        /// Deduct the money used from the sceneStats. Use CalculateBuyAmount before buying
        /// </summary>
        public void BuyTower()
        {

            if (Global.activeScene.sceneData.sceneStats.money <= 0) return; //Shouldnt go in minus. Would still build the tower though

            Global.activeScene.sceneData.sceneStats.money -= CalculateBuyAmount();

            moneyUsedOnTower += CalculateBuyAmount();
        }
    }
}
