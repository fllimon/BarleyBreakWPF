using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BarleyBreakWpf
{
    class GameFieldViewModel : ViewModel, IGameField
    {
        private ObservableCollection<Knuckle> _knuckls;
        private int _step = -1;
        private Random _rand;
        private bool _isWin = false;
        
        public GameFieldViewModel()
        {
             _knuckls = new ObservableCollection<Knuckle>();
            _rand = new Random();

            InitializeGameField();

            RestartGame = new Command(o =>
            {
                InitializeNewGame();
            });

            Exit = new Command(o =>
            {
                Environment.Exit(0);
            });
        }

        public ObservableCollection<Knuckle> Knuckles
        {
            get
            {
                return _knuckls;
            }
            private set
            {
                _knuckls = value;
                OnPropertyChanged();
            }
        }

        public bool IsWin
        {
            get
            {
                return _isWin;
            }
            set
            {
                _isWin = value;

                OnPropertyChanged();
            }
        }

        public int Step 
        {
            get => _step;
            set
            {
                _step = value;

                OnPropertyChanged();
            }
        }

        public ICommand Press { get; set; }

        public ICommand RestartGame { get; set; }

        public ICommand Exit { get; set; }


        public void InitializeGameField()
        {
            _knuckls.Clear();

            for (int i = 0; i < DefaultSettings.DEFAULT_COLLECTION_SIZE; i++)
            {
                var k = i / DefaultSettings.DEVIDER;
                var j = i % DefaultSettings.DEVIDER;
                int num = i + 1;

                if (num == DefaultSettings.DEFAULT_COLLECTION_SIZE)
                {
                    num = 0;
                }

                _knuckls.Add(new Knuckle(j, k, num));
            }

            IsWin = false;
            MixKnuckles();
            Step = 0;
           
            OnPropertyChanged(nameof(Knuckles)); 
        }


        private void InitializeNewGame()
        {
            InitializeGameField();
        }

        private bool GetWinCombination()
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_COLLECTION_SIZE; i++)
            {
                var k = i / DefaultSettings.DEVIDER;
                var j = i % DefaultSettings.DEVIDER;
                
                if ((_knuckls[i].X != (j * DefaultSettings.SIZE_AND_MARGIN)) || 
                    (_knuckls[i].Y != (k * DefaultSettings.SIZE_AND_MARGIN)))
                {
                    return IsWin;
                }
            }

            return IsWin = true;
        }

        private Knuckle FindEmtyKnuckle(int knuckle = DefaultSettings.EMPTY_KNUCKLE)
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_COLLECTION_SIZE; i++)
            {
                if(_knuckls[i].Number == DefaultSettings.EMPTY_KNUCKLE)
                {
                    return _knuckls[i];
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
            if ((current.X - DefaultSettings.COORDINATE_STEP == empty.X) || (current.X + DefaultSettings.COORDINATE_STEP == empty.X) ||
                (current.Y - DefaultSettings.COORDINATE_STEP == empty.Y) || (current.Y + DefaultSettings.COORDINATE_STEP == empty.Y))
            {
                return true;
            }

            return false;
        }

        private Knuckle GetCoordinate(int x, int y)
        {
            for (int i = 0; i < DefaultSettings.DEFAULT_COLLECTION_SIZE; i++)
            {
                if (x >= DefaultSettings.MIN_BORDER && x <= DefaultSettings.MAX_BORDER * DefaultSettings.SIZE_AND_MARGIN && 
                    y >= DefaultSettings.MIN_BORDER && y <= DefaultSettings.MAX_BORDER * DefaultSettings.SIZE_AND_MARGIN) 
                {
                    if ((_knuckls[i].X == x) && (_knuckls[i].Y == y))
                    {
                        return _knuckls[i];
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
                switch (_rand.Next(DefaultSettings.RANDOM_STEP))
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
