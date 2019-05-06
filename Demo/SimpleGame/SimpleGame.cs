using BulletEngine;
using GameSystem.GameCore;
using GameSystem.GameCore.Debugger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.SimpleGame
{
    public class SimpleGame
    {
        private Game game;
        private SimpleGameBuilder builder;

        public SimpleGame()
        {
            IDebugger debugger = new ConsoleDebugger();
            BulletEngineFactory phyEngineFactory = new BulletEngineFactory();            
            builder = new SimpleGameBuilder(debugger);
            game = new Game(phyEngineFactory, debugger);
        }

        public void Init()
        {
            builder.Build(game);
        }

        public void StartGame()
        {
            game.Start();
        }
    }
}
