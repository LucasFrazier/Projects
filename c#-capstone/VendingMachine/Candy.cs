using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStone
{
    public class Candy : MachineItem
    {
        public override string Sound { get; } = "Munch Munch, Yum!";
        public Candy(string name, decimal price) : base(name, price)
        {
        }
    }
}
