using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore.GameModule.PhysicModule.ShapeInterface
{
    public interface IConeShape
    {
        float Radius { get; }
        float Height { get; }

        void SetSize(float radius, float height);
    }
}
