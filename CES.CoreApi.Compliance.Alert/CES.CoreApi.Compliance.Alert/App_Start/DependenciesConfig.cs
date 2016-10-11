using AutoMapper;
using CES.CoreApi.Compliance.Alert.Business.Interfaces;
using CES.CoreApi.Compliance.Alert.Business.Repositories;
using SimpleInjector;


namespace CES.CoreApi.Compliance.Alert
{
	public class DependenciesConfig
	{
		public static void RegisterDependencies(Container container)
		{
			RegisterAutomapper(container);
			RegisterServices(container);
			RegisterRepository(container);
			container.Verify();
		}
		
		private static void RegisterAutomapper(Container container)
		{
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfile(new AlertsMapperProfile());
				cfg.ConstructServicesUsing(type => container.GetInstance(type));
			});
		}
		
		private static void RegisterServices(Container container)
		{
			container.Register<IAlertsService, AlertsService>();
		}

		private static void RegisterRepository(Container container)
		{
			container.Register<IAlertsRepository, AlertsRepository>();
		}
	}
}
