using CES.CoreApi.CustomerVerification.Business.Contract.Configuration;

namespace CES.CoreApi.CustomerVerification.Business.Contract.Interfaces
{
    public interface ICountryConfigurationProvider
    {
        CountryConfiguration GetConfiguration(string countryCode);
    }
}