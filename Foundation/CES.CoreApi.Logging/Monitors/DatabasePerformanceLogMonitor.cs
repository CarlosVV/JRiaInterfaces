using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Monitors
{
    public class DatabasePerformanceLogMonitor : BaseLogMonitor, IDatabasePerformanceLogMonitor
    {
        #region Core

        private readonly Stopwatch _timer;

        /// <summary>
        /// Initializes DatabasePerformanceLogMonitor instance
        /// </summary>
        /// <param name="dataContainer">Trace log data container instance</param>
        /// <param name="configuration">Performance log configuration </param>
        /// <param name="logProxy"></param>
        public DatabasePerformanceLogMonitor(DatabasePerformanceLogDataContainer dataContainer, ILogConfigurationProvider configuration, ILoggerProxy logProxy)
            : base(logProxy, configuration)
        {
            if (dataContainer == null)
                throw new ArgumentNullException("dataContainer");

            DataContainer = dataContainer;

            _timer = new Stopwatch();
        }

        #endregion //Core

        #region Properties

        /// <summary>
        /// Gets or sets database performance log data container instance
        /// </summary>
        public DatabasePerformanceLogDataContainer DataContainer { get; private set; }

        /// <summary>
        /// Returns TRUE if timeout exceeded, otherwise returns FALSE
        /// </summary>
        private bool ThresholdExceeded
        {
            get
            {
                return _timer.ElapsedMilliseconds >= Configuration.DatabasePerformanceLogConfiguration.Threshold;
            }
        }

        #endregion //Properties

        #region Public methods

        /// <summary>
        /// Starts performance log monitoring
        /// </summary>
        public void Start(DbCommand command)
        {
            if (!Configuration.PerformanceLogConfiguration.IsEnabled)
                return;
            if (command == null)
                throw new ArgumentNullException("command");
            if (_timer.IsRunning)
                throw new ApplicationException("Database Performance Monitor is running. Call Stop method first.");

            PopulateDataContainer(command);

            _timer.Start();
        }

        public void UpdateConnectionDetails(DbCommand command)
        {
            if (!Configuration.PerformanceLogConfiguration.IsEnabled)
                return;
            if (command == null)
                throw new ArgumentNullException("command");
            if (!_timer.IsRunning)
                throw new ApplicationException("Database Performance Monitor is not running. Start method should be called before Stop.");
            
            SetConnectionDetails(command);
        }

        /// <summary>
        /// Stops performance log monitoring
        /// </summary>
        public void Stop()
        {
            if (!Configuration.PerformanceLogConfiguration.IsEnabled)
                return;

            if (!_timer.IsRunning)
                throw new ApplicationException("Database Performance Monitor is not running. Start method should be called before Stop.");

            _timer.Stop();

            if (!ThresholdExceeded)
                return;

            DataContainer.ElapsedMilliseconds = _timer.ElapsedMilliseconds;
            Publish(DataContainer);
        }

        #endregion //Public methods

        #region Private methods
        
        private void PopulateDataContainer(DbCommand command)
        {
            DataContainer.CommandText = command.CommandText;
            DataContainer.CommandTimeout = command.CommandTimeout;
            DataContainer.CommandType = command.CommandType;
            DataContainer.Parameters = GetParametersCollection(command);

            SetConnectionDetails(command);
        }

        private void SetConnectionDetails(DbCommand command)
        {
            if (command.Connection == null)
                return;
            if (command.Connection.State != ConnectionState.Open)
                return;

            DataContainer.Connection = new DatabaseConnection
            {
                ConnectionString = command.Connection.ConnectionString.RemoveSecurityCredentials(),
                ConnectionTimeout = command.Connection.ConnectionTimeout,
                DatabaseName = command.Connection.Database,
                ServerName = command.Connection.DataSource,
                ServerVersion = command.Connection.ServerVersion
            };
        }

        private static IEnumerable<DatabaseParameter> GetParametersCollection(DbCommand command)
        {
            var parameters = new Collection<DatabaseParameter>();
            foreach (var dbParameter in from SqlParameter parameter in command.Parameters select new DatabaseParameter
            {
                Name = parameter.ParameterName,
                DataType = parameter.DbType,
                Direction = parameter.Direction,
                Value = parameter.Value
            })
            {
                parameters.Add(dbParameter);
            }
            return parameters;
        }

        #endregion
    }
}
