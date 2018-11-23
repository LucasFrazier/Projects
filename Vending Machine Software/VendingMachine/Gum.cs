using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStone
{
    public class Gum : MachineItem
    {
        public override string Sound { get; } = "Chew Chew, Yum";
        public Gum(string name, decimal price) : base(name, price)
        {
        }
    }
}
