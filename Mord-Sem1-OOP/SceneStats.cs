﻿using Microsoft.Xna.Framework;
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
        public int money = 600;
        public int killCount;
        public int Health
        {
            get { return _health; }
            set {
                _health = value;
                if (_health < 0)
                    _health = 0;
                if (_health == 0)
                {
                    // DEATH!!
                }
            }
        }
    }
}
