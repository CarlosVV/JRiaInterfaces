//using CES.CoreApi.Caching.Providers;
//using CES.CoreApi.Common.Managers;
//using CES.CoreApi.Common.Models;
//using CES.CoreApi.Foundation.Contract.Models;
//using CES.CoreApi.Foundation.Data.Providers;
//using CES.CoreApi.Logging.Factories;
//using CES.CoreApi.Payout.Service.Business.Contract.Models;
//using CES.CoreApi.Payout.Service.Business.Logic.Data;
//using CES.CoreApi.Payout.Service.Business.Logic.Providers.RiaDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace PayoutService.Test.UnitTests
{
    [TestClass]
    class CurrencyCode_Tests
    {

        //private const string USD_CUR = "USD";
        //private const int USD_ISO = 986;
        //private DataHelper _dataHelper;


        //[TestMethod]
        //public void CurrencyCode_USD()
        //{
        //    try
        //    {
        //        //Log Monitor Factory
        //        LogMonitorFactory lmf = new LogMonitorFactory();

        //        //Identity Manager:
        //        IdentityManager identMan = new IdentityManager();
        //        Application app = new Application(501, "PayoutService", true);
        //        ServiceCallHeaderParameters hList = new ServiceCallHeaderParameters(501, "", DateTime.UtcNow, "ABC123456", "32456", "TransactionID", "0");
        //        ClientApplicationIdentity clientIdent = new ClientApplicationIdentity(app, hList);
        //        ApplicationPrincipal appPrinc = new ApplicationPrincipal(clientIdent);
        //        identMan.SetCurrentPrincipal(appPrinc);

        //        //Cache Provider:
        //        RedisCacheProvider cacheProv = new RedisCacheProvider(lmf, identMan);

        //        //Database Instance:
        //        DatabaseConfigurationProvider dbConfProv = new DatabaseConfigurationProvider();
        //        DatabaseInstanceProvider dbInst = new DatabaseInstanceProvider(dbConfProv);

        //        RiaRepository repository = new RiaRepository(
        //            cacheProv,
        //            lmf,
        //            identMan,
        //            dbInst);
                
        //        _dataHelper = new DataHelper(repository);
        //        CurrencyCodeModel cc = _dataHelper.GetCurrencyCode(USD_CUR);

        //        Assert.AreEqual(USD_CUR, cc.IsoCodeText);
        //        Assert.AreEqual(USD_ISO, cc.IsoCodeNum);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}



    }
}