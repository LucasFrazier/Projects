using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace CapStone
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void LoadInventoryFile()
        {
            VendingMachine vm = new VendingMachine();
            Assert.IsTrue(vm.Inventory.Count > 0);
            
        }

        [TestMethod]
        public void FeedMoneyTest()
        {

            VendingMachine vm = new VendingMachine();

            vm.FeedMoney(5);
            Assert.AreEqual(5, vm.CurrentBalance);
        }

        [TestMethod]
        public void ReportTest()
        {
            VendingMachine vm = new VendingMachine();
            Assert.IsTrue(vm.Inventory.Count == vm.Report.ReportDict.Count);
        }

        [TestMethod]
        public void ProductSelectTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney(10.00M);
            vm.ProductSelect("A1");
            Assert.AreEqual(4, vm.Inventory["A1"].Qty);
        }

        [TestMethod]
        public void ReturnChangeTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.FeedMoney(10.00M);
            vm.ReturnChange();
            Assert.AreEqual(0, vm.CurrentBalance);
        }




    }
}
