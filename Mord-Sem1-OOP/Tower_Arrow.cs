using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MordSem1OOP.Scripts;
using System;
using System.Reflection.Metadata;

namespace MordSem1OOP
{

    public class Tower_Arrow : GameObject, IProjectile
    {
        public int Damage { get; set; }
        public ProjectileTypes Type { get; set; }
        public GameObject Target { get; set; }

        /// <summary>
        /// Makes a single arrow for the tower
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="enemyTarget">The target the arrow should hit</param>
        /// <param name="content">This is for calling the GameObject contructer that sets the sprite</param>
        /// <param name="texture">This is for calling the GameObject contructer that sets the sprite</param>
        public Tower_Arrow(Vector2 position, float scale, GameObject enemyTarget, int damage, int speed, ContentManager content, string texture) : base(content, texture)
        {
            Position = position;
            Scale = scale;
            Target = enemyTarget;

            Damage = damage;
            Speed = speed;
            Type = ProjectileTypes.Arrow;
            
        }   

        public override void Update(GameTime gameTime)
        {
            //Calculate direction towards target
            direction = Target.Position - Position;
            direction.Normalize();

            // Calculate rotation towards target
            Rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2;

            if (Target != null)
            {
                Move(gameTime);
            }
            else
            {
                //Move forward without changing directions
            }

            OnCollision();
        }

        public override void OnCollision()
        {
            if (Collision.IsCollidingBox(this, Target))
            {
                //Delete this object
                IsRemoved = true;
                //Target.IsRemoved = true; //Skal ikke være her
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Primitives2D.DrawLine(spriteBatch, Position, Target.Position, Color.Red, 1); //Draws the debug line from current position to the target position
            Primitives2D.DrawRectangle(spriteBatch, Position, Sprite.Rectangle, Color.Red, 1, Rotation); //Draws the collision box
        }


        //Scene script
        /*
         using Microsoft.Xna.Framework.Graphics;
        using Microsoft.Xna.Framework;
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        using System.Threading.Tasks;
        using Microsoft.Xna.Framework.Content;

        namespace MordSem1OOP
        {
        public class Scene
        {


        private ContentManager content;
        public List<GameObject> gameObjects = new List<GameObject>();
        //public List<GameObject> objectsToCreate = new List<GameObject>();
        //public List<GameObject> objectsToDestroy = new List<GameObject>();



        public Scene(ContentManager content)
        {
            this.content = content;
        }

        #region Methods

        public void Initialize()
        {
            Enemy enemy1 = new Enemy(EnemyType.Strong, new Vector2(100, 50), content);
            gameObjects.Add(enemy1);
            Global.enemies.Add(enemy1);

            Enemy enemy2 = new Enemy(EnemyType.Fast, new Vector2(30, 100), content);
            gameObjects.Add(enemy2);
            Global.enemies.Add(enemy2);

            Tower acherTower = new Tower(new Vector2(300,200), 1f, 300f, content, "Placeholder\\Parts\\beam6");
            gameObjects.Add(acherTower);
            //gameObjects.Add(new Tower_Arrow(new Vector2(50, 300), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            //gameObjects.Add(new Tower_Arrow(new Vector2(400, 30), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            //gameObjects.Add(new Tower_Arrow(new Vector2(10,10), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            //gameObjects.Add(new Tower_Arrow(new Vector2(600,300), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
            //gameObjects.Add(new Tower_Arrow(new Vector2(600,600), 1f, targetEnemy, content, "Placeholder\\Lasers\\laserBlue04"));
        }

        /// <summary>
        /// Update is called every frame
        /// </summary>
        /// <param name="gameTime">Used to get the time elapsed between each frame</param>
        public void Update(GameTime gameTime)
        {
            //All GameObjects to be added, are added to the active scene.
            foreach (GameObject gameObject in Global.gameObjectsToCreate)
                gameObjects.Add(gameObject);
            Global.gameObjectsToCreate.Clear();

            //All GameObjects to be destroyed, are removed from the active scene.
            gameObjects = gameObjects.Where(gameObject => !gameObject.IsRemoved).ToList();

            //Also remove from Global.enemies if it's an Enemy
            Global.enemies = Global.enemies.Where(enemy => !enemy.IsRemoved).ToList();

            //Call update on every GameObject in the active scene.
            foreach (GameObject gameObject in gameObjects)
                gameObject.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Call draw on every GameObject in the active scene.
            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(spriteBatch);
        }


        #endregion


        }
        }
        */
}
}
