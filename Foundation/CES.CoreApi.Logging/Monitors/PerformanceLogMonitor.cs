using System;
using System.Diagnostics;
using System.Reflection;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Monitors
{
    public class PerformanceLogMonitor : IPerformanceLogMonitor
    {
        #region Core

        private readonly Stopwatch _timer;
        private readonly ILogManager _logManager;
        private readonly IFullMethodNameFormatter _fullMethodNameFormatter;
        private readonly ILogConfigurationProvider _configuration;

        /// <summary>
        /// Initializes PerformanceLogMonitor instance
        /// </summary>
        /// <param name="dataContainer">Trace log data container instance</param>
        /// <param name="logManager">Log manager instance</param>
        /// <param name="fullMethodNameFormatter">Full method name provider</param>
        /// <param name="configuration">Performance log configuration </param>
        public PerformanceLogMonitor(PerformanceLogDataContainer dataContainer,
            ILogManager logManager,
            IFullMethodNameFormatter fullMethodNameFormatter,
            ILogConfigurationProvider configuration)
        {
            if (dataContainer == null)
                throw new ArgumentNullException("dataContainer");
            if (logManager == null)
                throw new ArgumentNullException("logManager");
            if (fullMethodNameFormatter == null)
                throw new ArgumentNullException("fullMethodNameFormatter");
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            DataContainer = dataContainer;
            _logManager = logManager;
            _fullMethodNameFormatter = fullMethodNameFormatter;
            _configuration = configuration;

            _timer = new Stopwatch();
        }

        #endregion //Core

        #region Properties

        /// <summary>
        /// Gets or sets performance log data container instance
        /// </summary>
        public PerformanceLogDataContainer DataContainer { get; private set; }

        /// <summary>
        /// Returns TRUE if timeout exceeded, otherwise returns FALSE
        /// </summary>
        private bool ThresholdExceeded
        {
            get
            {
                return _timer.ElapsedMilliseconds >= _configuration.PerformanceLogConfiguration.Threshold;
            }
        }

        #endregion //Properties

        #region Public methods

        /// <summary>
        /// Starts performance log monitoring
        /// </summary>
        public void Start(MethodBase method)
        {
            if (!_configuration.PerformanceLogConfiguration.IsEnabled)
                return;
            if (method == null)
                throw new ArgumentNullException("method");
            if (method.DeclaringType == null)
                throw new ApplicationException("method.DeclaringType is NULL.");
            if (_timer.IsRunning)
                throw new ApplicationException("Performance Monitor is running. Call Stop method first.");

            DataContainer.MethodName = _fullMethodNameFormatter.Format(method.DeclaringType.FullName, method.Name);
            DataContainer.GenericArguments = method.DeclaringType.IsGenericType ? method.DeclaringType.GetGenericArguments() : null;
            _timer.Start();
        }

        /// <summary>
        /// Starts performance log monitoring
        /// </summary>
        public void Start(string methodName)
        {
            if (!_configuration.PerformanceLogConfiguration.IsEnabled)
                return;
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException("methodName");
            if (_timer.IsRunning)
                throw new ApplicationException("Performance Monitor is running. Call Stop method first.");

            DataContainer.MethodName = methodName;
            _timer.Start();
        }

        /// <summary>
        /// Stops performance log monitoring
        /// </summary>
        public void Stop()
        {
            if (!_configuration.PerformanceLogConfiguration.IsEnabled)
                return;

            if (!_timer.IsRunning)
                throw new ApplicationException("Performance Monitor is not running. Start method should be called before Stop.");

            _timer.Stop();

            if (!ThresholdExceeded)
                return;

            DataContainer.ElapsedMilliseconds = _timer.ElapsedMilliseconds;
            _logManager.Publish(DataContainer);
        }

        #endregion //Public methods
    }
}
