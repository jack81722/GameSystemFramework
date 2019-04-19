using GameSystem.ForBullet;
using GameSystem.GameCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.SimpleGame
{
    public class SimpleGame
    {
        Task gameTask;
        Game game;

        public SimpleGame()
        {
            BulletPhysicEngine physicEngine = new BulletPhysicEngine();
            GameSourceManager gsManager = new GameSourceManager(physicEngine);
            game = new Game(physicEngine, gsManager);
        }

        public void Init()
        {
            game.Initialize();
        }

        public void StartGame()
        {
            gameTask = Task.Factory.StartNew(() => game.Start());
        }

        public void StopGame()
        {

        }
    }
}
