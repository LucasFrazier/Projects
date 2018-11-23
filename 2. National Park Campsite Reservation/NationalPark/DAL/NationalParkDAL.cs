using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class NationalParkDAL : INationalParkDAL
    {
        private string _connectionString;
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        // Single Parameter Constructor
        public NationalParkDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Park Methods

        /// <summary>
        /// Returns a list of ALL parks in the database
        /// </summary>
        public List<Park> GetParks()
        {
            List<Park> parkList = new List<Park>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Run query
                SqlCommand cmd = new SqlCommand();

                string SQL_Department = "SELECT * FROM park ORDER BY name";
                cmd.CommandText = SQL_Department;
                cmd.Connection = connection;

                // Returns a result set of data
                SqlDataReader reader = cmd.ExecuteReader();

                // Parse results        
                while (reader.Read())
                {
                    var item = PopulateParkFromReader(reader);
                    parkList.Add(item);
                }
            }

            return parkList;
        }

        /// <summary>
        /// Creates a new instance of a Park using data from SqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Park PopulateParkFromReader(SqlDataReader reader)
        {
            Park item = new Park();
            item.park_id = (int)reader["park_id"];
            item.Name = (string)reader["name"];
            item.Location = (string)reader["location"];
            item.EstablishDate = (DateTime)reader["establish_date"];
            item.Area = (int)reader["area"];
            item.Visitors = (int)reader["visitors"];
            item.Description = (string)reader["description"];
            return item;
        }

        #endregion

        #region Campground Methods

        /// <summary>
        /// Returns a list of campgrounds from the database filtered by the user's chosen park
        /// </summary>
        public List<Campground> GetCampgroundsByPark(Park userPark)
        {
            List<Campground> campgroundList = new List<Campground>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Run query
                SqlCommand cmd = new SqlCommand();

                string SQL_Department = "SELECT * FROM campground JOIN park on park.park_id = campground.park_id WHERE campground.park_id = @park_id";
                cmd.CommandText = SQL_Department;
                cmd.Connection = connection;

                // Returns a result set of data
                cmd.Parameters.AddWithValue("@park_id", userPark.park_id);
                SqlDataReader reader = cmd.ExecuteReader();

                
                // Parse results        
                while (reader.Read())
                {
                    var item = PopulateCampgroundFromReader(reader);
                    campgroundList.Add(item);
                }
            }

            return campgroundList;
        }

        /// <summary>
        /// Creates a new instance of a Campground using data from SqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Campground PopulateCampgroundFromReader(SqlDataReader reader)
        {
            Campground item = new Campground();
            item.Id = (int)reader["campground_id"];
            item.ParkId = (int)reader["park_id"];
            item.Name = (string)reader["name"];
            item.OpenFrom = (int)reader["open_from_mm"];
            item.ClosedFrom = (int)reader["open_to_mm"];
            item.DailyFee = (decimal)reader["daily_fee"];
            return item;
        }

        #endregion

        #region Site Methods 

        /// <summary>
        /// Returns a list of sites from the database filtered by the user's chosen campground
        /// </summary>
        public List<Site> GetSitesByCampground(int campgroundId)
        {
            List<Site> sitesList = new List<Site>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Run query
                SqlCommand cmd = new SqlCommand();

                string SQL_Department = "SELECT * FROM site WHERE site.campground_id = @campgroundId";
                cmd.CommandText = SQL_Department;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                // Returns a result set of data
                SqlDataReader reader = cmd.ExecuteReader();

                //Parse results
                while (reader.Read())
                {
                    var item = PopulateSiteFromReader(reader);
                    sitesList.Add(item);
                }
            }

            return sitesList;
        }

        private Site PopulateSiteFromReader(SqlDataReader reader)
        {
            Site item = new Site();
            item.SiteId = (int)reader["site_id"];
            item.CampgroundId = (int)reader["campground_id"];
            item.Number = (int)reader["site_number"];
            item.MaxOccupancy = (int)reader["max_occupancy"];
            item.Accessible = (bool)reader["accessible"];
            item.MaxRvLength = (int)reader["max_rv_length"];
            item.Utilities = (bool)reader["utilities"];
            return item;
        }

        #endregion

        #region Reservation Methods

        /// <summary>
        /// Inserts a new reservation into the database
        /// Contains the user's site choice, a name for the reservation, arrival date, and departure date
        /// </summary>
        public int MakeReservation(int userSiteChoice, string userResName, DateTime userArrDate, DateTime userDepDate)
        {
            int reservationId = 0;

            // Create a connection string
            // Create a db connection object
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open db connection
                connection.Open();

                // Create the query string
                string SQL_AddProject = "INSERT INTO reservation (site_id, name, from_date, to_date) " +
                                     "Values (@userSiteChoice, @userResName, @userArrDate, @userDepDate);";

                // Create a command object
                SqlCommand cmd = new SqlCommand(SQL_AddProject + _getLastIdSQL, connection);

                // Load up the command object parameters
                cmd.Parameters.AddWithValue("@userSiteChoice", userSiteChoice);
                cmd.Parameters.AddWithValue("@userResName", userResName);
                cmd.Parameters.AddWithValue("@userArrDate", userArrDate);
                cmd.Parameters.AddWithValue("@userDepDate", userDepDate);

                // Execute the query
                // Parse the result set
                reservationId = (int)cmd.ExecuteScalar();
            }

            return reservationId;
        }

        //private Reservation PopulateReservationFromReader(SqlDataReader reader)
        //{
        //    Reservation item = new Reservation();
        //    item.ReservationId = (int)reader["reservation_id"];
        //    item.SiteId = (int)reader["site_id"];
        //    item.Name = (string)reader["name"];
        //    item.FromDate = (DateTime)reader["from_date"];
        //    item.ToDate = (DateTime)reader["to_date"];
        //    item.CreateDate = (DateTime)reader["create_date"];
        //    return item;
        //}

        #endregion

        #region CustomItem Methods

        /// <summary>
        /// Returns a list of sites from the database filtered by the user's chosen campground, arrival date, and departure date
        /// Will only allow the user to see sites that are not already reserved for their chosen time period
        /// </summary>
        public List<CustomItem> GetSitesForUser(int campgroundId, DateTime fromDate, DateTime toDate)
        {
            List<CustomItem> customItemList = new List<CustomItem>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Run query
                SqlCommand cmd = new SqlCommand();

                string SQL_Department = "SELECT TOP (5) *, " +
                                        "(campground.daily_fee * (DATEDIFF(day, @fromDate, @toDate))) as 'totalCost' " +
                                        "from site " +
                                        "join campground on campground.campground_id = site.campground_id " +
                                        "where site.campground_id = @campgroundId " +
                                        "and site_id not in (select site_id " +
                                        "from reservation " +
                                        "where reservation.from_date <= @toDate and reservation.to_date >= @fromDate)";
                cmd.CommandText = SQL_Department;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                // Returns a result set of data
                SqlDataReader reader = cmd.ExecuteReader();

                //Parse results
                while (reader.Read())
                {
                    var item = PopulateCustomItemFromReader(reader);
                    customItemList.Add(item);
                }
            }

            return customItemList;
        }

        private CustomItem PopulateCustomItemFromReader(SqlDataReader reader)
        {
            CustomItem item = new CustomItem();
            item.Number = (int)reader["site_number"];
            item.MaxOccupancy = (int)reader["max_occupancy"];
            item.Accessible = (bool)reader["accessible"];
            item.MaxRvLength = (int)reader["max_rv_length"];
            item.Utilities = (bool)reader["utilities"];
            item.DailyFee = (decimal)reader["daily_fee"];
            item.TotalCost = (decimal)reader["totalCost"];
            return item;
        }

        #endregion
    }
}
