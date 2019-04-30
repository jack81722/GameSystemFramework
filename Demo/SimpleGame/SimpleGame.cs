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
        GameSourceManager gsManager;
        SimpleGameBuilder builder;

        public SimpleGame()
        {
            BulletPhysicEngine physicEngine = new BulletPhysicEngine();
            gsManager = new GameSourceManager(physicEngine);
            builder = new SimpleGameBuilder();
            game = new Game(physicEngine, gsManager);
        }

        public void Init()
        {
            game.Initialize();
            builder.Build(gsManager);
        }

        public void StartGame()
        {
            //gameTask = Task.Factory.StartNew(() => game.Start());
            game.Start();
        }

        public void StopGame()
        {   
            game.Stop();
        }
    }
}
