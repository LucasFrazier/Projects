using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Campground
    {
        #region Properties

        /// <summary>
        /// The campground id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The park id.
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// The campground name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The numerical month the campground is open for reservation.
        /// </summary>
        public int OpenFrom { get; set; }

        /// <summary>
        /// The numerical month the campground is closed for reservation.
        /// </summary>
        public int ClosedFrom { get; set; }

        /// <summary>
        /// The date when the campground was established.
        /// </summary>
        public decimal DailyFee { get; set; }

        #endregion
    }
}
