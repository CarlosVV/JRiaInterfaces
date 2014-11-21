using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Providers;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Tools;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Factories;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Utilities;
using CES.CoreApi.Logging.Configuration;

namespace CES.CoreApi.GeoLocation.Service.Configuration
{
    public class IocContainerConfigurator
    {
        public static void RegisterTypes(IIocContainer container)
        {
            container
                .RegisterTypeWithVirtualMethodInterceptor<IAddressService, GeoLocationService>(InterceptionBehaviorType.Performance)
                .RegisterTypeWithVirtualMethodInterceptor<IGeocodeService, GeoLocationService>(InterceptionBehaviorType.Performance)
                .RegisterTypeWithVirtualMethodInterceptor<IMapService, GeoLocationService>(InterceptionBehaviorType.Performance)
                .RegisterTypeWithVirtualMethodInterceptor<IHealthMonitoringService, GeoLocationService>(InterceptionBehaviorType.Performance)
                .RegisterTypeWithVirtualMethodInterceptor<IClientSideSupportService, GeoLocationService>(InterceptionBehaviorType.Performance)

                .RegisterType<IHealthMonitoringProcessor, HealthMonitoringProcessor>()
                .RegisterType<IAddressServiceRequestProcessor, AddressServiceRequestProcessor>()
                .RegisterType<IGeocodeServiceRequestProcessor, GeocodeServiceRequestProcessor>()
                .RegisterType<IMapServiceRequestProcessor, MapServiceRequestProcessor>()
                .RegisterType<IClientSideSupportServiceProcessor, ClientSideSupportServiceProcessor>()

                .RegisterType<IEntityFactory, EntityFactory>(container)

                .RegisterType<IUrlBuilder, BingUrlBuilder>("Bing_UrlBuilder")
                .RegisterType<IUrlBuilder, GoogleUrlBuilder>("Google_UrlBuilder")
                .RegisterType<IUrlBuilder, MelissaUrlBuilder>("MelissaData_UrlBuilder")

                .RegisterType<IResponseParser, BingResponseParser>("Bing_ResponseParser")
                .RegisterType<IResponseParser, GoogleResponseParser>("Google_ResponseParser")
                .RegisterType<IResponseParser, MelissaResponseParser>("MelissaData_ResponseParser")

                .RegisterType<IAddressVerificationDataProvider, AddressVerificationDataProvider>()
                .RegisterType<ICountryConfigurationProvider, CountryConfigurationProvider>()
                .RegisterType<IAddressVerificationDataProvider, AddressVerificationDataProvider>()
                .RegisterType<IMappingDataProvider, MappingDataProvider>()
                .RegisterTypeWithInterfaceInterceptor<IDataResponseProvider, DataResponseProvider>(InterceptionBehaviorType.Performance)
                .RegisterType<IMelissaLevelOfConfidenceProvider, MelissaLevelOfConfidenceProvider>()
                .RegisterType<IGoogleLevelOfConfidenceProvider, GoogleLevelOfConfidenceProvider>()
                .RegisterType<ICurrentDateTimeProvider, CurrentDateTimeProvider>()
                .RegisterType<IAddressAutocompleteDataProvider, AddressAutocompleteDataProvider>()
                .RegisterType<IGeocodeAddressDataProvider, GeocodeAddressDataProvider>()
                .RegisterType<IRegistrationNameProvider, RegistrationNameProvider>()
                .RegisterType<IBingPushPinParameterProvider, BingPushPinParameterProvider>()
                .RegisterType<IGooglePushPinParameterProvider, GooglePushPinParameterProvider>()
                .RegisterType<ICorrectImageSizeProvider, CorrectImageSizeProvider>()

                .RegisterType<IRequestValidator, RequestValidator>()
                .RegisterType<IMappingHelper, MappingHelper>()

                .RegisterType<IBingAddressParser, BingAddressParser>()
                .RegisterType<IMelissaAddressParser, MelissaAddressParser>()
                .RegisterType<IAddressQueryBuilder, AddressQueryBuilder>()
                .RegisterType<IGoogleAddressParser, GoogleAddressParser>()

                .RegisterType<ValidateAddressResponse, ValidateAddressResponse>(LifetimeManagerType.AlwaysNew)
                .RegisterType<ClearCacheResponse, ClearCacheResponse>(LifetimeManagerType.AlwaysNew)
                .RegisterType<AutocompleteAddressResponse, AutocompleteAddressResponse>(LifetimeManagerType.AlwaysNew)
                .RegisterType<HealthResponse, HealthResponse>(LifetimeManagerType.AlwaysNew)
                .RegisterType<GetMapResponse, GetMapResponse>(LifetimeManagerType.AlwaysNew);


            //Configure logs
            new LogConfigurator(container)
                .ConfigureLog(LogType.ExceptionLog | LogType.PerformanceLog | LogType.TraceLog | LogType.DbPerformanceLog);
        }
    }
}
