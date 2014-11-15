using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using CES.CoreApi.Caching.Interfaces;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Data
{
    public class ApplicationRepository: BaseRepository, IApplicationRepository
    {
        #region Core

        public ApplicationRepository(ICacheProvider cacheProvider,  ILogManager logManager)
            : base(cacheProvider, logManager, ConnectionStrings.Main)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets application instance by ID
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <returns></returns>
        public Application GetApplication(int applicationId)
        {
            return CacheProvider.GetCacheItem(string.Format("GetApplication-{0}", applicationId), () =>
            {
                using (var cmd = Database.GetStoredProcCommand("CoreApiFoundation_GetApplicationByID"))
                {
                    Database.AddInParameter(cmd, "applicationID", DbType.Int32, applicationId);

                    return GetApplication(cmd, applicationId);
                }
            });
        }

        /// <summary>
        /// Gets application instance by ID and ServerID
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="serverId">Server ID</param>
        /// <returns></returns>
        public HostApplication GetApplication(int applicationId, int serverId)
        {
            var application = GetApplication(applicationId);
            var server = application.Servers.FirstOrDefault(s => s.Id == serverId);

            return server != null
                ? new HostApplication(application, serverId)
                : null;
        }

        /// <summary>
        /// Gets application configuration items collection
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns></returns>
        public ICollection<ApplicationConfiguration> GetApplicationConfiguration(int applicationId)
        {
            var application = GetApplication(applicationId);
            return application.Configuration;
        }

        public void Ping()
        {
            PingDatabase();
        }

        #endregion

        #region Private methods

        private Application GetApplication(DbCommand cmd, int applicationId)
        {
            return ExecuteReader(cmd, reader =>
            {
                //Initialize applicaiton instance
                var application = InitializeApplication(reader, applicationId);
                reader.NextResult();

                //Initialize configuration
                InitializeConfigurationList(reader, application);
                reader.NextResult();

                //Initialize service operations
                InitializeOperationList(reader, application);
                reader.NextResult();

                //Initialize list of applications assigned to every operation
                InitializeAssignedApplicationList(reader, application);
                reader.NextResult();

                //Initialize list of servers where application installed
                InitializeServerList(reader, application);

                return application;
            });
        }

        private static Application InitializeApplication(IDataReader reader, int applicationId)
        {
            if (reader.Read())
            {
                return new Application(
                    applicationId, 
                    reader.ReadEnumValueFromInt<ApplicationType>("ApplicationTypeId"),
                    reader.ReadValue<string>("Name"), 
                    reader.ReadValue<bool>("IsActive"));
            }
            throw new CoreApiException(TechnicalSubSystem.CoreApiData, SubSystemError.ApplicationNotFoundInDatabase, applicationId);
        }
        private static void InitializeConfigurationList(IDataReader reader, Application application)
        {
            while (reader.Read())
            {
                var configurationItem = new ApplicationConfiguration(
                    reader.ReadValue<string>("ConfigurationName"),
                    reader.ReadValue<string>("ConfigurationValue"));
                application.Configuration.Add(configurationItem);
            }
        }

        private static void InitializeOperationList(IDataReader reader, Application application)
        {
            while (reader.Read())
            {
                var operation = new ServiceOperation(
                    reader.ReadValue<int>("ServiceOperationID"),
                    reader.ReadValue<string>("MethodName"),
                    reader.ReadValue<bool>("IsActive"));

                application.Operations.Add(operation);
            }
        }

        private static void InitializeAssignedApplicationList(IDataReader reader, Application application)
        {
            while (reader.Read())
            {
                var operationId = reader.ReadValue<int>("ServiceOperationID");
                var foundOperation = application.Operations.FirstOrDefault(o => o.Id == operationId);
                if (foundOperation == null) continue;

                var applicationOperation = new ApplicationServiceOperation(
                    reader.ReadValue<int>("ApplicationID"),
                    reader.ReadValue<bool>("IsActive"));

                foundOperation.AssignedApplications.Add(applicationOperation);
            }
        }

        private static void InitializeServerList(IDataReader reader, Application application)
        {
            while (reader.Read())
            {
                var server = new ApplicationServer(
                    reader.ReadValue<int>("ApplicationServerID"),
                    reader.ReadValue<string>("Name"), 
                    reader.ReadValue<bool>("IsActive"));
                application.Servers.Add(server);
            }
        }

        #endregion
    }
}
