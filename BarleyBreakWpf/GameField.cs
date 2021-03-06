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
        private int _step = -1;
        private Random _rand;
        private bool _isWin = false;

        public GameField(int whidth = DefaultSettings.DEFAULT_WHIDTH, int height = DefaultSettings.DEFAULT_HEIGHT)
        {
            _gameField = new int[whidth, height];
            _rand = new Random();
        }

        public int Step => _step;

        public EventHandler<int[,]> RePrint { get; internal set; }

        public EventHandler<bool> Winned { get; internal set; }

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
                    _gameField[i, j] = (i * DefaultSettings.DEFAULT_WHIDTH + j + 1) % DefaultSettings.DEVIDER;
                }
            }

            MixKnuckles();
            _step = 0;

            RePrint(this, _gameField);
        }

        private void MixKnuckles()
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_MIX_STEP; i++)
            {
                switch (_rand.Next(4))
                {
                    case 0:
                        ToLeft();
                        break;
                    case 1:
                        ToRight();
                        break;
                    case 2:
                        ToUp();
                        break;
                    case 3:
                        ToDown();
                        break;
                    default:
                        break;
                }
            }
        }

        private bool GetWinCombination()
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_WHIDTH; i++)
            {
                for (int j = 0; j < DefaultSettings.DEFAULT_HEIGHT; j++)
                {
                    if (_gameField[i, j] != (i * DefaultSettings.DEFAULT_WHIDTH + j + 1) % DefaultSettings.DEVIDER)
                    {
                        return _isWin;
                    } 
                }
            }

            return _isWin = true;
        }

        private Coordinate FindEmtyKnuckle(int knuckle = DefaultSettings.EMPTY_KNUCKLE)
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_WHIDTH; i++)
            {
                for (int j = 0; j < DefaultSettings.DEFAULT_HEIGHT; j++)
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

            ChekDirection(emptyKnuckle, currentKnuckleClik);
            GetWinCombination();

            Winned(this, _isWin);
            RePrint(this, _gameField);
        }

        private void ChekDirection(Coordinate emptyKnuckle, Coordinate currentKnuckleClik)
        {
            if (emptyKnuckle.X == currentKnuckleClik.X)
            {
                if (emptyKnuckle.Y < currentKnuckleClik.Y)
                {
                    ToLeft();
                }
                else
                {
                    ToRight();
                }
            }
            else
            {
                if (emptyKnuckle.Y == currentKnuckleClik.Y)
                {
                    if (emptyKnuckle.X < currentKnuckleClik.X)
                    {
                        ToUp();
                    }
                    else
                    {
                        ToDown();
                    }
                }
            }
        }

        private void ToDown()
        {
            Coordinate empty = FindEmtyKnuckle();

            if (empty.X > 0) 
            {
                Swap(ref _gameField[empty.X - 1, empty.Y], ref _gameField[empty.X, empty.Y]);
                _step++;
            }
        }

        private void ToUp()
        {
            Coordinate empty = FindEmtyKnuckle();

            if (empty.X < 3)
            {
                Swap(ref _gameField[empty.X, empty.Y], ref _gameField[empty.X + 1, empty.Y]);
                _step++;
            }
        }

        private void ToLeft()
        {
            Coordinate empty = FindEmtyKnuckle();

            if (empty.Y < 3)
            {
                Swap(ref _gameField[empty.X, empty.Y], ref _gameField[empty.X, empty.Y + 1]);
                _step++;
            }
        }

        private void ToRight()
        {
            Coordinate empty = FindEmtyKnuckle();

            if (empty.Y > 0)
            {
                Swap(ref _gameField[empty.X, empty.Y], ref _gameField[empty.X, empty.Y - 1]);
                _step++;
            }
        }
    }
}
