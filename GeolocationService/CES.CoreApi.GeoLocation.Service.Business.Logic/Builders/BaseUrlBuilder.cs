using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.Configuration.Provider;
namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Builders
{
    public abstract class BaseUrlBuilder 
    {
        protected BaseUrlBuilder(IConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService, SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");

            ConfigurationProvider = configurationProvider;
			//LicenseKey = GeoLocationConfigurationSection.Instance.Bing.Key;
		  //  LicenseKey = ConfigurationProvider.Read<string>(lisenceKeyConfigurationName);

			//This is temporary hack since google does not require license
			//if (providerType == DataProviderType.Google)
   //             return;

   //         if (string.IsNullOrEmpty(LicenseKey))
   //             throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
   //                 SubSystemError.GeolocationLicenseKeyNotFound,
   //                 providerType);
        }

        protected IConfigurationProvider ConfigurationProvider { get; private set; }

      //  protected string LicenseKey { get; private set; }
    }
}
