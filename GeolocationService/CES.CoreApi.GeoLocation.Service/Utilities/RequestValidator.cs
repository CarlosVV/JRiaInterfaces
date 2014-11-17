using System.Collections.Generic;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Validation;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Utilities
{
    public class RequestValidator : IRequestValidator
    {
        private const int CountryFieldLength = 2;
        private const int MinZoomLevel = 0;
        private const int MaxZoomLevel = 21;
        private const int MinLatitude = -90;
        private const int MaxLatitude = 90;
        private const int MinLongitude = -180;
        private const int MaxLongitude = 180;
        private const int MaxNumberOfPushPins = 15;

        // ReSharper disable PossibleNullReferenceException

        #region Public methods

        public void Validate(ValidateAddressRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ValidateIncomingAddress(request.Address);
            ValidateRequestConfidence(request.MinimumConfidence);
        }

        public void Validate(ValidateFormattedAddressRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ValidateImcomingAddress(request.FormattedAddress, request.Country);
            ValidateRequestConfidence(request.MinimumConfidence);
        }

        public void Validate(AutocompleteAddressRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");

            ValidateCountry(request.Country);
            
            ContractValidation.Requires(!string.IsNullOrEmpty(request.Address), TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Address");
        }

        public void Validate(GeocodeAddressRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ValidateIncomingAddress(request.Address);
            ValidateRequestConfidence(request.MinimumConfidence);
        }

        public void Validate(GeocodeFormattedAddressRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ValidateImcomingAddress(request.FormattedAddress, request.Country);
            ValidateRequestConfidence(request.MinimumConfidence);
        }

        public void Validate(ReverseGeocodePointRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ValidateLocation(request.Location, "request.Location");
            ValidateCountry(request.Country);
            ValidateRequestConfidence(request.MinimumConfidence);
        }

        public void Validate(GetMapRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");

            ValidateLocation(request.Center, "request.Center");
            
            ContractValidation.Requires(request.MapSize != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.MapSize");
            ContractValidation.Requires(request.MapSize.Width > 0, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralInvalidParameterValue, "request.MapSize.Width", request.MapSize.Width);
            ContractValidation.Requires(request.MapSize.Height > 0, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralInvalidParameterValue, "request.MapSize.Height", request.MapSize.Height);
            
            ContractValidation.Requires(request.MapOutputParameters != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.MapOutputParameters");
            ContractValidation.Requires(
                request.MapOutputParameters.ZoomLevel >= MinZoomLevel &&
                request.MapOutputParameters.ZoomLevel <= MaxZoomLevel, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralInvalidParameterValue, "request.MapOutputParameters.ZoomLevel",
                request.MapOutputParameters.ZoomLevel);

            ValidateCountry(request.Country);

            ValidatePushPins(request.PushPins);
        }

        public void Validate(GetProviderKeyRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.DataProvider != DataProvider.Undefined, TechnicalSubSystem.GeoLocationService,
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.DataProvider");
        }

        public void Validate(LogEventRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.DataProvider != DataProvider.Undefined, TechnicalSubSystem.GeoLocationService,
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.DataProvider");
            ContractValidation.Requires(!string.IsNullOrEmpty(request.Message.Trim()), TechnicalSubSystem.GeoLocationService,
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.Message");
        }

        #endregion

        #region Private methods

        private static void ValidateIncomingAddress(AddressRequest address)
        {
            ContractValidation.Requires(address != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Address");
            
            ValidateCountry(address.Country);
            
            ContractValidation.Requires(!string.IsNullOrEmpty(address.Address1), TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Address.Address1");
        }

        private static void ValidateImcomingAddress(string address, string country)
        {
            ContractValidation.Requires(!string.IsNullOrEmpty(address), TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.FormattedAddress");

            ValidateCountry(country);
        }
        
        private static void ValidateRequestConfidence(Confidence confidence)
        {
            ContractValidation.Requires(
                confidence == Confidence.Low ||
                confidence == Confidence.Medium ||
                confidence == Confidence.High, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralInvalidParameterValue, "request.MinimumConfidence",
                confidence);
        }

        private static void ValidateCountry(string country)
        {
            ContractValidation.Requires(!string.IsNullOrEmpty(country), TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Country");
            ContractValidation.Requires(country.Length == CountryFieldLength,
                TechnicalSubSystem.GeoLocationService, SubSystemError.GeneralInvalidParameterValue, "request.Country",
                country);
        }
        
        private static void ValidateLocation(Location location, string prefix)
        {
            ContractValidation.Requires(location != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, prefix);

            ContractValidation.Requires(location.Latitude >= MinLatitude && 
                                        location.Latitude <= MaxLatitude, 
                                        TechnicalSubSystem.GeoLocationService, 
                                        SubSystemError.GeneralInvalidParameterValue, 
                                        prefix + ".Latitude", 
                                        location.Latitude);

            ContractValidation.Requires(location.Longitude >= MinLongitude && 
                                        location.Longitude <= MaxLongitude, 
                                        TechnicalSubSystem.GeoLocationService,
                                        SubSystemError.GeneralInvalidParameterValue, 
                                        prefix + ".Longitude", 
                                        location.Longitude);
        }
        
        private static void ValidatePushPins(ICollection<PushPin> pushPins)
        {
            if (pushPins == null)
                return;

            ContractValidation.Requires(pushPins.Count <= MaxNumberOfPushPins,
                                        TechnicalSubSystem.GeoLocationService,
                                        SubSystemError.GeneralInvalidParameterValue,
                                        "pushPins.Count",
                                        pushPins.Count);

            foreach (var pushPin in pushPins)
            {
                ValidatePushPin(pushPin);
            }
        }

        private static void ValidatePushPin(PushPin pushPin)
        {
            ContractValidation.Requires(pushPin != null, TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "pushPin");
            ValidateLocation(pushPin.Location, "pushPin.Location");
        }

        #endregion

        // ReSharper restore PossibleNullReferenceException
    }
}
