using GameSystem.GameCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SimpleGame.Scripts
{
    public class Character : Component
    {
        public float speed;

        public override void Start()
        {
            speed = 1;
        }

        public override void Update()
        {
            
        }
    }
}
