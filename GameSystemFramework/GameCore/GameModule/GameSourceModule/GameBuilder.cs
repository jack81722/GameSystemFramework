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
            Building();
        }

        protected abstract void Building();

        protected GameObject CreateGameObject(string name = "GameObject")
        {
            var go = gsMgr.Create<GameObject>();
            go.Name = name;
            return go;
        }

        protected GameObject Instantiate(GameObject prefab)
        {
            return GameObject.Instantiate(prefab);
        }
    }
}
