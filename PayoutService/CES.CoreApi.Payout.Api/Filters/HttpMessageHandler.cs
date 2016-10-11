using CES.CoreApi.Payout.Api.Filters.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CES.CoreApi.Payout.Api.Filters
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

			return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
			{
				var response = task.Result;
				response.Headers.Add("ResponseId", id.ToString());
				ObjectContent content = response.Content as ObjectContent;

				if (content != null)
				{
					Logging.Log.Info(string.Format("{0}-{1}", id, content.ReadAsStringAsync().Result));
				}
				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					return BuildApiResponse(request, response);
				}



				return response;
			}, cancellationToken);
		}


		private HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
		{

			object content;
			List<Error> modelStateErrors = new List<Error>();
			var errors = new List<Error>();
	
			if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
			{
				HttpError error = content as HttpError;
				if (error != null)
				{
					content = null;
					if (error.ModelState != null)
					{
						foreach (var item in error.ModelState)
						{
							modelStateErrors.Add(new Error
							{
								Property = item.Key,
								Message = string.Join(",", ((string[])item.Value))
							});
						}
					}
				}
			}


			var newResponse = request.CreateResponse(response.StatusCode
				, new ErrorResponse("Invalid request or required fields was not provided", 400
				, modelStateErrors, request.GetCorrelationId()));

			foreach (var header in response.Headers) //Add back the response headers
			{
				newResponse.Headers.Add(header.Key, header.Value);
			}

			return newResponse;
		}
	}
}