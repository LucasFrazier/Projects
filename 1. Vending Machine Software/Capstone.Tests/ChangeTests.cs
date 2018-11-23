using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapStone
{
    [TestClass]
    public class ChangeTests
    {
        [TestMethod]
        public void ChangeTest()
        {
            Change test = new Change(1.15M);

            Assert.AreEqual(4, test.Quarters);
            Assert.AreEqual(1, test.Dimes);
            Assert.AreEqual(1, test.Nickels);
        }
    }
}
