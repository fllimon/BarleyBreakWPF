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
        private IGameField _gameField;
        public GameFieldPage()
        {
            InitializeComponent();

            _gameField = new GameFieldViewModel();
            DataContext = _gameField;
        }
    }
}
