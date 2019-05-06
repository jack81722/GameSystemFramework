using GameSystem.GameCore.Debugger;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public interface IPhysicEngineFactory
    {
        PhysicEngineProxy Create(IDebugger debugger);
    }
}
