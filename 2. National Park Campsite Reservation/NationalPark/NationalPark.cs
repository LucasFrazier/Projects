using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone
{
    public class NationalPark
    {
        private NationalParkDAL _db = null;

        public NationalPark(string connectionString)
        {
            _db = new NationalParkDAL(connectionString);            
        }

        #region Methods

        /// <summary>
        /// Returns a list of ALL parks in the database
        /// </summary>
        
        public List<Park> GetParks()
        {
            return _db.GetParks();
        }

        /// <summary>
        /// Returns a list of campgrounds from the database filtered by the user's chosen park
        /// </summary>
        public List<Campground> GetCampgroundsByPark(Park userPark)
        {
            return _db.GetCampgroundsByPark(userPark);
        }

        /// <summary>
        /// Returns a list of sites from the database filtered by the user's chosen campground
        /// </summary>
        public List<Site> GetSitesByCampground(Campground userCampground)
        {
            return _db.GetSitesByCampground(userCampground.Id);
        }

        /// <summary>
        /// Returns a list of sites from the database filtered by the user's chosen campground, arrival date, and departure date
        /// Will only allow the user to see sites that are not already reserved for their chosen time period
        /// </summary>
        public List<CustomItem> GetSitesForUser(int campgroundId, DateTime fromDate, DateTime toDate)
        {
            return _db.GetSitesForUser(campgroundId, fromDate, toDate);
        }

        /// <summary>
        /// Inserts a new reservation into the database
        /// Contains the user's site choice, a name for the reservation, arrival date, and departure date
        /// </summary>
        public int MakeReservation(int userSiteChoice, string userResName, DateTime userArrDate, DateTime userDepDate)
        {
            return _db.MakeReservation(userSiteChoice, userResName, userArrDate, userDepDate);
        }

        #endregion
    }
}
