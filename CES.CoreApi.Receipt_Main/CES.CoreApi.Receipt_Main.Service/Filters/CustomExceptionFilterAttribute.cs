using CES.CoreApi.Receipt_Main.Service.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http.Filters;

namespace CES.CoreApi.Receipt_Main.Service.Filters
{
    /// <summary>
    /// Exception Filter: This is to response  a customize error message and HttpStatusCode code.
    /// Optional: You can customize the exception message
    ///			 You can send exception with custom message
    ///          Or You can only return custom message
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is NotImplementedException)
                statusCode = HttpStatusCode.NotImplemented;

            else if (context.Exception is AuthenticationException)
                statusCode = HttpStatusCode.Unauthorized;


            var message =
                        new
                        {
                            ResultCode = statusCode,
                            Message = context.Exception == null ? "" : context.Exception.Message,
                            ResponseId = context.Request.GetCorrelationId()
                        };

            context.Response = context.Request.CreateResponse(statusCode, message, "application/json");
        }
    }
}