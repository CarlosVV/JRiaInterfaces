using AutoMapper;
using CES.CoreApi.Payout.Facade.Utilities;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Logic.Data;
using CES.CoreApi.Payout.Service.Business.Logic.Email;
using CES.CoreApi.Payout.Service.Business.Logic.Factories;
using CES.CoreApi.Payout.Service.Business.Logic.Processors;
using CES.CoreApi.Payout.Service.Business.Logic.Providers;
using CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents;
using CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents.GoldenCrown;
using CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents.Ria;
using CES.CoreApi.Payout.Service.Business.Logic.Providers.RiaDatabase;
using CES.CoreApi.Payout.Service.Contract.Interfaces;
using CES.CoreApi.Payout.Models;
using CES.CoreApi.Shared.Persistence.Business;
using CES.CoreApi.Shared.Persistence.Data;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Providers.Helper.Business;
using CES.CoreApi.Shared.Providers.Helper.Data;
using CES.CoreApi.Shared.Providers.Helper.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Payout.Api
{
	public class DependenciesConfig
	{
		public static void RegisterDependencies(Container container)
		{

			RegisterAutomapper(container);
			RegisterProcessors(container);
			RegisterProviders(container);
			RegisterResponses(container);
			RegisterOthers(container);
			RegisterRepositories(container);
			RegisterFactories(container);
			container.Verify();
		}


		private static void RegisterAutomapper(Container container)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new PayoutMapperProfile());
				cfg.ConstructServicesUsing(type => container.GetInstance(type));
			});
			container.RegisterSingleton(config);
			container.Register(() => config.CreateMapper(container.GetInstance));


		}
		private static void RegisterProcessors(Container container)
		{
			container.Register<IPayoutProcessor, PayoutProcessor>();
		}
		private static void RegisterProviders(Container container)
		{

			container.RegisterCollection<ICorrespondentAPI>(new[] {
					typeof(GoldenCrownPayoutProcess),
					typeof(RiaPayoutProcess) });

			container.Register<IProviderHelper, ProviderHelper>();
			container.Register<IPayoutServiceProvider, PayoutServiceProvider>();
		}
		private static void RegisterResponses(Container container)
		{

			container.Register(() => new GetTransactionInfoResponse(0, string.Empty));
			container.Register(() => new PayoutTransactionResponse(0, string.Empty));
		}
		private static void RegisterOthers(Container container)
		{
			container.Register<IRequestValidator, RequestValidator>();
		}



		private static void RegisterRepositories(Container container)
		{
			container.Register<IProviderRepository, ProviderRepository>();
			container.Register<IRiaRepository, RiaRepository>();
			container.Register<IDataHelper, DataHelper>();
			container.Register<IPersistenceHelper, PersistenceHelper>();
			container.Register<IPersistenceRepository, PersistenceRepository>();
			container.Register<IEmailHelper, EmailHelper>();


		}
		private static void RegisterFactories(Container container)
		{
			container.RegisterSingleton<IPayoutServiceProviderFactory>(new PayoutProviderFactory
			{
				{"IGoldenCrownPayoutProvider",container.GetInstance<GoldenCrownPayoutProcess>},
				{"IRiaPayoutProvider", container.GetInstance<RiaPayoutProcess>}
			});
		}
	}
}