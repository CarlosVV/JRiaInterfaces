using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers
{
    public abstract class BaseDataResponseParser
    {
        #region Core

        protected BaseDataResponseParser(DataProviderType providerType)
        {
            ProviderType = providerType;
        }

        #endregion

        #region Protected methods

        protected IEnumerable<int> GetAcceptableConfidenceLevels(LevelOfConfidence acceptableConfidence)
        {
            var fullList = EnumTools.GetValues<LevelOfConfidence, int>();
            return fullList.Where(p => p >= (int) acceptableConfidence);
        }
        
        protected DataProviderType ProviderType { get; private set; }

        protected AutocompleteAddressResponseModel GetInvalidAddressAutocompleteResponse()
        {
            return new AutocompleteAddressResponseModel
            {
                DataProvider = ProviderType,
                IsValid = false
            };
        }

        protected AutocompleteAddressResponseModel GetValidAddressAutocompleteResponse()
        {
            return new AutocompleteAddressResponseModel
            {
                DataProvider = ProviderType,
                IsValid = true
            };
        }

        /// <summary>
        /// Gets failed response
        /// </summary>
        /// <returns></returns>
        protected ValidateAddressResponseModel GetInvalidAddressVerificationResponse()
        {
            return new ValidateAddressResponseModel
            {
                Confidence = LevelOfConfidence.NotFound,
                IsValid = false,
                AddressValidationProvider = ProviderType,
                GeocodingProvider = ProviderType
            };
        }

        protected ValidateAddressResponseModel GetValidAddressVerificationResponse(LevelOfConfidence confidence)
        {
            return new ValidateAddressResponseModel
            {
                Confidence = confidence,
                IsValid = true,
                AddressValidationProvider = ProviderType,
            };
        }


        /// <summary>
        /// Gets failed response
        /// </summary>
        /// <returns></returns>
        protected GeocodeAddressResponseModel GetInvalidGeocodeAddressResponse()
        {
            return new GeocodeAddressResponseModel
            {
                Confidence = LevelOfConfidence.NotFound,
                IsValid = false,
                DataProvider = ProviderType
            };
        }

        protected GeocodeAddressResponseModel GetValidGeocodeAddressResponse(LevelOfConfidence confidence)
        {
            return new GeocodeAddressResponseModel
            {
                Confidence = confidence,
                IsValid = true,
                DataProvider = ProviderType
            };
        }

        protected GetMapResponseModel GetInvalidMapResponse()
        {
            return new GetMapResponseModel
            {
                IsValid = false,
                DataProvider = ProviderType
            };
        }

        protected GetMapResponseModel GetValidMapResponse(byte[] mapData)
        {
            return new GetMapResponseModel
            {
                IsValid = true,
                DataProvider = ProviderType,
                MapData = mapData
            };
        }

        #endregion
    }
}
