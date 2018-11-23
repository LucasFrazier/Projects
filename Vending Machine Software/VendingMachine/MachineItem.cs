using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStone
{
    public abstract class MachineItem
    {
        public int Qty { get; set; } = 5;
        public string DisplayQty
        {
            get
            {
                if (!SoldOut)
                {
                    return Qty.ToString();
                }
                else
                {
                    return "Sold Out";
                }
            }
        }
        public string Name { get; }
        public decimal Price { get; }
        public bool SoldOut
        {
            get
            {
                if (Qty == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public abstract string Sound { get; }

        public MachineItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
