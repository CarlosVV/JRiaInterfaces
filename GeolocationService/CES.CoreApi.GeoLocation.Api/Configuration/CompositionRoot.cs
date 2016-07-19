using AutoMapper;
using CES.CoreApi.GeoLocation.Logic.Builders;
using CES.CoreApi.GeoLocation.Logic.Factories;
using CES.CoreApi.GeoLocation.Logic.Parsers;
using CES.CoreApi.GeoLocation.Logic.Processors;
using CES.CoreApi.GeoLocation.Logic.Providers;
using SimpleInjector;
using CES.CoreApi.GeoLocation.Interfaces;

namespace CES.CoreApi.GeoLocation.Api.Configuration
{
	public class CompositionRoot
    {
        public static void RegisterDependencies(Container container)
        {
  
            RegisterAutomapper(container);
            RegisterProcessors(container);
            RegisterUrlBuilders(container);
            RegisterParsers(container);
            RegisterProviders(container);     
            RegisterFactories(container); 	
            container.Verify();
        }

  
		

		private static void RegisterAutomapper(Container container)
		{

			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new GeoLocationMapperProfile());
				cfg.ConstructServicesUsing(type => container.GetInstance(type));
			});
			container.RegisterSingleton(config);
			container.Register(() => config.CreateMapper(container.GetInstance));
		}      
        
        private static void RegisterFactories(Container container)
        {
            container.RegisterSingleton<IUrlBuilderFactory>(new UrlBuilderFactory
            {
                {"IBingUrlBuilder", container.GetInstance<BingUrlBuilder>},
                {"IGoogleUrlBuilder", container.GetInstance<GoogleUrlBuilder>},
                {"IMelissaDataUrlBuilder", container.GetInstance<MelissaUrlBuilder>}
            });

            container.RegisterSingleton<IResponseParserFactory>(new ResponseParserFactory
            {
                {"IBingResponseParser", container.GetInstance<BingResponseParser>},
                {"IGoogleResponseParser", container.GetInstance<GoogleResponseParser>},
                {"IMelissaDataResponseParser", container.GetInstance<MelissaResponseParser>}
            });
        }

        private static void RegisterProcessors(Container container)
        {     
            container.Register<IAddressServiceRequestProcessor, AddressServiceRequestProcessor>();
            container.Register<IGeocodeServiceRequestProcessor, GeocodeServiceRequestProcessor>();
            container.Register<IMapServiceRequestProcessor, MapServiceRequestProcessor>();
          
        }
        private static void RegisterUrlBuilders(Container container)
        {
            container.RegisterCollection<IUrlBuilder>(new[] {
				typeof(BingUrlBuilder),
				typeof(GoogleUrlBuilder),
				typeof(MelissaUrlBuilder)});
        }
        private static void RegisterParsers(Container container)
        {
            container.RegisterCollection<IResponseParser>(new[] {
				typeof(BingResponseParser),
				typeof(GoogleResponseParser),
				typeof(MelissaResponseParser) });

            container.Register<IBingAddressParser, BingAddressParser>();
            container.Register<IMelissaAddressParser, MelissaAddressParser>();
            container.Register<IAddressQueryBuilder, AddressQueryBuilder>();
            container.Register<IGoogleAddressParser, GoogleAddressParser>();
        }

        private static void RegisterProviders(Container container)
        {
            container.Register<IAddressVerificationDataProvider, AddressVerificationDataProvider>();      
            container.Register<IMappingDataProvider, MappingDataProvider>();
            container.Register<IDataResponseProvider, DataResponseProvider>();
            container.Register<IMelissaLevelOfConfidenceProvider, MelissaLevelOfConfidenceProvider>();
            container.Register<IGoogleLevelOfConfidenceProvider, GoogleLevelOfConfidenceProvider>();
         
            container.Register<IAddressAutocompleteDataProvider, AddressAutocompleteDataProvider>();
            container.Register<IGeocodeAddressDataProvider, GeocodeAddressDataProvider>();
            container.Register<IBingPushPinParameterProvider, BingPushPinParameterProvider>();
            container.Register<IGooglePushPinParameterProvider, GooglePushPinParameterProvider>();
            container.Register<ICorrectImageSizeProvider, CorrectImageSizeProvider>();
        }       


		
    }
}
