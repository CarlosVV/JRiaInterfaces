using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.UnitTestTools
{
    public class ExceptionHelper
    {
        private static void Handle(Exception ex)
        {
            var coreApiException = ex as CoreApiException;
            var message = coreApiException != null ? coreApiException.ClientMessage : ex.Message;
            Assert.Fail("Unexpected exception occurred. " + message);
        }

        private static void Handle(Exception ex, string expectedErrorMessage)
        {
            var coreApiException = ex as CoreApiException;
            if (coreApiException == null)
                Assert.Fail("Unexpected exception occurred. " + ex.Message);

            // ReSharper disable PossibleNullReferenceException
            Assert.AreEqual(expectedErrorMessage, coreApiException.ClientMessage);
            // ReSharper restore PossibleNullReferenceException
            
        }

        public static void CheckException(Func<object> methodToCall, SubSystemError subSystemError, params object[] parameters)
        {
            var isRaised = false;
            try
            {
                methodToCall();
            }
            catch (Exception ex)
            {
                isRaised = true;
                var expectedMessage = ClientErrorMessageProvider.GetMessage(subSystemError, parameters);
                Handle(ex, expectedMessage);
            }

            if(!isRaised)
                Assert.Fail("An expected exception was not raised.");
        }

        public static void CheckException(Action methodToCall, SubSystemError subSystemError, params object[] parameters)
        {
            var isRaised = false;
            try
            {
                methodToCall();
            }
            catch (Exception ex)
            {
                isRaised = true;
                var expectedMessage = ClientErrorMessageProvider.GetMessage(subSystemError, parameters);
                Handle(ex, expectedMessage);
            }

            if (!isRaised)
                Assert.Fail("An expected exception was not raised.");
        }

        public static void CheckHappyPath(Func<object> methodToCall)
        {
            try
            {
                methodToCall();
            }
            catch (Exception ex)
            {
                Handle(ex);
            }
        }

        public static void CheckHappyPath(Action methodToCall)
        {
            try
            {
                methodToCall();
            }
            catch (Exception ex)
            {
                Handle(ex);
            }
        }
    }
}