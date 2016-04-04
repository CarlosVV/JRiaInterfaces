using SimpleInjector;
using CES.CoreApi.GeoLocation.Api.Models;

namespace CES.CoreApi.GeoLocation.Api
{
	public class CompositionRoot
    {
        public static void RegisterDependencies(Container container)
        {
		
			container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);		

			container.Verify();
        }

	}
}
