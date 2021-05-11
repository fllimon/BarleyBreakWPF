using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarleyBreakWpf
{
    interface IGameField
    {
        void InitializeGameField();

        void Press(int knucklesNumber);

        int this[int i, int j] { get; }

        int Step { get; }

        EventHandler<int[,]> RePrint { get; set; }

        EventHandler<bool> Winned { get; set; }
    }
}
