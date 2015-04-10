using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Factories;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class ResponseParserFactoryUnitTests
    {
        private IResponseParserFactory _responseFactory;

        [TestInitialize]
        public void Setup()
        {
            _responseFactory = new ResponseParserFactory
            {
                {"IBingResponseParser", It.IsAny<BaseDataResponseParser>},
                {"IGoogleResponseParser", It.IsAny<BaseDataResponseParser>},
                {"IMelissaDataResponseParser", It.IsAny<BaseDataResponseParser>}
            };
        }

        //[TestMethod]
        //public void GetInstance_BingResponseParser_HappyPath()
        //{
        //    var result = _responseFactory.GetInstance<IResponseParser>(DataProviderType.Bing);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(IResponseParser));
        //}
    }
}
