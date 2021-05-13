using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BarleyBreakWpf
{
    class GameField : ViewModel, IGameField
    {
        private ObservableCollection<Knuckle> _gameField;
        private int _step = -1;
        private Random _rand;
        private bool _isWin = false;
        private Command _restartGame;


        public GameField()
        {
             _gameField = new ObservableCollection<Knuckle>();
            _rand = new Random();

        }

        public ObservableCollection<Knuckle> Knuckles
        {
            get
            {
                return _gameField;
            }
            private set
            {
                _gameField = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return _isWin;
            }
            private set
            {
                _isWin = value;

                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public int Step 
        {
            get => _step;
            private set
            {
                _step = value;

                OnPropertyChanged(nameof(Step));
            }
        }

        public Command RestartGame
        {
            get
            {
                return _restartGame;
            }
        }

        public EventHandler<bool> Winned { get; set; }

        public void InitializeGameField()
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_COLLECTION_SIZE; i++)
            {
                var k = i / DefaultSettings.DEVIDER;
                var j = i % DefaultSettings.DEVIDER;
                int num = i + 1;

                if (num == DefaultSettings.DEFAULT_COLLECTION_SIZE)
                {
                    num = 0;
                }

                _gameField.Add(new Knuckle(j, k, num));
            }

            _isWin = false;
            MixKnuckles();
            _step = 0;
        }


        private void InitializeNewGame()
        {
            InitializeGameField();

            Knuckles = _gameField;
        }

        private bool GetWinCombination()
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_COLLECTION_SIZE; i++)
            {
                var k = i / DefaultSettings.DEVIDER;
                var j = i % DefaultSettings.DEVIDER;
                
                if ((_gameField[i].X != (j * DefaultSettings.SIZE_AND_MARGIN)) || 
                    (_gameField[i].Y != (k * DefaultSettings.SIZE_AND_MARGIN)))
                {
                    return _isWin;
                }
            }

            return _isWin = true;
        }

        private Knuckle FindEmtyKnuckle(int knuckle = DefaultSettings.EMPTY_KNUCKLE)
        {
            for (int i = 0; i < 16; i++)
            {
                if(_gameField[i].Number == DefaultSettings.EMPTY_KNUCKLE)
                {
                    return _gameField[i];
                }
            }

            throw new KeyNotFoundException();
        }

        public void ChekDirection(Knuckle currentKnuckleClik)
        {
            Knuckle emptyKnuckle = FindEmtyKnuckle();

            if (emptyKnuckle.Y == currentKnuckleClik.Y)
            {
                ToLeftOrRight(currentKnuckleClik, emptyKnuckle);
            }
            else
            {
                ToUpOrDown(currentKnuckleClik, emptyKnuckle);
            }

            GetWinCombination();

            Winned(this, _isWin);
        }

        private void ToLeftOrRight(Knuckle currentKnuckleClik, Knuckle emptyKnuckle)
        {
            if (emptyKnuckle.X > currentKnuckleClik.X)
            {
                if (IsEmpty(emptyKnuckle, currentKnuckleClik))
                {
                    currentKnuckleClik.ToRight();
                    emptyKnuckle.ToLeft();
                    Step++;
                }
            }
            else
            {
                if (IsEmpty(emptyKnuckle, currentKnuckleClik))
                {
                    currentKnuckleClik.ToLeft();
                    emptyKnuckle.ToRight();
                    Step++;
                }
            }
        }

        private void ToUpOrDown(Knuckle currentKnuckleClik, Knuckle emptyKnuckle)
        {
            if (emptyKnuckle.Y > currentKnuckleClik.Y)
            {
                if (emptyKnuckle.X == currentKnuckleClik.X)
                {
                    if (IsEmpty(emptyKnuckle, currentKnuckleClik))
                    {
                        currentKnuckleClik.ToDown();
                        emptyKnuckle.ToUp();
                        Step++;
                    }
                }
            }
            else
            {
                if (emptyKnuckle.Y < currentKnuckleClik.Y)
                {
                    if (emptyKnuckle.X == currentKnuckleClik.X)
                    {
                        if (IsEmpty(emptyKnuckle, currentKnuckleClik))
                        {
                            currentKnuckleClik.ToUp();
                            emptyKnuckle.ToDown();
                            Step++;
                        }
                    }
                }
            }
        }

        private bool IsEmpty(Knuckle empty, Knuckle current)
        {
            if ((current.X - 110 == empty.X) || (current.X + 110 == empty.X) ||
                (current.Y - 110 == empty.Y) || (current.Y + 110 == empty.Y))
            {
                return true;
            }

            return false;
        }

        private Knuckle GetCoordinate(int x, int y)
        {
            for (int i = 0; i < 16; i++)
            {
                if (x >= 0 && x <= 330 && y >= 0 && y <= 330) 
                {
                    if ((_gameField[i].X == x) && (_gameField[i].Y == y))
                    {
                        return _gameField[i];
                    }
                }
                else
                {
                    return null;
                }

            }

            throw new KeyNotFoundException();
        }

        private void MixKnuckles()
        {
            Knuckle empty = FindEmtyKnuckle();
            Knuckle current = null;

            for (int i = 0; i < DefaultSettings.DEFAULT_MIX_STEP; i++)
            {
                switch (_rand.Next(4))
                {
                    case 0:
                        current = GetCoordinate(((empty.X / DefaultSettings.SIZE_AND_MARGIN) - 1) * 
                                                            DefaultSettings.SIZE_AND_MARGIN, empty.Y);
                        if (current != null)
                        {
                            current.ToRight();
                            empty.ToLeft();
                        }
                        
                        break;
                    case 1:
                        current = GetCoordinate(((empty.X / DefaultSettings.SIZE_AND_MARGIN) + 1) * 
                                                            DefaultSettings.SIZE_AND_MARGIN, empty.Y);
                        
                        if (current != null)
                        {
                            current.ToLeft();
                            empty.ToRight();
                        } 

                        break;
                    case 2:
                        current = GetCoordinate(empty.X, ((empty.Y / DefaultSettings.SIZE_AND_MARGIN) - 1) * 
                                                                     DefaultSettings.SIZE_AND_MARGIN);
                        if (current != null)
                        {
                            current.ToDown();
                            empty.ToUp();
                        }
                        
                        break;
                    case 3:
                        current = GetCoordinate(empty.X, ((empty.Y / DefaultSettings.SIZE_AND_MARGIN) + 1) * 
                                                                     DefaultSettings.SIZE_AND_MARGIN);

                        if (current != null)
                        {
                            current.ToUp();
                            empty.ToDown();
                        }
                        
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
