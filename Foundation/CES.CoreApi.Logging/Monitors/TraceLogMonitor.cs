using System;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Monitors
{
    public class TraceLogMonitor : BaseLogMonitor, ITraceLogMonitor
    {
        #region Core

        private bool _isStarted;

        public TraceLogMonitor(TraceLogDataContainer dataContainer, ILogConfigurationProvider configuration, ILoggerProxy logProxy)
            : base(logProxy, configuration)
        {
            if (dataContainer == null) throw new ArgumentNullException("dataContainer");

            DataContainer = dataContainer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets trace log data container instance
        /// </summary>
        public TraceLogDataContainer DataContainer { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts performance log monitoring
        /// </summary>
        public void Start()
        {
            if (!Configuration.TraceLogConfiguration.IsEnabled)
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
            if (!Configuration.TraceLogConfiguration.IsEnabled)
                return;

            if (!_isStarted)
                throw new ApplicationException("Trace Monitor is not running. Start method should be called before Stop.");

            _isStarted = false;
            Publish(DataContainer as IDataContainer);
        }

        #endregion //Public methods
    }
}
