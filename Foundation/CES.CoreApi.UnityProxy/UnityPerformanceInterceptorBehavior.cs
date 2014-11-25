using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Interfaces;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace CES.CoreApi.UnityProxy
{
    public class UnityPerformanceInterceptorBehavior : IInterceptionBehavior
    {
        #region Core

        private readonly ILogManager _logManager;
        private readonly ILogConfigurationProvider _configuration;

        public UnityPerformanceInterceptorBehavior(ILogManager logManager, ILogConfigurationProvider configuration)
        {
            if (logManager == null) throw new ArgumentNullException("logManager");
            if (configuration == null) throw new ArgumentNullException("configuration");
            _logManager = logManager;
            _configuration = configuration;
        }

        #endregion

        #region IInterceptionBehavior implementation

        /// <summary>
        /// Implement this method to execute your behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the behavior chain.</param>
        /// <returns>
        /// Return value from the target.
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.DeclaringType == null) 
                return default(IMethodReturn);

            var monitor = _logManager.GetMonitorInstance<IPerformanceLogMonitor>();
            monitor.Start(input.MethodBase);

            //Invoke method
            var message = getNext()(input, getNext);

            //Post method calling
            monitor.DataContainer.Arguments = input.Arguments;
            monitor.DataContainer.ReturnValue = message.ReturnValue;
            monitor.Stop();

            //Handle exception since it can be lost if happened on very early configuration stage
            if (message.Exception != null)
                _logManager.Publish(message.Exception);

            return message;
        }

        /// <summary>
        /// Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>
        /// The required interfaces.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Returns a flag indicating if this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        /// This is used to optimize interception. If the behaviors won't actually
        /// do anything (for example, PIAB where no policies match) then the interception
        /// mechanism can be skipped completely.
        /// </remarks>
        public bool WillExecute
        {
            get { return _configuration.PerformanceLogConfiguration.IsEnabled; }
        }

        #endregion
    }
}