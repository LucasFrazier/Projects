using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Site
    {
        /// <summary>
        /// The site id.
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// The campground id.
        /// </summary>
        public int CampgroundId { get; set; }

        /// <summary>
        /// The site number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The max occupancy for the site.
        /// </summary>
        public int MaxOccupancy { get; set; }

        /// <summary>
        /// Whether or not the site is accessible.
        /// </summary>
        public bool Accessible { get; set; }

        /// <summary>
        /// The max RV length allowed on the site.
        /// </summary>
        public int MaxRvLength { get; set; }

        /// <summary>
        /// Whether or not the site has utilities.
        /// </summary>
        public bool Utilities { get; set; }
    }
}
