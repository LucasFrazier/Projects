using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone
{
    [TestClass]
    public class NationalParkDALTests
    {
        private TransactionScope _tran;
        private string _connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = NationalPark; Integrated Security = True";

        [TestInitialize]
        public void Initialize()
        {
            _tran = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose();
        }

        [TestMethod]
        public void NationalParkDALMethods()
        {
            NationalParkDAL _park = new NationalParkDAL(_connectionString);

            //GetParks() and PopulateParkFromReader() - second method contained within first
            List<Park> parkList = _park.GetParks();
            Assert.IsNotNull(parkList);
            //GetCampgroundsByPark() and PopulateCampgroundFromReader() - second method contained within first
            List<Campground> campList = _park.GetCampgroundsByPark(parkList[0]);
            Assert.IsNotNull(campList);

            //GetSitesByCampground() and PopulateSiteFromReader() - second method contained within first
            List<Site> siteList = _park.GetSitesByCampground(campList[0].Id);
            Assert.IsNotNull(siteList);

            //GetSitesForUser() and PopulateCustomItemFromReader() - second method contained within first
            DateTime userArrDate = new DateTime(2018, 08, 01);
            DateTime userDepDate = new DateTime(2018, 08, 30);
            List<CustomItem> sitesForUser = _park.GetSitesForUser(campList[0].Id, userArrDate, userDepDate);
            Assert.IsNotNull(sitesForUser);

            //MakeReservation()
            int confirmNum = _park.MakeReservation(siteList[0].SiteId, "TEST FAMILY NAME", userArrDate, userDepDate);
            Assert.IsNotNull(confirmNum);
        }
    }
}
