﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public enum EnemyType : byte
    {
        Normal = 0,
        Fast = 1,
        Strong = 2,
    }

    public class Enemy : GameObject
    {
        #region Fields
        private int health;
        EnemyType enemyType;
        private Vector2[] waypoints;
        private int currentTargetPoint = 0;
        #endregion

        public int Health { get => health; set => health = value; }

        /// <summary>
        /// Initializer a enemy with speed and health
        /// </summary>
        /// <param name="enemyType"></param>
        public Enemy(EnemyType enemyType, ContentManager content)
        {
            this.enemyType = enemyType;
            string enemyName;
            switch (enemyType)
            {
                case EnemyType.Normal: 
                    speed = 5; 
                    Health = 10; 
                    enemyName = "Placeholder\\Enemies\\enemyBlack1"; 
                    break;

                case EnemyType.Fast: 
                    speed = 10; 
                    Health = 5;
                    enemyName = "Placeholder\\Enemies\\enemyBlue1";
                    break;

                case EnemyType.Strong: 
                    speed = 3; 
                    Health = 20;
                    enemyName = "Placeholder\\Enemies\\enemyGreen1";
                    break;

                default: 
                    speed = 5; 
                    Health = 10;
                    enemyName = "Placeholder\\Enemies\\enemyRed1";
                    break;  
            }


            //Assign sprites
            Sprite = new Scripts.Sprite(content, enemyName);


        }

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            Position = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            //if(Position == waypoints[currentTargetPoint])
            //{
            //    currentTargetPoint++;
            //}
        }
        #endregion
    }
}
