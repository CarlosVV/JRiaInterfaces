﻿using System;
using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Logic.Providers
{
    public class GoogleLevelOfConfidenceProvider : IGoogleLevelOfConfidenceProvider
    {
        #region Core

        private readonly Dictionary<string, LevelOfConfidence> _confidenceMapping;

        public GoogleLevelOfConfidenceProvider()
        {
            _confidenceMapping = new Dictionary<string, LevelOfConfidence>(StringComparer.OrdinalIgnoreCase)
            {
                {"ROOFTOP", LevelOfConfidence.High},
                {"RANGE_INTERPOLATED", LevelOfConfidence.Medium},
                {"GEOMETRIC_CENTER", LevelOfConfidence.Medium},
                {"APPROXIMATE", LevelOfConfidence.Low}
            };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets confidence level based on location type field in response
        /// </summary>
        /// <param name="locationType"></param>
        /// <returns></returns>
        public LevelOfConfidence GetLevelOfConfidence(string locationType)
        {
            if (string.IsNullOrEmpty(locationType))
                return LevelOfConfidence.NotFound;

            return !_confidenceMapping.ContainsKey(locationType)
                ? LevelOfConfidence.NotFound
                : _confidenceMapping[locationType];
        }

        #endregion
    }
}
