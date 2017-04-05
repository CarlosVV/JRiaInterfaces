using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Receipt_Main.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Receipt_Main.Validators.Tests;
using CES.CoreApi.Receipt_Main.App_Start;
using CES.CoreApi.Receipt_Main.ExceptionHandling;
using CES.CoreApi.Receipt_Main.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Receipt_Main.Controllers.Tests
{
    [TestClass()]
    public class TaxControllerTests
    {
        [TestInitialize()]
        public void InitTest()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
            ///*To client application auth*/
            //GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.Receipt_Main"));
            ///*To return custom error messages*/
            ////GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute());
            ///*To capture htto request message and http response message: You comment out this line if you want to stop it*/
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());
            ///*To Register Mapper*/
            AutoMapperConfig.RegisterMappings();

            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);
        }
        [TestMethod()]
        public void PingServerTest()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod()]
        public void PostCreateCAFTest()
        {
            var controller = new TaxController();
            var request = new ViewModels.ServiceTaxCreateCAFRequestViewModel
            {
                CAFContent = CAFValidatorTests.GetDataToTest()
            };
            var response = controller.PostCreateCAF(request);
            Assert.IsNotNull(response);
        }
    }
}