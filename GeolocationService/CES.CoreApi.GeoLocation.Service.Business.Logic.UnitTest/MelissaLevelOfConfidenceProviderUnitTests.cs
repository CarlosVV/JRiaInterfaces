using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class MelissaLevelOfConfidenceProviderUnitTests
    {
        private const string ResponseCodes = "AC02,AC13,AC16,AV25,GS05";
        private const string ResponseCodesWithoutAv = "AC02,AC13,AC16,GS05";

        private const string ResponseCodesAv23 = "AC02,AC13,AC16,AV23,GS05";
        private const string ResponseCodesAv24 = "AC02,AC13,AC16,AV24,GS05";
        private const string ResponseCodesAv25 = "AC02,AC13,AC16,AV25,GS05";
        
        private const string ResponseCodesAv14 = "AC02,AC13,AC16,AV14,GS05";
        private const string ResponseCodesAv15 = "AC02,AC13,AC16,AV15,GS05";
        private const string ResponseCodesAv21 = "AC02,AC13,AC16,AV21,GS05";
        private const string ResponseCodesAv22 = "AC02,AC13,AC16,AV22,GS05";

        private const string ResponseCodesAv11 = "AC02,AC13,AC16,AV11,GS05";
        private const string ResponseCodesAv12 = "AC02,AC13,AC16,AV12,GS05";
        private const string ResponseCodesAv13 = "AC02,AC13,AC16,AV13,GS05";


        [TestMethod]
        public void GetLevelOfConfidence_ResponseCodesIsNull_NotFoundReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(string.Empty);
            Assert.AreEqual(LevelOfConfidence.NotFound, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_ResponseCodeExists_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodes);
            Assert.AreEqual(LevelOfConfidence.High, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_NoVerificationResponseCodeInList_NotFoundReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesWithoutAv);

            Assert.AreEqual(LevelOfConfidence.NotFound, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV25_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv25);
            Assert.AreEqual(LevelOfConfidence.High, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV24_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv24);
            Assert.AreEqual(LevelOfConfidence.High, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV23_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv23);
            Assert.AreEqual(LevelOfConfidence.High, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV14_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv14);
            Assert.AreEqual(LevelOfConfidence.Medium, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV15_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv15);
            Assert.AreEqual(LevelOfConfidence.Medium, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV21_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv21);
            Assert.AreEqual(LevelOfConfidence.Medium, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV22_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv22);
            Assert.AreEqual(LevelOfConfidence.Medium, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV11_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv11);
            Assert.AreEqual(LevelOfConfidence.Low, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV12_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv12);
            Assert.AreEqual(LevelOfConfidence.Low, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_AV13_HighReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence(ResponseCodesAv13);
            Assert.AreEqual(LevelOfConfidence.Low, result);
        }

        [TestMethod]
        public void GetLevelOfConfidence_InvalidAVCode_NotFoundReturned()
        {
            var result = new MelissaLevelOfConfidenceProvider().GetLevelOfConfidence("AV150");
            Assert.AreEqual(LevelOfConfidence.NotFound, result);
        }
    }
}
