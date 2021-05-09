using BarleyBreakWpf.Pages;
using MahApps.Metro.Controls;
using System.Windows;
namespace BarleyBreakWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGameButtonClick(object sender, RoutedEventArgs e)
        {
            _frame.Content = new GameFieldPage();
            _startGameButton.Visibility = Visibility.Hidden;
        }
    }
}
