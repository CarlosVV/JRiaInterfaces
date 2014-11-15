using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class GoogleLevelOfConfidenceProviderUnitTests
    {
        private const string Rooftop = "ROOFTOP";
        private const string RangeInterpolated = "RANGE_INTERPOLATED";
        private const string GeometricCenter = "GEOMETRIC_CENTER";
        private const string Approximate = "APPROXIMATE";
        private const string UnknownLocationType = "UnknownLocationType";

        [TestMethod]
        public void GetLevelOfConfidence_Rooftop_HighReturned()
        {
            var result = new GoogleLevelOfConfidenceProvider().GetLevelOfConfidence(Rooftop);

            Assert.AreEqual(LevelOfConfidence.High, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_RangeInterpolated_MediumRreturned()
        {
            var result = new GoogleLevelOfConfidenceProvider().GetLevelOfConfidence(RangeInterpolated);

            Assert.AreEqual(LevelOfConfidence.Medium, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_GeometricCenter_MediumRreturned()
        {
            var result = new GoogleLevelOfConfidenceProvider().GetLevelOfConfidence(GeometricCenter);

            Assert.AreEqual(LevelOfConfidence.Medium, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_Approximate_LowReturned()
        {
            var result = new GoogleLevelOfConfidenceProvider().GetLevelOfConfidence(Approximate);

            Assert.AreEqual(LevelOfConfidence.Low, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_EmptyLocationType_NotFoundReturned()
        {
            var result = new GoogleLevelOfConfidenceProvider().GetLevelOfConfidence(string.Empty);

            Assert.AreEqual(LevelOfConfidence.NotFound, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_UnknownLocationType_NotFoundReturned()
        {
            var result = new GoogleLevelOfConfidenceProvider().GetLevelOfConfidence(UnknownLocationType);

            Assert.AreEqual(LevelOfConfidence.NotFound, result);
        }
    }
}
