using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CES.CoreApi.Security.Managers;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Models;

namespace CES.CoreApi.Security.Test
{
	[TestClass]
	public class ApplicationAuthenticatorTest
	{
		[TestMethod]
		public void Authenticate_WhenRequestHeaderIsNull_ExceptionWithGeneralRequiredParameterIsUndefinedSubsystemIsThrown()
		{
			try
			{
				var applicationAuthenticator = new ApplicationAuthenticator(null);
				applicationAuthenticator.Authenticate(null);
			}
			catch (Exception ex)
			{
				var coreApiException = ex as CoreApiException;
				if (coreApiException == null)
					Assert.Fail("Unexpected exception occurred. " + ex.Message);
				
				Assert.AreEqual(Common.Enumerations.SubSystemError.GeneralRequiredParameterIsUndefined, coreApiException.SubSystemError);
			}
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsNotFound_ApplicationNotFoundInDatabaseExceptionIsThrown()
		{
			var stubApplicationRepository = new Mock<IApplicationRepository>();

			try
			{
				var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
				applicationAuthenticator.Authenticate(
					new ServiceCallHeaderParameters(0, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty));
			}
			catch (Exception ex)
			{
				var coreApiException = (ex as AggregateException).InnerException as CoreApiException;

				if (coreApiException == null)
					Assert.Fail("Unexpected exception occurred. " + ex.Message);

				Assert.AreEqual(Common.Enumerations.SubSystemError.ApplicationNotFoundInDatabase, coreApiException.SubSystemError);
			}
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsNotActive_ClientApplicationDoesNotExistOrInactiveExceptionIsThrown()
		{
			var stubHostApplication = 1;
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(new Mock<Application>(1, "stub", false).Object);

			try
			{
				var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
				applicationAuthenticator.Authenticate(
					new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty));
			}
			catch (Exception ex)
			{
				var coreApiException = (ex as AggregateException).InnerException as CoreApiException;

				if (coreApiException == null)
					Assert.Fail("Unexpected exception occurred. " + ex.Message);

				Assert.AreEqual(Common.Enumerations.SubSystemError.ClientApplicationDoesNotExistOrInactive, coreApiException.SubSystemError);
			}
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityAsAuthenticated()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(
				new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty));

			Assert.IsTrue(principal.Identity.IsAuthenticated);
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityName()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(
				new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty));

			Assert.AreEqual(clientApplication.Name, principal.Identity.Name);
		}


		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityId()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(
				new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty));

			Assert.AreEqual(clientApplication.Id, (principal.Identity as IClientApplicationIdentity).ApplicationId);
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityOperationName()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);
			var serviceCallHeaderParameters = new ServiceCallHeaderParameters(stubHostApplication, "OperationName", DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty);

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(serviceCallHeaderParameters);

			Assert.AreEqual(serviceCallHeaderParameters.OperationName, (principal.Identity as IClientApplicationIdentity).OperationName);
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityTimeStamp()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);
			var serviceCallHeaderParameters = new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty);

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(serviceCallHeaderParameters);

			Assert.AreEqual(serviceCallHeaderParameters.Timestamp, (principal.Identity as IClientApplicationIdentity).Timestamp);
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityApplicationSessionId()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);
			var serviceCallHeaderParameters = new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, "applicationSessionIs", string.Empty, string.Empty, string.Empty);

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(serviceCallHeaderParameters);

			Assert.AreEqual(serviceCallHeaderParameters.ApplicationSessionId, (principal.Identity as IClientApplicationIdentity).ApplicationSessionId);
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityReferenceNumber()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);
			var serviceCallHeaderParameters = new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, string.Empty, "referenceNumber", string.Empty, string.Empty);

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(serviceCallHeaderParameters);

			Assert.AreEqual(serviceCallHeaderParameters.ReferenceNumber, (principal.Identity as IClientApplicationIdentity).ReferenceNumber);
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityReferenceNumberType()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);
			var serviceCallHeaderParameters = new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, string.Empty, string.Empty, "referenceNumberType", string.Empty);

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(serviceCallHeaderParameters);

			Assert.AreEqual(serviceCallHeaderParameters.ReferenceNumberType, (principal.Identity as IClientApplicationIdentity).ReferenceNumberType);
		}

		[TestMethod]
		public void Authenticate_WhenClientApplicationIsActive_PrincipalObjectIsBackWithClientApplicationIdentityCorrelationId()
		{
			var stubHostApplication = 1;
			var clientApplication = new Application(1, "ApplicationName", true);
			var stubApplicationRepository = new Mock<IApplicationRepository>();
			stubApplicationRepository.Setup(s => s.GetApplication(stubHostApplication)).ReturnsAsync(clientApplication);
			var serviceCallHeaderParameters = new ServiceCallHeaderParameters(stubHostApplication, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, "correlationId");

			var applicationAuthenticator = new ApplicationAuthenticator(stubApplicationRepository.Object);
			var principal = applicationAuthenticator.Authenticate(serviceCallHeaderParameters);

			Assert.AreEqual(serviceCallHeaderParameters.CorrelationId, (principal.Identity as IClientApplicationIdentity).CorrelationId);
		}
	}
}
