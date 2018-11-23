using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CapStone
{
    [TestClass]
    public class MachineItemTests
    {
        [TestMethod]
        public void MakeSoundTests()
        {
            Chip testChip = new Chip("test", 10.00M);
            Candy testCandy = new Candy("test", 10.00M);
            Drink testDrink = new Drink("test", 10.00M);
            Gum testGum = new Gum("test", 10.00M);
            Assert.AreEqual("Crunch Crunch, Yum!", testChip.Sound);
            Assert.AreEqual("Munch Munch, Yum!", testCandy.Sound);
            Assert.AreEqual("Glug Glug, Yum!", testDrink.Sound);
            Assert.AreEqual("Chew Chew, Yum", testGum.Sound);
        }

        [TestMethod]
        public void DisplayQtyTest()
        {
            Chip test = new Chip("testName", 10.00M);
            Assert.AreEqual(5, int.Parse(test.DisplayQty));
            test.Qty = 0;
            Assert.AreEqual("Sold Out", test.DisplayQty);
        }
    }
}
