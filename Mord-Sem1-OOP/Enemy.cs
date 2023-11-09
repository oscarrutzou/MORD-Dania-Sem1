using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
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
        #region Fields & Properties
        private EnemyType enemyType;
        private int health;
        private float distanceTraveled;
        public int Health { get => health; set => health = value; }
        public float DistanceTraveled { get => distanceTraveled; set => distanceTraveled = value; }
        

        protected Waypoint _waypoint;
        #endregion

        /// <summary>
        /// Main constructor for the enemy
        /// </summary>
        /// <param name="enemyType"></param>
        public Enemy(EnemyType enemyType, Vector2 position, ContentManager content)
        {
            //This is to be replaced by a direction towards a waypoint
            direction = new Vector2(1, 0);


            this.enemyType = enemyType;
            string enemyName;
            switch (enemyType)
            {
                case EnemyType.Normal:
                    Speed = 50;
                    Health = 10;
                    enemyName = "Placeholder\\Enemies\\enemyBlack1";
                    break;

                case EnemyType.Fast:
                    Speed = 100;
                    Health = 5;
                    enemyName = "Placeholder\\Enemies\\enemyBlue1";
                    break;

                case EnemyType.Strong:
                    Speed = 30;
                    Health = 20;
                    enemyName = "Placeholder\\Enemies\\enemyGreen1";
                    break;

                default:
                    Speed = 50;
                    Health = 10;
                    enemyName = "Placeholder\\Enemies\\enemyRed1";
                    break;
            }


            //Assign sprites
            Sprite = new Scripts.Sprite(content, enemyName);

            Position = position;
            Scale = 1;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DistanceTraveled += Speed * deltaTime;

            Move(gameTime);
            //TODO: Set target to be the next waypoint
        }

        public void SetDestination(Waypoint waypoint)
        {
            _waypoint = waypoint;
        }

        protected void MoveToWaypoint(GameTime gameTime)
        {
            if (_waypoint is null)
                return;

            if (!AlternativeMove(_waypoint.Position, gameTime))
                return;

            _waypoint.Arrived();

            if (_waypoint.GetNextWaypoint(out Waypoint waypoint))
                _waypoint = waypoint;
        }
    }
}
