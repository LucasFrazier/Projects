using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// The default and only controller for this project.
    /// We're using dependency injection to connect to the database, "NPGeek".
    /// </summary>
    public class HomeController : Controller
    {
        //member variable used as key in session data
        private const string _tempTypekey = "tempType";
        //member variable used as key in session data
        private const string _parkCodeKey = "parkCode";
        //member variable used for database dependency injection
        private INPGeekDAL _dal = null;

        /// <summary>
        /// Constructor. Establishes dependency injection of database, "NPGeek".
        /// </summary>
        /// <param name="dal">Data Access Layer</param>
        public HomeController(INPGeekDAL dal)
        {
            _dal = dal;
        }

        // GET: Home/Index
        /// <summary>
        /// Loads default view, "Index".
        /// Passes it a list of what it needs to populate the view as "model".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            //Model gets populated by dal method.
            IList<IndexViewModel> model = _dal.GetParksForHomePage();
            return View("Index", model);
        }

        // GET: Home/Detail
        /// <summary>
        /// Returns the Detail View of a Park determined by the parkCode passed in by the user.
        /// </summary>
        /// <param name="parkCode">Passed in from clicking on photo in Index View.</param>
        /// <returns>Detail View populated with chosen park's information</returns>
        [HttpGet]
        public ActionResult Detail(string parkCode)
        {
            DetailViewModel model = new DetailViewModel();
            model.ParkDetails = _dal.GetAllDetailsByParkCode(parkCode);
            model.FiveDayForecast = _dal.GetFiveDayForecast(parkCode);

            //Save the park code of the detail view the user navigates to
            Session[_parkCodeKey] = model.ParkDetails.ParkCode;

            //Pull current temperature unit of measurement
            string sessionTempType = Session[_tempTypekey] as string;
            
            //Check current temperature unit to display
            if (sessionTempType != null && sessionTempType == "Celsius")
            {
                model.TempType = sessionTempType;
                model.FiveDayForecast = ConvertToCelsius(model.FiveDayForecast);
            }
            else
            {
                Session[_tempTypekey] = model.TempType;
            }

            return View("Detail", model);
        }

        //POST:Home/DetailTempUpdate
        /// <summary>
        /// Updates user chosen temperature type on session data.
        /// </summary>
        [HttpPost]
        public ActionResult DetailTempUpdate(string tempType)
        {
            string sessionParkCode = Session[_parkCodeKey] as string;
            Session[_tempTypekey] = tempType;            

            return Detail(sessionParkCode);
        }

        // GET: Home/Survey
        /// <summary>
        /// Returns the empty Survey View
        /// </summary>
        [HttpGet]
        public ActionResult Survey()
        {
            return View();
        }

        // POST: Home/Survey
        /// <summary>
        /// Validate the model and redirect to SurveyResults (if successful) or return the
        /// Survey view (if validation fails)
        /// </summary>
        [HttpPost]
        public ActionResult Survey(SurveyViewModel model)
         {
            string action = "";
            // Let's validate the model before proceeding
            if (!ModelState.IsValid)
            {
                return View("Survey");
            }
            //Add survey to database and confirm it worked
            int numRowsAffected = _dal.AddSurveyToDatabase(model);
            if(numRowsAffected > 0)
            {
                action = "SurveyResults";
            }
            else
            {
                action = "Survey";
            }

            return RedirectToAction(action);
        }

        // GET: Home/SurveyResults
        /// <summary>
        /// Returns view of favorite parks by user votes from surveys
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SurveyResults()
        {
            IList<SurveyResultsViewModel> model = _dal.GetSurveyResults();
            return View("SurveyResults", model);
        }

        /// <summary>
        /// Converts fahrenheit to celsius for all forecast days passed in.
        /// </summary>
        private IList<ForecastDay> ConvertToCelsius(IList<ForecastDay> fiveDayForecast)
        {
            IList<ForecastDay> updatedForecast = new List<ForecastDay>();

            for (int i = 0; i < fiveDayForecast.Count(); i++)
            {
                fiveDayForecast[i].High = (int)((5.0 / 9.0) * (fiveDayForecast[i].High - 32));
                fiveDayForecast[i].Low = (int)((5.0 / 9.0) * (fiveDayForecast[i].Low - 32));
                updatedForecast.Add(fiveDayForecast[i]);
            }

            return updatedForecast;
        }
}
}