using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Park
    {
        #region Properties

        /// <summary>
        /// The park id.
        /// </summary>
        public int park_id { get; set; }

        /// <summary>
        /// The park name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The park location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The date when the park was established.
        /// </summary>
        public DateTime EstablishDate { get; set; }

        /// <summary>
        /// The total park area.
        /// </summary>
        public int Area { get; set; }

        /// <summary>
        /// The total park visitors.
        /// </summary>
        public int Visitors { get; set; }

        /// <summary>
        /// Description of the park.
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
