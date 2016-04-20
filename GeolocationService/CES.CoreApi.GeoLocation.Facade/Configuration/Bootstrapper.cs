
using SimpleInjector;


namespace CES.CoreApi.GeoLocation.Facade.Configuration
{
	public static class Bootstrapper
	{
		public static readonly Container Container;

		static Bootstrapper()
		{
			var container = new Container();
			//container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();

			// register all your components with the container here:
			container.Register<IUser, User>(Lifestyle.Scoped);
			//container.Register<IMappingHelper, MappingHelper>();
			//container.Register<IAddressServiceRequestProcessor, AddressServiceRequestProcessor>();

			container.Verify();

			Container = container;
		}
	}
}
