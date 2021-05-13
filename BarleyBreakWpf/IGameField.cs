using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarleyBreakWpf
{
    interface IGameField
    {
        void InitializeGameField();

        ObservableCollection<Knuckle> Knuckles { get; }

        void ChekDirection(Knuckle currentKnuckleClik);

        int Step { get; }

        EventHandler<bool> Winned { get; set; }
    }
}
