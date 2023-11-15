using Microsoft.Xna.Framework;
using MordSem1OOP.SceneScripts;
using MordSem1OOP.Scripts.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    /// <summary>
    /// This class is to proplerly divivide the things that should be drawn to the player like the players money
    /// </summary>
    public class SceneStats
    {
        public int maxHealth = 100;
        private int _health = 100;
        public int money = 1000;
        public int killCount;
        //TODO point (bruges af animated counter) Enemy switch case
        private int score;

        public int Health
        {
            get { return _health; }
            set {
                _health = value;
                if (_health < 0)
                    _health = 0;
                if (_health == 0)
                {
                    Global.gameWorld.activeScene = 5;
                    Global.activeScene = GameWorld.scenes[5];
                    GameWorld.scenes[5].Initialize();
                }
             
            }
        }

        public int Score 
        { 
            get => score; 
            set 
            {
                if(score < 1000000)
                score = value;
                else { score = 999999; }

            } 
        }
    }
}
