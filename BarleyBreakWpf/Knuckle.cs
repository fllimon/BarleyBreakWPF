using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarleyBreakWpf
{
    class Knuckle : ViewModel
    {
        private int _x;
        private int _y;
        private int _number;

        public Knuckle(int x, int y, int number)
        {
            _x = x;
            _y = y;
            _number = number;
        }

        public Knuckle(Knuckle source)
            : this(source._x, source._y, source._number)
        {

        }

        public int Y 
        {
            get
            {
                return _y * DefaultSettings.SIZE_AND_MARGIN;
            }
            private set
            {
                _y = value;
            }
        }

        public int X
        {
            get
            {
                return _x * DefaultSettings.SIZE_AND_MARGIN;
            }
            private set
            {
                _x = value;
            }
        }


        public int Number => _number;

        public void ToUp()
        {
            if (_y > DefaultSettings.MIN_BORDER)
            {
                _y--;

                OnPropertyChanged(nameof(Y));
            }
        }

        public void ToDown()
        {
            if (_y < DefaultSettings.MAX_BORDER)
            {
                _y++;

                OnPropertyChanged(nameof(Y));
            }
        }

        public void ToLeft()
        {
            if (_x > DefaultSettings.MIN_BORDER)
            {
                _x--;

                OnPropertyChanged(nameof(X));
            }
        }

        public void ToRight()
        {
            if (_x < DefaultSettings.MAX_BORDER)
            {
                _x++;

                OnPropertyChanged(nameof(X));
            }
        }
    }
}
