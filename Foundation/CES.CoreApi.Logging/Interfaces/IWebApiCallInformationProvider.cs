using System.Web.Http.ExceptionHandling;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IWebApiCallInformationProvider
    {
        void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, ExceptionLoggerContext context);
    }
}