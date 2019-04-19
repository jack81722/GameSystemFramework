using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public interface IGameModule
    {
        void Initialize();
        void Update(TimeSpan tick);
    }
}
