using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Data
{
    public class ApplicationRepository: BaseGenericRepository, IApplicationRepository
    {
        #region Core

        public ApplicationRepository(ICacheProvider cacheProvider, ILogMonitorFactory logMonitorFactory, IIdentityManager identityManager)
            : base(cacheProvider, logMonitorFactory, identityManager, DatabaseType.Main)
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
            var request = new DatabaseRequest<Application>
            {
                ProcedureName = "coreapi_sp_GetApplicationByID",
                IsCacheable = true,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@applicationID", applicationId)
                },
                Shaper = reader => GetApplication(reader, applicationId)
            };

            return Get(request);
        }

        /// <summary>
        /// Gets application configuration items collection
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns></returns>
        public ICollection<ApplicationConfiguration> GetApplicationConfiguration(int applicationId)
        {
            var application = GetApplication(applicationId);

            if (application == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApiData, SubSystemError.ApplicationNotFoundInDatabase, applicationId);

            return application.Configuration;
        }

        public DatabasePingModel Ping()
        {
            return PingDatabase();
        }

        #endregion

        #region Private methods

        private Application GetApplication(IDataReader reader, int applicationId)
        {
            //Initialize applicaiton instance
            var application = InitializeApplication(reader, applicationId);
            if (application == null)
                return null;
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

            return application;
        }

        private static Application InitializeApplication(IDataReader reader, int applicationId)
        {
            if (reader.Read())
            {
                return new Application(
                    applicationId, 
                    reader.ReadValue<string>("Name"), 
                    reader.ReadValue<bool>("IsActive"));
            }
            return null;
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

        #endregion
    }
}
