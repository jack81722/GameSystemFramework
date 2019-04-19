using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public abstract class GameBuilder
    {
        protected GameSourceManager gsMgr;

        public void Build(GameSourceManager gsMgr)
        {
            this.gsMgr = gsMgr;   
        }

        protected abstract void Building();

        public GameObject CreateGameObject()
        {
            var go = gsMgr.Create<GameObject>();
            return go;
        }
    }
}
