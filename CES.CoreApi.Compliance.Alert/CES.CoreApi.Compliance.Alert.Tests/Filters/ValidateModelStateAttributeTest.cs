using CES.CoreApi.Compliance.Alert.Filters;
using CES.CoreApi.Compliance.Alert.Tests.InitTests;
using CES.CoreApi.Compliance.Alert.Controllers;
using Moq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xunit;

namespace CES.CoreApi.Compliance.Alert.Tests.Filters
{
	public class ValidateModelStateAttributeTest
	{
		[Theory]
		[AutoMoqData(typeof(WebApiCustomization))]
		public void OnActionExecuting_WhenModelIsInvalid_ResponseHttpStatusCodeIsBadRequest(ValidateModelStateAttribute validationAttribute, Mock<AlertsController> controller)
		{
			controller.Object.ModelState.AddModelError("Errorkey", "ErrorMsg");
			controller.Object.Request = new HttpRequestMessage();
			
			validationAttribute.OnActionExecuting(controller.Object.ActionContext);

			Assert.Equal(controller.Object.ActionContext.Response.StatusCode, HttpStatusCode.BadRequest);
		}

		[Theory]
		[AutoMoqData(typeof(WebApiCustomization))]
		public void OnActionExecuting_WhenModelIsValid_ResponseHttpStatusCodeIsDifferentFromBadRequest(ValidateModelStateAttribute validationAttribute, Mock<AlertsController> controller)
		{
			validationAttribute.OnActionExecuting(controller.Object.ActionContext);

			Assert.Null(controller.Object.ActionContext.Response);
		}
	}
}
