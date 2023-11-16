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
        private int scoreOnDeath;
        public int Health { get => health; set => health = value; }
        public int Damage { get => damage; }
        public float DistanceTraveled { get => distanceTraveled; set => distanceTraveled = value; }
        protected Waypoint _waypoint;

        private SpriteSheet _spriteSheet;
        private int _spriteLoopIntervalMs;
        private int _timeSinceSpriteLoopMs;

        private int level;
        #endregion

        /// <summary>
        /// Main constructor for the enemy.
        /// </summary>
        /// <param name="enemyType">This type defines the speed, health and texture of the enemy.</param>            
        public Enemy(EnemyType enemyType, Vector2 position) : this(enemyType, position, null, 0) { }

        public Enemy(EnemyType enemyType, Vector2 position, Waypoint waypoint, int level)
        {
            this.level = level;
            _waypoint = waypoint;

            //This is to be replaced by a direction towards a waypoint
            //direction = new Vector2(1, 0);

            this.enemyType = enemyType;

            switch (enemyType)
            {
                case EnemyType.Normal:
                    Speed = 50;
                    Health = 100 + level * 60;
                    damage = 10;
                    moneyOnDeath = 10;
                    Sprite = _spriteSheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Enemy_Normal_Sheet], 2, true);
                    Sprite.Rotation = 3.14159f;
                    scoreOnDeath = 1111;
                    _spriteLoopIntervalMs = 200;
                    break;

                case EnemyType.Fast:
                    Speed = 100;
                    Health = 50 + level * 35;
                    damage = 7;
                    moneyOnDeath = 5;
                    Sprite = _spriteSheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Enemy_Fast_Sheet], 2, true);
                    Sprite.Rotation = 3.14159f;
                    scoreOnDeath = 2222;
                    _spriteLoopIntervalMs = 150;
                    break;

                case EnemyType.Strong:
                    Speed = 30;
                    Health = 200 + level * 100;
                    damage = 23;
                    moneyOnDeath = 20;
                    Sprite = _spriteSheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Enemy_Strong_Sheet], 2, true);
                    Sprite.Rotation = 3.14159f;
                    scoreOnDeath = 3333;
                    _spriteLoopIntervalMs = 350;
                    break;

                default:
                    Speed = 50;
                    Health = 100 + level * 60;
                    damage = 10;
                    moneyOnDeath = 10;
                    Sprite = _spriteSheet = new SpriteSheet(GlobalTextures.Textures[TextureNames.Enemy_Normal_Sheet], 2, true);
                    Sprite.Rotation = 3.14159f;
                    scoreOnDeath = 1111;
                    _spriteLoopIntervalMs = 200;
                    break;
            }

            Position = position;
            Scale = 1;
            SetColor();
            Rotation = 4.71239f;
        }

        public Enemy(EnemyType enemyType, Vector2 position, Waypoint waypoint) : this(enemyType, position, waypoint, 0) { }

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
                Global.activeScene.sceneData.sceneStats.Score += scoreOnDeath;
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

        private void SetColor()
        {
            switch (level)
            {
                case 0:
                    Sprite.Color = Color.White;
                    break;

                case 1:
                    Sprite.Color = Color.DarkGray;
                    break;

                case 2:
                    Sprite.Color = Color.LightBlue;
                    break;

                case 3:
                    Sprite.Color = Color.LightGreen;
                    break;

                case 4:
                    Sprite.Color = Color.Green;
                    break;

                case 6:
                    Sprite.Color = Color.Tomato;
                    break;

                default:
                    Sprite.Color = Color.OrangeRed;
                    break;
            }
        }
    }
}
