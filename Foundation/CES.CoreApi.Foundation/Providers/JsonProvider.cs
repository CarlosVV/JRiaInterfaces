using Newtonsoft.Json;

namespace CES.CoreApi.Foundation.Providers
{
	public class JsonProvider
	{
		public static string ConvertToJson(object t)
		{
			return JsonConvert.SerializeObject(t);
		}
	}
}
