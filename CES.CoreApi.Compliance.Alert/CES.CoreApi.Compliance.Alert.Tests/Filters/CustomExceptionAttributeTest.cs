using CES.CoreApi.Compliance.Alert.Filters;
using CES.CoreApi.Compliance.Alert.Tests.InitTests;
using System;
using System.Net;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using Xunit;

namespace CES.CoreApi.Compliance.Alert.Tests.Filters
{
	public class CustomExceptionAttributeTest
	{
		[Theory]
		[AutoMoqData(typeof(WebApiCustomization))]
		public void OnException_WhenUnhandledException_MessageWithInternalServerErrorStatusCodeIsReturned(HttpActionExecutedContext context, CustomExceptionsAttribute customExceptionsAttribute)
		{
			context.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
			customExceptionsAttribute.OnException(context);

			Assert.Equal(context.Response.StatusCode, HttpStatusCode.InternalServerError);
		}

		[Theory]
		[AutoMoqData(typeof(WebApiCustomization))]
		public void OnException_WhenNotImplementedExceptionIsThrown_MessageWithNotImplementedStatusCodeIsReturned(HttpActionExecutedContext context, CustomExceptionsAttribute customExceptionsAttribute)
		{
			context.Exception = new NotImplementedException();
			context.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
			customExceptionsAttribute.OnException(context);

			Assert.Equal(context.Response.StatusCode, HttpStatusCode.NotImplemented);
		}

		[Theory]
		[AutoMoqData(typeof(WebApiCustomization))]
		public void OnException_WhenAuthenticationExceptionIsThrown_MessageWithUnauthorizesStatusCodeIsReturned(HttpActionExecutedContext context, CustomExceptionsAttribute customExceptionsAttribute)
		{
			context.Exception = new AuthenticationException();
			context.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
			customExceptionsAttribute.OnException(context);

			Assert.Equal(context.Response.StatusCode, HttpStatusCode.Unauthorized);
		}
	}
}
