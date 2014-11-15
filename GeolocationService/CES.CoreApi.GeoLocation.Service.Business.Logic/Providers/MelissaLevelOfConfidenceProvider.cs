using System;
using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class MelissaLevelOfConfidenceProvider : IMelissaLevelOfConfidenceProvider
    {
        #region Core

        private readonly Dictionary<int, LevelOfConfidence> _confidenceMapping;
        private const string VerificationCodePrefix = "AV";

        public MelissaLevelOfConfidenceProvider()
        {
            _confidenceMapping = new Dictionary<int, LevelOfConfidence>
            {
                {11, LevelOfConfidence.Low},
                {12, LevelOfConfidence.Low},
                {13, LevelOfConfidence.Low},
                {14, LevelOfConfidence.Medium},
                {15, LevelOfConfidence.Medium},
                {21, LevelOfConfidence.Medium},
                {22, LevelOfConfidence.Medium},
                {23, LevelOfConfidence.High},
                {24, LevelOfConfidence.High},
                {25, LevelOfConfidence.High}
            };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets confidence level based on AV response verification code
        /// </summary>
        /// <param name="responseCodes"></param>
        /// <returns></returns>
        public LevelOfConfidence GetLevelOfConfidence(string responseCodes)
        {
            if (string.IsNullOrEmpty(responseCodes))
                return LevelOfConfidence.NotFound;

            var codes = responseCodes.Split(',');
            var code = codes.FirstOrDefault(
                p => p.StartsWith(VerificationCodePrefix, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(code))
                return LevelOfConfidence.NotFound;

            var codeValue = code.Replace(VerificationCodePrefix, string.Empty).ConvertValue<int>();

            return !_confidenceMapping.ContainsKey(codeValue)
                ? LevelOfConfidence.NotFound
                : _confidenceMapping[codeValue];
        }

        #endregion
    }
}
