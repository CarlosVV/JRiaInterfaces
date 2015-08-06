using System;
using System.Linq;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Data.Providers
{
    public class DatabasePingProvider : IDatabasePingProvider
    {
        #region Core

        private readonly IDatabaseInstanceProvider _instanceProvider;
        private readonly IDatabaseConfigurationProvider _configurationProvider;
        private readonly ILogMonitorFactory _monitorFactory;
        private readonly IIdentityManager _identityManager;

        public DatabasePingProvider(IDatabaseInstanceProvider instanceProvider, IDatabaseConfigurationProvider configurationProvider, 
            ILogMonitorFactory monitorFactory, IIdentityManager identityManager)
        {
            if (instanceProvider == null) throw new ArgumentNullException("instanceProvider");
            if (monitorFactory == null) throw new ArgumentNullException("monitorFactory");
            if (identityManager == null) throw new ArgumentNullException("identityManager");

            _instanceProvider = instanceProvider;
            _configurationProvider = configurationProvider;
            _monitorFactory = monitorFactory;
            _identityManager = identityManager;
        } 

        #endregion

        #region IDatabasePingProvider implementation

        public PingResponseModel PingDatabases()
        {
            return new PingResponseModel
            {
                Databases = from name in _configurationProvider.GetDatabaseNameList()
                            select PingDatabase(name)
            };
        }
        
        #endregion
        
        #region Private methods

        private DatabasePingModel PingDatabase(string groupName)
        {
            var pingResult = new DatabasePingModel { Database = groupName };

            try
            {
                var database = _instanceProvider.GetDatabase(groupName);
                using (var connection = database.CreateConnection())
                {
                    connection.Open();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                pingResult.IsOk = false;
            }

            pingResult.IsOk = true;

            return pingResult;
        } 

        private void HandleException(Exception ex)
        {
            var exceptionLogMonitor = _monitorFactory.CreateNew<IExceptionLogMonitor>();
            exceptionLogMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();
            exceptionLogMonitor.Publish(ex);
        } 

        #endregion
    }
}
