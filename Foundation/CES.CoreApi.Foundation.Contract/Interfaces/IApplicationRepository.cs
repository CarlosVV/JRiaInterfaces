using System.Collections.Generic;
using System.Threading.Tasks;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Foundation.Contract.Interfaces
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