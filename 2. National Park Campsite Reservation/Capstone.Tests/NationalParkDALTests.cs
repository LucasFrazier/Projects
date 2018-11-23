using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            //GetParks() and PopulateParkFromReader()
            List<Park> parkList = _park.GetParks();
            Assert.IsNotNull(parkList);
            //Campground Methods

            //Site Methods

            //Reservation Methods

            //CustomItem Methods


        }
    }
}
