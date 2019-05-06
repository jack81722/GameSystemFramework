using GameSystem.GameCore.Debugger;
using System;
using GameSystem.GameCore.Physics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameSystem.GameCore
{
    public class Scene
    {
        int GameID;
        int SceneID;

        private bool running;
        public float TargetFps = 30;

        private Task loopTask;
        
        public PhysicEngineProxy PhysicEngine { get; private set; }
        public GameSourceManager GSManager { get; private set; }

        public IDebugger Debugger { get; private set; }

        public Scene(PhysicEngineProxy physicEngine, IDebugger debugger)
        {
            Debugger = debugger;
            PhysicEngine = physicEngine;
            GSManager = new GameSourceManager(PhysicEngine, Debugger);
        }

        public void Run()
        {
            running = true;
            if(loopTask != null) loopTask.Wait();
            loopTask = Task.Factory.StartNew(GameLoop);
        }

        public void Stop()
        {
            running = false;
        }

        public void GameLoop()
        {
            DateTime curr_time = DateTime.UtcNow;
            DateTime last_time = curr_time;
            running = true;
            while (running)
            {
                curr_time = DateTime.UtcNow;
                TimeSpan deltaTime;
                // caculate time span between current and last time
                if ((deltaTime = curr_time - last_time).TotalMilliseconds > 0)
                {
                    PhysicEngine.Update(deltaTime);
                    GSManager.Update(deltaTime);
                }
                // correct time into fps
                float TargetSecond = 1f / TargetFps;
                if (deltaTime.TotalSeconds < TargetSecond)
                {
                    Thread.Sleep((int)(TargetSecond - deltaTime.TotalSeconds));
                }
                last_time = curr_time;
            }
        }

        public void Close()
        {
            if (loopTask != null) loopTask.Wait();
            GSManager.Clear();
        }
    }
}
