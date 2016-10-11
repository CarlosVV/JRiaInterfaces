using System.Collections.Generic;
using System.Net.Http;

namespace CES.CoreApi.GeoLocation.Tools
{
	public class ClientHeader
	{
		public static bool IsCountryProviderSelection()
		{
			var ctx =System.Web.HttpContext.Current.Request.Headers;
			var countryProviderSelections = null as IEnumerable<string>;
			countryProviderSelections = ctx.GetValues("UseCountrySetting");
			if (countryProviderSelections == null)
				return false;
			//if (ctx.("UseCountryAsProviderPicker", out countryProviderSelections))
			//{
				var key = string.Join("", countryProviderSelections);
				if (!string.IsNullOrEmpty(key))
				{
					bool yes;
					bool.TryParse(key, out yes);
					return yes;
				}
			//}
			return false;
			
		}
	}
}
