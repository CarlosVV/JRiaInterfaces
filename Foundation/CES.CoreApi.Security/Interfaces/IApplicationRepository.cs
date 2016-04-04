using CES.CoreApi.Security.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CES.CoreApi.Foundation.Security.Interfaces
{
    public interface IApplicationRepository
    {
        Task<Application> GetApplication(int applicationId);

        /// <summary>
        /// Gets application configuration items collection
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns></returns>
        Task<ICollection<ApplicationConfiguration>> GetApplicationConfiguration(int applicationId);
    }
}