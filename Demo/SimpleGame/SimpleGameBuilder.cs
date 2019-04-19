using GameSystem.GameCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SimpleGame
{
    public class SimpleGameBuilder : GameBuilder
    {
        protected override void Building()
        {
            GameObject go = CreateGameObject();
            
        }
    }
}
