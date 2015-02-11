using System.Collections.Generic;
using System.Collections.ObjectModel;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.UnitTestTools
{
    public class TestModelsProvider
    {
        private const string Country = "US";
        private const string Address1 = "Address1";
        private const string Address2 = "Address2";
        private const string AdministrativeArea = "CA";
        private const string City = "Buena Park";
        private const string PostalCode = "90620";
        private const string FormattedAddress = "6565 Knott Ave., Buena Park, CA 90620";

        public static CountryConfiguration GetUsCountryConfiguration(bool differentGeocodingProvider = false)
        {
            var configuration = new CountryConfiguration
            {
                CountryCode = Country,
                DataProviders = new List<DataProviderConfiguration>
                {
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.AddressVerification,
                        DataProviderType = DataProviderType.Bing,
                        Priority = 1
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.AddressVerification,
                        DataProviderType = DataProviderType.Google,
                        Priority = 2
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.AddressVerification,
                        DataProviderType = DataProviderType.MelissaData,
                        Priority = 3
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.AddressAutoComplete,
                        DataProviderType = DataProviderType.Google,
                        Priority = 1
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.Geocoding,
                        DataProviderType = !differentGeocodingProvider ? DataProviderType.Bing : DataProviderType.Google,
                        Priority = 1
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.Geocoding,
                        DataProviderType = DataProviderType.Google,
                        Priority = 2
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.Geocoding,
                        DataProviderType = DataProviderType.MelissaData,
                        Priority = 3
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.Mapping,
                        DataProviderType = DataProviderType.Google,
                        Priority = 1
                    }
                }
            };
            return configuration;
        }

        public static CountryConfiguration GetDefaultCountryConfiguration(bool differentGeocodingProvider = false)
        {
            var configuration = new CountryConfiguration
            {
                CountryCode = "Default",
                DataProviders = new List<DataProviderConfiguration>
                {
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.AddressVerification,
                        DataProviderType = DataProviderType.Bing,
                        Priority = 1
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.AddressVerification,
                        DataProviderType = DataProviderType.Google,
                        Priority = 2
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.AddressVerification,
                        DataProviderType = DataProviderType.MelissaData,
                        Priority = 3
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.AddressAutoComplete,
                        DataProviderType = DataProviderType.Google,
                        Priority = 1
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.Geocoding,
                        DataProviderType = DataProviderType.Bing,
                        Priority = 1
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.Geocoding,
                        DataProviderType = DataProviderType.Google,
                        Priority = 2
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.Geocoding,
                        DataProviderType = DataProviderType.MelissaData,
                        Priority = 3
                    },
                    new DataProviderConfiguration
                    {
                        DataProviderServiceType = DataProviderServiceType.Mapping,
                        DataProviderType = DataProviderType.Google,
                        Priority = 1
                    }
                }
            };
            return configuration;
        }

        public static DataProviderServiceConfiguration GetCountryConfigurations()
        {
            return new DataProviderServiceConfiguration
            {
                CountryConfigurations = new List<CountryConfiguration>
                {
                    GetUsCountryConfiguration(),
                    GetDefaultCountryConfiguration()
                }
            };
        }

        public static CountryConfiguration GetCountryConfigurationWithoutProviders()
        {
            var configuration = new CountryConfiguration
            {
                CountryCode = Country,
                DataProviders = new List<DataProviderConfiguration>()
            };
            return configuration;
        }

        public static AddressModel GetAddressModel()
        {
            return new AddressModel
            {
                Address1 = Address1,
                Address2 = Address2,
                AdministrativeArea = AdministrativeArea,
                Country = Country,
                City = City,
                FormattedAddress = FormattedAddress,
                PostalCode = PostalCode
            };
        }

        public static ICollection<PushPinModel> GetPushPins(int? iconStyle = 1, string label = "1", Color pinColor = Color.Green)
        {
            return new Collection<PushPinModel>
            {
                new PushPinModel
                {
                    IconStyle = iconStyle,
                    Label = label,
                    Location = new LocationModel
                    {
                        Latitude = 35.4567,
                        Longitude = -100.5678
                    },
                    PinColor = pinColor
                },
                new PushPinModel
                {
                    IconStyle = 2,
                    Label = "2",
                    Location = new LocationModel
                    {
                        Latitude = 36.4567,
                        Longitude = -101.5678
                    },
                    PinColor = Color.Red
                }
            };
        }
    }
}
