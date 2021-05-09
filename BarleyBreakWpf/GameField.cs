using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BarleyBreakWpf
{
    class GameField
    {
        private int[,] _gameField;

        public GameField(int whidth = DefaultSettings.DEFAULT_WHIDTH, int height = DefaultSettings.DEFAULT_HEIGHT)
        {
            _gameField = new int[whidth, height];
        }

        public Action<object, int[,]> RePrint { get; internal set; }

        public int this[int i, int j]
        {
            get { return _gameField[i, j]; }
        }

        public void InitializeGameField()
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_WHIDTH; i++)
            {
                for (int j = 0; j < DefaultSettings.DEFAULT_HEIGHT; j++)
                {
                    _gameField[i, j] = (i * 4 + j + 1) % DefaultSettings.DEVIDER;
                }
            }

            RePrint(this, _gameField);
        }

        private Coordinate FindEmtyKnuckle(int knuckle = DefaultSettings.EMPTY_KNUCKLE)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_gameField[i,j] == knuckle)
                    {
                        return new Coordinate(i, j);
                    }
                }
            }

            return new Coordinate();
        }

        private void Swap( ref int emptyKnuckles, ref int currentKnuckles)
        {
            var tmp = emptyKnuckles;
            emptyKnuckles = currentKnuckles;
            currentKnuckles = tmp;
        }

        public void Press(int knucklesNumber)
        {
            Coordinate emptyKnuckle = FindEmtyKnuckle();
            Coordinate currentKnuckleClik = FindEmtyKnuckle(knucklesNumber);

            Swap(ref _gameField[emptyKnuckle.X, emptyKnuckle.Y], ref _gameField[currentKnuckleClik.X, currentKnuckleClik.Y]);

            RePrint(this, _gameField);
        }
    }
}
