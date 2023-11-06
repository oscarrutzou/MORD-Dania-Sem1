using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP.Scripts
{
    public struct Vector2Int
    {
        public int X;
        public int Y;

        public static Vector2Int Zero { get { return new Vector2Int(0, 0);  } }

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
