
using CES.CoreApi.Compliance.Alert.Filters;
using CES.Security.CoreApi;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using Xunit;

namespace CES.CoreApi.Compliance.Alert.Tests.App_Start
{
	public class FiltersConfigTest
	{
		public FiltersConfigTest()
		{
			FiltersConfig.RegisterGlobalFilters();
		}

		[Fact]
		public void RegisterGlobalFilters_AddValidateModelStateAttribute()
		{
			Assert.True(GlobalConfiguration.Configuration.Filters.Any(filter => filter.Scope == FilterScope.Global && 
																		filter.Instance.GetType() == typeof(ValidateModelStateAttribute)));
		}
		
		[Fact]
		public void RegisterGlobalFilters_AddAuthenticationFilter()
		{
			Assert.True(GlobalConfiguration.Configuration.Filters.Any(filter => filter.Scope == FilterScope.Global && 
																		filter.Instance.GetType() == typeof(AuthenticationFilter)));
		}

		[Fact]
		public void RegisterGlobalFilters_AddCustomExceptionsAttribute()
		{
			Assert.True(GlobalConfiguration.Configuration.Filters.Any(filter => filter.Scope == FilterScope.Global && 
																		filter.Instance.GetType() == typeof(CustomExceptionsAttribute)));
		}
	}
}
