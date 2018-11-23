using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStone
{
    /// <summary>
    /// Hey Joe
    /// </summary>
    public class Change
    {
        /// <summary>
        /// 
        /// </summary>
        private const decimal Quarter = .25M;

        /// <summary>
        /// 
        /// </summary>
        private const decimal Nickel = .05M;
        private const decimal Dime = .10M;
        public int Quarters { get; private set; } = 0;
        public int Dimes { get; private set; } = 0;
        public int Nickels { get; private set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentBalance"></param>
        public Change(decimal currentBalance)
        {
            if (currentBalance >= Quarter)
            {
                Quarters = (int)(currentBalance / Quarter);
                currentBalance -= Quarters * Quarter;
            }
            if (currentBalance >= Dime)
            {
                Dimes = (int)(currentBalance / Dime);
                currentBalance -= Dimes * Dime;
            }
            if (currentBalance >= Nickel)
            {
                Nickels = (int)(currentBalance / Nickel);
                currentBalance -= Nickels * Nickel;
            }
        }
    }
}
