using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMenus menu = new VendingMenus(new VendingMachine());
            menu.MainMenu();
        }
    }
}
