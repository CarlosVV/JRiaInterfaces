using System.Collections.ObjectModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using CES.CoreApi.GeoLocation.Service.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.UnitTest
{
    [TestClass]
    public class RequestValidatorUnitTests
    {
        private const string Address1 = "Address1";
        private const string Address2 = "Address2";
        private const string ValidCountry = "US";
        private const string InValidCountry = "USA";
        private const string AdministrativeArea = "CA";
        private const string City = "Buena Park";
        private const string PostalCode = "90620";
        private const int MaxRecords = 15;
        private const double LatitudeValid = 36.3456;
        private const double LatitudeInValidNegative = -90.876;
        private const double LatitudeInValidPositive = 95.765;
        private const double LongitudeValid = -170.5453;
        private const double LongitudeInValidNegative = -180.5453;
        private const double LongitudeInValidPositive = 180.5453;
        private const int ZoomLevelValid = 10;
        private const int ZoomLevelMinimum = 0;
        private const int ZoomLevelMaximum = 21;
        private const int Width = 100;
        private const int Height = 100;
        private const string Message = "Some message";

        #region ValidateAddressRequest test methods

        [TestMethod]
        public void ValidateAddressRequest_Success_NoExceptionRaised()
        {
            var request = new ValidateAddressRequest
            {
                Address = new AddressRequest
                {
                    Address1 = Address1,
                    Country = ValidCountry
                },
                MinimumConfidence = Confidence.High
            };

            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void ValidateAddressRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(ValidateAddressRequest)),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        [TestMethod]
        public void ValidateAddressRequest_AddressIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(new ValidateAddressRequest()),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Address");
        }

        [TestMethod]
        public void ValidateAddressRequest_CountryIsEmpty_ExceptionRaised()
        {
            var request = new ValidateAddressRequest
            {
                Address = new AddressRequest
                {
                    Address1 = Address1
                },
                MinimumConfidence = Confidence.High
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.Country");
        }

        [TestMethod]
        public void ValidateAddressRequest_CountryHasInvalidLength_ExceptionRaised()
        {
            var request = new ValidateAddressRequest
            {
                Address = new AddressRequest
                {
                    Address1 = Address1,
                    Country = InValidCountry
                },
                MinimumConfidence = Confidence.High
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralInvalidParameterValue, "request.Country", request.Address.Country);
        }

        [TestMethod]
        public void ValidateAddressRequest_Address1IsEmpty_ExceptionRaised()
        {
            var request = new ValidateAddressRequest
            {
                Address = new AddressRequest
                {
                    Country = ValidCountry
                },
                MinimumConfidence = Confidence.High
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Address.Address1");
        }

        [TestMethod]
        public void ValidateAddressRequest_MinimumConfidenceIsInvalid_ExceptionRaised()
        {
            var request = new ValidateAddressRequest
            {
                Address = new AddressRequest
                {
                    Country = ValidCountry,
                    Address1 = Address1
                },
                MinimumConfidence = Confidence.Undefined
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralInvalidParameterValue, "request.MinimumConfidence", request.MinimumConfidence);
        }

        #endregion

        #region ValidateFormattedAddressRequest test methods

        [TestMethod]
        public void ValidateFormattedAddressRequest_Success_NoExceptionRaised()
        {
            var request = new ValidateFormattedAddressRequest
            {
                FormattedAddress = Address1,
                MinimumConfidence = Confidence.High,
                Country = ValidCountry
            };

            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void ValidateFormattedAddressRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(ValidateFormattedAddressRequest)),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        [TestMethod]
        public void ValidateFormattedAddressRequest_FormattedAddressIsEmpty_ExceptionRaised()
        {
            var request = new ValidateFormattedAddressRequest
            {
                MinimumConfidence = Confidence.High,
                Country = ValidCountry
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.FormattedAddress");
        }

        [TestMethod]
        public void ValidateFormattedAddressRequest_CountryIsEmpty_ExceptionRaised()
        {
            var request = new ValidateFormattedAddressRequest
            {
                FormattedAddress = Address1,
                MinimumConfidence = Confidence.High
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                 SubSystemError.GeneralRequiredParameterIsUndefined, "request.Country");
        }

        [TestMethod]
        public void ValidateFormattedAddressRequest_CountryHasInvalidLength_ExceptionRaised()
        {
            var request = new ValidateFormattedAddressRequest
            {
                FormattedAddress = Address1,
                MinimumConfidence = Confidence.High,
                Country = InValidCountry
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralInvalidParameterValue, "request.Country", request.Country);
        }

        [TestMethod]
        public void ValidateFormattedAddressRequest_MinimumConfidenceIsInvalid_ExceptionRaised()
        {
            var request = new ValidateFormattedAddressRequest
            {
                FormattedAddress = Address1,
                MinimumConfidence = Confidence.Undefined,
                Country = ValidCountry
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralInvalidParameterValue, "request.MinimumConfidence", request.MinimumConfidence);
        }

        #endregion

        #region AutocompleteAddressRequest test methods

        [TestMethod]
        public void AutocompleteAddressRequest_Success_NoExceptionRaised()
        {
            var request = new AutocompleteAddressRequest
            {
                Address = Address1,
                Country = ValidCountry,
                AdministrativeArea = AdministrativeArea,
                MaxRecords = MaxRecords,
                MinimumConfidence = Confidence.High
            };

            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void AutocompleteAddressRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(AutocompleteAddressRequest)),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        [TestMethod]
        public void AutocompleteAddressRequest_AddressIsEmpty_ExceptionRaised()
        {
            var request = new AutocompleteAddressRequest
            {
                Country = ValidCountry,
                AdministrativeArea = AdministrativeArea,
                MaxRecords = MaxRecords
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Address");
        }
        
        #endregion

        #region GeocodeAddressRequest test methods

        [TestMethod]
        public void GeocodeAddressRequest_Success_NoExceptionRaised()
        {
            var request = new GeocodeAddressRequest
            {
                Address = new AddressRequest
                {
                    Address1 = Address1,
                    Address2 = Address2,
                    AdministrativeArea = AdministrativeArea,
                    City = City,
                    Country = ValidCountry,
                    PostalCode = PostalCode
                },
                MinimumConfidence = Confidence.High
            };
            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void GeocodeAddressRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(GeocodeAddressRequest)),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        #endregion

        #region GeocodeFormattedAddressRequest test methods

        [TestMethod]
        public void GeocodeFormattedAddressRequest_Success_NoExceptionRaised()
        {
            var request = new GeocodeFormattedAddressRequest
            {
                FormattedAddress = Address1,
                Country = ValidCountry,
                MinimumConfidence = Confidence.High
            };
            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void GeocodeFormattedAddressRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(GeocodeFormattedAddressRequest)),
              SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        #endregion

        #region ReverseGeocodePointRequest test methods

        [TestMethod]
        public void ReverseGeocodePointRequest_Success_NoExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Location = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                Country = ValidCountry,
                MinimumConfidence = Confidence.High
            };

            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(ReverseGeocodePointRequest)),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_LocationIsNull_ExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Country = ValidCountry,
                MinimumConfidence = Confidence.High
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Location");
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_LatitudeIsInvalidNegative_ExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Location = new Location
                {
                    Latitude = LatitudeInValidNegative,
                    Longitude = LongitudeValid
                },
                Country = ValidCountry,
                MinimumConfidence = Confidence.High
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralInvalidParameterValue, "request.Location.Latitude", request.Location.Latitude);
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_LatitudeIsInvalidPositive_ExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Location = new Location
                {
                    Latitude = LatitudeInValidPositive,
                    Longitude = LongitudeValid
                },
                Country = ValidCountry,
                MinimumConfidence = Confidence.High
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.Location.Latitude", request.Location.Latitude);
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_LongitudeIsInvalidNegative_ExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Location = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeInValidNegative
                },
                Country = ValidCountry,
                MinimumConfidence = Confidence.High
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
              SubSystemError.GeneralInvalidParameterValue, "request.Location.Longitude", request.Location.Longitude);
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_LongitudeIsInvalidPositive_ExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Location = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeInValidPositive
                },
                Country = ValidCountry,
                MinimumConfidence = Confidence.High
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.Location.Longitude", request.Location.Longitude);
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_MinimumConfidenceIsInvalid_ExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Location = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                Country = ValidCountry,
                MinimumConfidence = Confidence.Undefined
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.MinimumConfidence", request.MinimumConfidence);
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_CountryIsInvalid_ExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Location = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                Country = InValidCountry,
                MinimumConfidence = Confidence.Undefined
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.Country", request.Country);
        }

        [TestMethod]
        public void ReverseGeocodePointRequest_CountryIsNull_ExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest
            {
                Location = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MinimumConfidence = Confidence.Undefined
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.Country");
        }

        #endregion

        #region GetMapRequest test methods

        [TestMethod]
        public void GetMapRequest_Success_NoExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };
            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void GetMapRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(GetMapRequest)),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        [TestMethod]
        public void GetMapRequest_CenterIsNull_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.Center");
        }

        [TestMethod]
        public void GetMapRequest_LatitudeIsInvalidNegative_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeInValidNegative,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.Center.Latitude", LatitudeInValidNegative);
        }

        [TestMethod]
        public void GetMapRequest_LatitudeIsInvalidPositive_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeInValidPositive,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                           SubSystemError.GeneralInvalidParameterValue, "request.Center.Latitude", LatitudeInValidPositive);
        }

        [TestMethod]
        public void GetMapRequest_LongitudeIsInvalidNegative_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeInValidNegative
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralInvalidParameterValue, "request.Center.Longitude", LongitudeInValidNegative);
        }

        [TestMethod]
        public void GetMapRequest_LongitudeIsInvalidPositive_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeInValidPositive
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.Center.Longitude", LongitudeInValidPositive);
        }

        [TestMethod]
        public void GetMapRequest_MapSizeIsNull_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.MapSize");
        }

        [TestMethod]
        public void GetMapRequest_MapSizeWidthIsZero_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                },
                MapSize = new MapSize
                {
                    Width = 0,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.MapSize.Width", request.MapSize.Width);
        }

        [TestMethod]
        public void GetMapRequest_MapSizeHeightIsZero_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelValid
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = 0
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralInvalidParameterValue, "request.MapSize.Height", request.MapSize.Height);
        }

        [TestMethod]
        public void GetMapRequest_MapOutputParametersAreNull_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.MapOutputParameters");
        }

        [TestMethod]
        public void GetMapRequest_ZoomLevelLessThanMimimum_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelMinimum - 1
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.MapOutputParameters.ZoomLevel", request.MapOutputParameters.ZoomLevel);
        }

        [TestMethod]
        public void GetMapRequest_ZoomLevelMoreThanMaximum_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelMaximum + 1
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.MapOutputParameters.ZoomLevel", request.MapOutputParameters.ZoomLevel);
        }

        [TestMethod]
        public void GetMapRequest_CountryIsNull_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelMaximum
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.Country");
        }

        [TestMethod]
        public void GetMapRequest_CountryHasInvalidLength_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = InValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelMaximum
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralInvalidParameterValue, "request.Country", InValidCountry);
        }

        [TestMethod]
        public void GetMapRequest_PushpinHasValidLocation_HappyPath()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelMaximum
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                },
                PushPins = new Collection<PushPin>
                {
                    new PushPin
                    {
                        IconStyle = 1,
                        Label = "2",
                        Location = new Location
                        {
                            Latitude = 35.4567,
                            Longitude = -100.5678
                        },
                        PinColor = PinColor.Blue
                    }
                }
            };

            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void GetMapRequest_PushpinLocationIsNull_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelMaximum
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                },
                PushPins = new Collection<PushPin>
                {
                    new PushPin
                    {
                        IconStyle = 1,
                        Label = "2",
                        Location = null,
                        PinColor = PinColor.Blue
                    }
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralRequiredParameterIsUndefined, "pushPin.Location");
        }

        [TestMethod]
        public void GetMapRequest_PushpinIsNull_ExceptionRaised()
        {
            var request = new GetMapRequest
            {
                Country = ValidCountry,
                Center = new Location
                {
                    Latitude = LatitudeValid,
                    Longitude = LongitudeValid
                },
                MapOutputParameters = new MapOutputParameters
                {
                    ZoomLevel = ZoomLevelMaximum
                },
                MapSize = new MapSize
                {
                    Width = Width,
                    Height = Height
                },
                PushPins = new Collection<PushPin>
                {
                    null
                }
            };

            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
                SubSystemError.GeneralRequiredParameterIsUndefined, "pushPin");
        }

        #endregion

        #region GetProviderKeyRequest test methods

        [TestMethod]
        public void GetProviderKeyRequest_Success_NoExceptionRaised()
        {
            var request = new GetProviderKeyRequest
            {
                DataProvider = DataProvider.Google
            };
            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void GetProviderKeyRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(GetProviderKeyRequest)),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        [TestMethod]
        public void GetProviderKeyRequest_DataProviderIsUndefined_ExceptionRaised()
        {
            var request = new GetProviderKeyRequest
            {
                DataProvider = DataProvider.Undefined
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.DataProvider");
        }

        #endregion

        #region LogEventRequest test methods

        [TestMethod]
        public void LogEventRequest_Success_NoExceptionRaised()
        {
            var request = new LogEventRequest
            {
                DataProvider = DataProvider.Google,
                Message = Message
            };
            ExceptionHelper.CheckHappyPath(() => new RequestValidator().Validate(request));
        }

        [TestMethod]
        public void LogEventRequest_RequestIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(default(LogEventRequest)),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        [TestMethod]
        public void LogEventRequest_DataProviderIsUndefined_ExceptionRaised()
        {
            var request = new LogEventRequest
            {
                DataProvider = DataProvider.Undefined,
                Message = Message
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.DataProvider");
        }

        [TestMethod]
        public void LogEventRequest_MessageIsNullOrEmpty_ExceptionRaised()
        {
            var request = new LogEventRequest
            {
                DataProvider = DataProvider.Google,
                Message = string.Empty
            };
            ExceptionHelper.CheckException(() => new RequestValidator().Validate(request),
               SubSystemError.GeneralRequiredParameterIsUndefined, "request.Message");
        }

        #endregion
    }
}

