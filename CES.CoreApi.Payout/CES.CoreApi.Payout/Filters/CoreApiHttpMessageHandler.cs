using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Filters
{
	public class CoreApiHttpMessageHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			Guid id = Guid.NewGuid();
			if (request.Content != null)
			{
				if (request.Method.Method == "GET")
				{
					/*Optional to log request*/
					Logging.Log.Info(string.Format("{0}-{1}", id, request.RequestUri.AbsoluteUri));
				}
				else
				{
					await request.Content.ReadAsStringAsync().ContinueWith(task =>
					{
						/*Optional to log request*/
						Logging.Log.Info(string.Format("{0}-{1}", id, task.Result, request.RequestUri.AbsoluteUri));
					}, cancellationToken);
				}
			}

			return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
			{

				var response = task.Result;
				if (response.Content != null)
				{
					/*Optional to log response*/
					Logging.Log.Info(string.Format("{0}-{1}", id, response.Content.ReadAsStringAsync().Result));
				}



				return response;
			}, cancellationToken);
		}
	}

}