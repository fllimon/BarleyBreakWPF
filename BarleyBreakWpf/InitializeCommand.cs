using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;
using BarleyBreakWpf.Pages;

namespace BarleyBreakWpf
{
    class InitializeCommand
    {
        private GameField _gameField = new GameField();
        private ICommand _printGameField;

        //public InitializeCommand()
        //{
        //    _printGameField = new BarleyBreakCommand(WriteGameField);
        //}

        //public ICommand PrintGameField => _printGameField;

        //private void WriteGameField()
        //{
        //    _gameField.InitializeGameField();
        //}
    }
}
