using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore.GameModule.PhysicModule.ShapeInterface
{
    public interface ISphereShape
    {
        float Radius { get; }

        void SetSize(float radius);
    }
}
