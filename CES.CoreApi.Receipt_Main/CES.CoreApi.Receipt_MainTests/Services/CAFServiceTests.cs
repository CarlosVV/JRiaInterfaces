using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Receipt_Main.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Receipt_Main.ViewModels;
using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Validators.Tests;

namespace CES.CoreApi.Receipt_Main.Services.Tests
{
    [TestClass()]
    public class CAFServiceTests
    {
        [TestMethod()]
        public void CreateCAFNullTest()
        {
            var service = new CAFService();
            var request = new TaxCreateCAFRequest();
            service.CreateCAF(request);

            Assert.IsFalse(service.Successful);
        }

        [TestMethod()]
        public void CreateCAFRealTest()
        {
            var service = new CAFService();
            var request = new TaxCreateCAFRequest();

            request.CAFContent = CAFValidatorTests.GetDataToTest();

            service.CreateCAF(request);

            Assert.IsTrue(service.Successful);
        }
    }
}