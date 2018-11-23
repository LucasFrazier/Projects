using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    interface INationalParkDAL
    {
        /// <summary>
        /// Returns a list of ALL parks in the database
        /// </summary>
        List<Park> GetParks();

        /// <summary>
        /// Returns a list of campgrounds from the database filtered by the user's chosen park
        /// </summary>
        List<Campground> GetCampgroundsByPark(Park userPark);

        /// <summary>
        /// Returns a list of sites from the database filtered by the user's chosen campground
        /// </summary>
        List<Site> GetSitesByCampground(int campgroundId);

        /// <summary>
        /// Inserts a new reservation into the database
        /// Contains the user's site choice, a name for the reservation, arrival date, and departure date
        /// </summary>
        int MakeReservation(int userSiteChoice, string userResName, DateTime userArrDate, DateTime userDepDate);

        /// <summary>
        /// Returns a list of sites from the database filtered by the user's chosen campground, arrival date, and departure date
        /// Will only allow the user to see sites that are not already reserved for their chosen time period
        /// </summary>
        List<CustomItem> GetSitesForUser(int campgroundId, DateTime fromDate, DateTime toDate);

    }
}
