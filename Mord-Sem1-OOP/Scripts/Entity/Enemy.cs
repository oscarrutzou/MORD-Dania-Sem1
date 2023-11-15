using Microsoft.Xna.Framework;
using MordSem1OOP.Scripts;
using Spaceship.Scripts;
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
        private int score;
        public int Health { get => health; set => health = value; }
        public int Damage { get => damage; }
        public float DistanceTraveled { get => distanceTraveled; set => distanceTraveled = value; }
        protected Waypoint _waypoint;

        private SpriteSheet _spriteSheet;
        private int _spriteLoopIntervalMs;
        private int _timeSinceSpriteLoopMs;
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

            switch (enemyType)
            {
                case EnemyType.Normal:
                    Speed = 50;
                    Health = 100;
                    damage = 10;
                    moneyOnDeath = 10;
                    Sprite = _spriteSheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Enemy_Normal_Sheet], 2, true);
                    Sprite.Rotation = 3.14159f;
                    score = 1111;
                    _spriteLoopIntervalMs = 200;
                    break;

                case EnemyType.Fast:
                    Speed = 100;
                    Health = 50;
                    damage = 7;
                    moneyOnDeath = 5;
                    Sprite = _spriteSheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Enemy_Fast_Sheet], 2, true);
                    Sprite.Rotation = 3.14159f;
                    score = 2222;
                    _spriteLoopIntervalMs = 150;
                    break;

                case EnemyType.Strong:
                    Speed = 30;
                    Health = 200;
                    damage = 23;
                    moneyOnDeath = 20;
                    Sprite = _spriteSheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Enemy_Strong_Sheet], 2, true);
                    Sprite.Rotation = 3.14159f;
                    score = 3333;
                    _spriteLoopIntervalMs = 350;
                    break;

                default:
                    Speed = 50;
                    Health = 100;
                    damage = 10;
                    moneyOnDeath = 10;
                    Sprite = _spriteSheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Enemy_Normal_Sheet], 2, true);
                    Sprite.Rotation = 3.14159f;
                    score = 1111;
                    _spriteLoopIntervalMs = 200;
                    break;
            }

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

            LoopAnimation(gameTime);

            MoveToWaypoint(gameTime);
            //Move(gameTime);
        }

        private void LoopAnimation(GameTime gameTime)
        {
            _timeSinceSpriteLoopMs += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceSpriteLoopMs >= _spriteLoopIntervalMs)
            {
                _timeSinceSpriteLoopMs -= _spriteLoopIntervalMs;
                _spriteSheet.NextIndex();
            }
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
