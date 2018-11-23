using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStone
{
    public class Drink : MachineItem
    {
        public override string Sound { get; } = "Glug Glug, Yum!";
        public Drink(string name, decimal price) : base(name, price)
        {
        }
    }
}
