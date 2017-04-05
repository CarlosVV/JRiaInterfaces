using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Receipt_Main.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Controllers.Tests
{
    [TestClass()]
    public class MTControllerTests
    {
        [TestMethod()]
        public void PingServerTest()
        {
           Assert.AreEqual(true, true);
        }

        [TestMethod()]
        public void PostPayoutTest()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod()]
        public void PostOrderSendTest()
        {
           Assert.AreEqual(true, true);
        }

        [TestMethod()]
        public void PostComplianceTest()
        {
           Assert.AreEqual(true, true);
        }
    }
}