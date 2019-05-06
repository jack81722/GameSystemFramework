using GameSystem.GameCore;
using GameSystem.GameCore.Debugger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletEngine
{
    public class BulletEngineFactory : IPhysicEngineFactory
    {
        public PhysicEngineProxy Create(IDebugger debugger)
        {
            return new BulletPhysicEngine(debugger);
        }
    }
}
