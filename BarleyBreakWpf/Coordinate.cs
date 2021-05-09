using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarleyBreakWpf
{
    struct Coordinate
    {
        private int _x;
        private int _y;

        public Coordinate(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Coordinate(Coordinate source) 
            : this(source._x, source._y)
        {

        }

        public int X => _x;

        public int Y => _y;
    }
}
