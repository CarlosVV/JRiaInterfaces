using System.Collections.Generic;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IApplicationRepository
    {
        Application GetApplication(int applicationId);
        HostApplication GetApplication(int applicationId, int serverId);

        /// <summary>
        /// Gets application configuration items collection
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns></returns>
        ICollection<ApplicationConfiguration> GetApplicationConfiguration(int applicationId);

        void Ping();
    }
}