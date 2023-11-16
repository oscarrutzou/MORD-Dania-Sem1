namespace MordSem1OOP
{
    /// <summary>
    /// This class is to proplerly divivide the things that should be drawn to the player like the players money
    /// </summary>
    public class SceneStats
    {
        public int maxHealth = 100;
        private int _health = 100;
        public int money = 400;
        public int killCount;
        //TODO point (bruges af animated counter) Enemy switch case
        private int score;

        public int Health
        {
            get { return _health; }
            set { _health = value;
                if (_health < 0) _health = 0;
                if (_health == 0)
                {
                    GameWorld.scenes[6].sceneData.sceneStats.score = score;
                    Global.gameWorld.activeScene = 6;
                    Global.activeScene = GameWorld.scenes[6];
                    GameWorld.scenes[6].Initialize();
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
