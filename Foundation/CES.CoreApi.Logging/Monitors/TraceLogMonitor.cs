using System;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Monitors
{
    public class TraceLogMonitor : ITraceLogMonitor
    {
        #region Core

        private readonly ILogManager _logManager;
        private readonly ILogConfigurationProvider _configuration;
        private bool _isStarted;

        /// <summary>
        /// Initializes TraceLogMonitor instance
        /// </summary>
        /// <param name="dataContainer">Trace log data container instance</param>
        /// <param name="logManager">Log manager instance</param>
        /// <param name="configuration">Trace log configuration </param>
        public TraceLogMonitor(ITraceLogDataContainer dataContainer,
            ILogManager logManager,
            ILogConfigurationProvider configuration)
        {
            if (dataContainer == null)
                throw new ArgumentNullException("dataContainer");
            if (logManager == null)
                throw new ArgumentNullException("logManager");
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            DataContainer = dataContainer;
            _logManager = logManager;
            _configuration = configuration;
        }

        #endregion //Core

        #region Properties

        /// <summary>
        /// Gets or sets trace log data container instance
        /// </summary>
        public ITraceLogDataContainer DataContainer { get; private set; }

        #endregion //Properties

        #region Public methods

        /// <summary>
        /// Starts performance log monitoring
        /// </summary>
        public void Start()
        {
            if (!_configuration.TraceLogConfiguration.IsEnabled)
                return;
            if (_isStarted)
                throw new ApplicationException("Trace Monitor is running. Call Stop method first.");

            _isStarted = true;
        }

        /// <summary>
        /// Stops performance log monitoring
        /// </summary>
        public void Stop()
        {
            if (!_configuration.TraceLogConfiguration.IsEnabled)
                return;

            if (!_isStarted)
                throw new ApplicationException("Trace Monitor is not running. Start method should be called before Stop.");
            
            _isStarted = false;
            _logManager.Publish(DataContainer as IDataContainer);
        }

        #endregion //Public methods
    }
}
