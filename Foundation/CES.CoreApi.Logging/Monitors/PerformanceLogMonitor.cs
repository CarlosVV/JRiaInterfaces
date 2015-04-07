﻿using System;
using System.Diagnostics;
using System.Reflection;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Monitors
{
    public class PerformanceLogMonitor : BaseLogMonitor, IPerformanceLogMonitor
    {
        #region Core

        private readonly Stopwatch _timer;
        private readonly IFullMethodNameFormatter _fullMethodNameFormatter;

        /// <summary>
        /// Initializes PerformanceLogMonitor instance
        /// </summary>
        /// <param name="dataContainer">Trace log data container instance</param>
        /// <param name="fullMethodNameFormatter">Full method name provider</param>
        /// <param name="configuration">Performance log configuration </param>
        /// <param name="logProxy"></param>
        public PerformanceLogMonitor(PerformanceLogDataContainer dataContainer,
            IFullMethodNameFormatter fullMethodNameFormatter,
            ILogConfigurationProvider configuration, ILoggerProxy logProxy) : 
            base(logProxy, configuration)
        {
            if (dataContainer == null)
                throw new ArgumentNullException("dataContainer");
            if (fullMethodNameFormatter == null)
                throw new ArgumentNullException("fullMethodNameFormatter");

            DataContainer = dataContainer;
            _fullMethodNameFormatter = fullMethodNameFormatter;

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
                return _timer.ElapsedMilliseconds >= Configuration.PerformanceLogConfiguration.Threshold;
            }
        }

        #endregion //Properties

        #region Public methods

        /// <summary>
        /// Starts performance log monitoring
        /// </summary>
        public void Start(MethodBase method)
        {
            if (!Configuration.PerformanceLogConfiguration.IsEnabled)
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
            if (!Configuration.PerformanceLogConfiguration.IsEnabled)
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
            if (!Configuration.PerformanceLogConfiguration.IsEnabled)
                return;

            if (!_timer.IsRunning)
                throw new ApplicationException("Performance Monitor is not running. Start method should be called before Stop.");

            _timer.Stop();

            if (!ThresholdExceeded)
                return;

            DataContainer.ElapsedMilliseconds = _timer.ElapsedMilliseconds;
            Publish(DataContainer);
        }

        #endregion //Public methods
    }
}
