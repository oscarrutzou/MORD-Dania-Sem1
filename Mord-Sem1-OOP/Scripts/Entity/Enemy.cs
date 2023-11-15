using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;

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
        private int damage;
        private float distanceTraveled;
        private int moneyOnDeath;
        public int Health { get => health; set => health = value; }
        public int Damage { get => damage; }
        public float DistanceTraveled { get => distanceTraveled; set => distanceTraveled = value; }
        protected Waypoint _waypoint;
        #endregion

        /// <summary>
        /// Main constructor for the enemy.
        /// </summary>
        /// <param name="enemyType">This type defines the speed, health and texture of the enemy.</param>
        public Enemy(EnemyType enemyType, Vector2 position)
        {
            //This is to be replaced by a direction towards a waypoint
            //direction = new Vector2(1, 0);

            
            this.enemyType = enemyType;
            Texture2D texture;
            switch (enemyType)
            {
                case EnemyType.Normal:
                    Speed = 50;
                    Health = 100;
                    damage = 10;
                    moneyOnDeath = 10;
                    texture = GlobalTextures.Textures[TextureNames.Enemy_Black1];
                    break;

                case EnemyType.Fast:
                    Speed = 100;
                    Health = 50;
                    damage = 7;
                    moneyOnDeath = 5;
                    texture = GlobalTextures.Textures[TextureNames.Enemy_Green1];
                    break;

                case EnemyType.Strong:
                    Speed = 30;
                    Health = 200;
                    damage = 23;
                    moneyOnDeath = 20;
                    texture = GlobalTextures.Textures[TextureNames.Enemy_Red1];
                    break;

                default:
                    Speed = 50;
                    Health = 100;
                    damage = 10;
                    moneyOnDeath = 10;
                    texture = GlobalTextures.Textures[TextureNames.Enemy_Black1];
                    break;
            }

            Sprite = new Sprite(texture);
            Position = position;
            Scale = 1;
            direction = new Vector2(1, 0);
        }

        public Enemy(EnemyType enemyType, Vector2 position, Waypoint waypoint) : this(enemyType, position)
        {
            _waypoint = waypoint;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DistanceTraveled += Speed * deltaTime;

            MoveToWaypoint(gameTime);
            //Move(gameTime);
        }

        /// <summary>
        /// Called in OnCollision, by objects that can damage the enemy.
        /// </summary>
        /// <param name="damage">The amount of health the enemy will lose.</param>
        public void TakeDamage(int damage)
        {
            Health -= damage;

            //Enemy is dead
            if (Health <= 0)
            {
                IsRemoved = true;
                Global.activeScene.sceneData.sceneStats.money += moneyOnDeath;
                Global.activeScene.sceneData.sceneStats.killCount++;
            }
        }

        public void SetDestination(Waypoint waypoint)
        {
            _waypoint = waypoint;
        }

        /// <summary>
        /// Moves this GameObject in the direction towards the current targeted waypoint.
        /// </summary>
        /// <returns>Returns true if this GameObject has arrived at the waypoint position.</returns>
        protected bool MoveToWaypoint(GameTime gameTime)
        {
            if (_waypoint is null) return true;

            bool arrivedAtWaypoint = AlternativeMove(_waypoint.Position, gameTime, out float distanceTravelled);
            DistanceTraveled += distanceTravelled;

            if (!arrivedAtWaypoint) return false;

            _waypoint.Arrived(this);
            _waypoint.GetNextWaypoint(out _waypoint);

            return true;
        }
    }
}
