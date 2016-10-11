using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PayoutService.Test.UnitTests
{
    [TestClass]
    class CountryCode_Tests
    {

        private const string US = "US";
        private const string USA = "USA";
        private const string UNITED_STATES = "United States";
        private const string MX = "MX";
        private const string MEX = "MEX";
        private const string MEXICO = "Mexico";


        [TestMethod]
        public void CountryCode_US()
        {
            Assert.AreEqual(1, "Need to implement these tests with new model.");
        }

        //[TestMethod]
        //public void CountryCode_US()
        //{
        //    try
        //    {
        //        CountryCode cc = CountryCode.S_CreateFrom2CharCode(US, false);
        //        Assert.AreEqual(US, cc.Get2CharCountryCode());
        //        Assert.AreEqual(USA, cc.Get3CharISOCountryCode());
        //        Assert.AreEqual(UNITED_STATES, cc.GetCountryDescription());
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}

        //[TestMethod]
        //public void CountryCode_USA()
        //{
        //    try
        //    {
        //        CountryCode cc = CountryCode.S_CreateFrom3CharCode(USA, false);
        //        Assert.AreEqual(US, cc.Get2CharCountryCode());
        //        Assert.AreEqual(USA, cc.Get3CharISOCountryCode());
        //        Assert.AreEqual(UNITED_STATES, cc.GetCountryDescription());
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}


        //[TestMethod]
        //public void CountryCode_MX()
        //{
        //    try
        //    {
        //        CountryCode cc = CountryCode.S_CreateFrom2CharCode(MX, false);
        //        Assert.AreEqual(MX, cc.Get2CharCountryCode());
        //        Assert.AreEqual(MEX, cc.Get3CharISOCountryCode());
        //        Assert.AreEqual(MEXICO, cc.GetCountryDescription());
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}

        //[TestMethod]
        //public void CountryCode_MEX()
        //{
        //    try
        //    {
        //        CountryCode cc = CountryCode.S_CreateFrom3CharCode(MEX, false);
        //        Assert.AreEqual(MX, cc.Get2CharCountryCode());
        //        Assert.AreEqual(MEX, cc.Get3CharISOCountryCode());
        //        Assert.AreEqual(MEXICO, cc.GetCountryDescription());
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}


    }
}
