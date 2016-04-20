using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface ICountryConfigurationProvider
    {
        CountryConfiguration GetProviderConfigurationByCountry(string countryCode);

        /// <summary>
        /// Global application configuration provider instance
        /// </summary>
      //  IConfigurationProvider ConfigurationProvider { get; }
    }
}