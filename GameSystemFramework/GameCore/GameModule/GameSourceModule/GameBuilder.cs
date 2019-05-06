using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public abstract class GameBuilder
    {
        protected Game game;

        public void Build(Game game)
        {
            this.game = game;
            Building();
        }

        protected abstract void Building();

        protected GameObject CreateGameObject(string name = "GameObject")
        {
            var go = game.mainScene.GSManager.Create<GameObject>();
            go.Name = name;
            return go;
        }

        protected GameObject Instantiate(GameObject prefab)
        {
            return GameObject.Instantiate(prefab);
        }
    }
}
