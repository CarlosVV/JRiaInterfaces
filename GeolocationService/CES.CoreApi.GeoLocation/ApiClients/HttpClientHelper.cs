using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.ApiClients
{
	class HttpClientHelper
	{
		static async Task RunAsync(string uriString, string requestUri)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://devcoreapi.riadev.local");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			

				
				var response = await client.GetAsync(requestUri);
				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadAsAsync<object>();
					//SuggestionResponse suggestion = JsonConvert.DeserializeObject<SuggestionResponse>(result.Result.ToString());

				}
			}
		}
	}
}
