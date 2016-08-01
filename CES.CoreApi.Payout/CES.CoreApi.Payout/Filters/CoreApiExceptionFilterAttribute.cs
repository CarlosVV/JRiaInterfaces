using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace CES.CoreApi.Payout.Filters
{
	/// <summary>
	/// Exception Filter: This is to response  a customize error message and HttpStatusCode code.
	/// Optional: You can customize the exception message
	///			 You can send exception with custom message
	///          Or You can only return custom message

	public class CoreApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			var statusCode = HttpStatusCode.InternalServerError;

			if (context.Exception is NotImplementedException)
				statusCode = HttpStatusCode.NotImplemented;

			context.Response = context.Request.CreateErrorResponse(statusCode, context.Exception);
			/*
			context.Response = context.Request.CreateErrorResponse(statusCode, "CES.CoreApi.Payout: Your custome messsage", context.Exception);
			*/
		}
	}
}