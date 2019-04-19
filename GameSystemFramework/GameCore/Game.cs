using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameSystem.GameCore
{
    public class Game
    {
        /// <summary>
        /// Defined game module list
        /// </summary>
        protected List<IGameModule> GMList;

        private bool running = true;
        public float TargetFPS = 60f;

        public Game(params IGameModule[] gameModules)
        {
            GMList = new List<IGameModule>(gameModules);
            GMList.RemoveAll(gm => gm == null);
        }

        public void Initialize()
        {
            for(int i = 0; i < GMList.Count; i++)
            {
                GMList[i].Initialize();
            }
        }

        public void Start()
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
                    // update all game module by delta time
                    for(int i = 0; i < GMList.Count; i++)
                    {
                        GMList[i].Update(deltaTime);
                    }
                }
                // correct time into fps
                float TargetSecond = 1f / TargetFPS;
                if (deltaTime.TotalSeconds < TargetSecond)
                {
                    Thread.Sleep((int)(TargetSecond - deltaTime.TotalSeconds));
                }
                last_time = curr_time;
            }
        }

        public void Stop()
        {
            running = false;
        }
    }
}
