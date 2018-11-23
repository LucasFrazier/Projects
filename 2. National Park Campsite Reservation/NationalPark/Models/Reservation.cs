using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Reservation
    {
        #region Properties

        /// <summary>
        /// The reservation id.
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// The site id.
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// The reservation name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date when the reservation begins.
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// The date when the reservation ends.
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// The date the reservation was made.
        /// </summary>
        public DateTime CreateDate { get; set; }

        #endregion
    }
}
