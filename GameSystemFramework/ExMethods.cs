using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem
{
    public static class ExMethods
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}
