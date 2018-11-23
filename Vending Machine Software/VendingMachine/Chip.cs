using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStone
{
    public class Chip : MachineItem
    {
        public override string Sound { get; } = "Crunch Crunch, Yum!";
        public Chip(string name, decimal price) : base(name, price)
        {
        }
    }
}
