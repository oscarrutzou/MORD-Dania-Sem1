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

        public int CalculateBuyAmount()
        {
            return buyAmount * towerLevel;
        }
    }
}
