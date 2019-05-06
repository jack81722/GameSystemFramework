using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore.Communication
{
    public interface ISender
    {
        void Recv();
        void Send(object obj);
    }
}
