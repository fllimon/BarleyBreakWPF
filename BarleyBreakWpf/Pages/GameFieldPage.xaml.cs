using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarleyBreakWpf.Pages
{
    /// <summary>
    /// Interaction logic for GameFieldPage.xaml
    /// </summary>
    public partial class GameFieldPage : Page
    {
        private GameField _gameField;

        public GameFieldPage()
        {
            InitializeComponent();

            _gameField = new GameField();
            _gameField.RePrint += Print;
            _gameField.InitializeGameField();
        }

        private void Print(object sender, int[,] obj)
        {
            int[][] gameField = new int[4][];
            
            for (int i = 0; i < 4; i++)
            {
                gameField[i] = new int[4];

                for (int j = 0; j < 4; j++)
                {
                    gameField[i][j] = _gameField[i, j];
                }    
            }

            _data.ItemsSource = gameField;
            _dataText.Text = "Step: " + _gameField.Step.ToString();
        }

        private void MouseDownClick(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            int knucklesNumber = (int)border.DataContext;

            _gameField.Press(knucklesNumber);
        }
    }
}
