using CES.CoreApi.Compliance.Alert.Filters;
using CES.Security.CoreApi;
using System.Web.Http;

namespace CES.CoreApi.Compliance.Alert
{
	public class FiltersConfig
	{
		public static void RegisterGlobalFilters()
		{
			GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("coreAPI"));
			GlobalConfiguration.Configuration.Filters.Add(new ValidateModelStateAttribute());
			GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionsAttribute());
		}
	}
}
