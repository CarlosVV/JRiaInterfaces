using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CES.CoreApi.PushNotifications.Filters
{
	public class HttpMessageHandler : DelegatingHandler
	{

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var id = request.GetCorrelationId();

			if (request.Content != null)
			{
				if (request.Method.Method == "GET")
				{
					Logging.Log.Info(string.Format("{0}-{1}", id, request.RequestUri.AbsoluteUri));
				}
				else
				{
					await request.Content.ReadAsStringAsync().ContinueWith(task =>
					{
						Logging.Log.Info(string.Format("{0}-{1}", id, task.Result, request.RequestUri.AbsoluteUri));
					}, cancellationToken);
				}
			}

			HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
		
			response.Headers.Add("ResponseId", id.ToString());
			ObjectContent content = response.Content as ObjectContent;
			if (content != null)
			{
				Logging.Log.Info(string.Format("{0}-{1}", id, content.ReadAsStringAsync().Result));
			}	

			return response;
			
		}		
	}
}