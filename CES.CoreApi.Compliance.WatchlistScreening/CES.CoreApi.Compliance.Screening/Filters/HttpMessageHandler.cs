
using CES.CoreApi.Compliance.Screening.Utilities;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CES.CoreApi.Compliance.Screening.Filters
{
	public class HttpMessageHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var id = Client.GetCorrelationId(request);
			if (request.Content != null)
			{
				if (request.Method.Method == "GET")
				{
					Logging.Log.Info($"{id}-Request-{request.Method.Method}\n {request.RequestUri.AbsoluteUri}");
				}
				else
				{
					await request.Content.ReadAsStringAsync().ContinueWith(task =>
					{
						Logging.Log.Info($"{id}-Request-{request.Method.Method}\n {task.Result}");
					}, cancellationToken);
				}
			}

			return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
			{
				var response = task.Result;
				response.Headers.Add("ResponseId", id.ToString());
				ObjectContent content = response.Content as ObjectContent;
				if (content != null)
				{
					Logging.Log.Info($"{id}-Response\n{content.ReadAsStringAsync().Result}");
				}

				return response;
			}, cancellationToken);
		}
	}

}