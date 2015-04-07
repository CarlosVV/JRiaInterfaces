using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Interfaces;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace CES.CoreApi.UnityProxy
{
    public class UnityPerformanceInterceptorBehavior : IInterceptionBehavior
    {
        #region Core

        private readonly IPerformanceLogMonitor _performanceMonitor;
        private readonly IExceptionLogMonitor _exceptionMonitor;
        private readonly ILogConfigurationProvider _configuration;

        public UnityPerformanceInterceptorBehavior(IPerformanceLogMonitor performanceMonitor, IExceptionLogMonitor exceptionMonitor, 
            ILogConfigurationProvider configuration)
        {
            if (performanceMonitor == null) throw new ArgumentNullException("performanceMonitor");
            if (exceptionMonitor == null) throw new ArgumentNullException("exceptionMonitor");
            if (configuration == null) throw new ArgumentNullException("configuration");
            _performanceMonitor = performanceMonitor;
            _exceptionMonitor = exceptionMonitor;
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

            _performanceMonitor.Start(input.MethodBase);

            //Invoke method
            var message = getNext()(input, getNext);

            //Post method calling
            _performanceMonitor.DataContainer.Arguments = input.Arguments;
            _performanceMonitor.DataContainer.ReturnValue = message.ReturnValue;
            _performanceMonitor.Stop();

            //Handle exception since it can be lost if happened on very early configuration stage
            if (message.Exception != null)
                _exceptionMonitor.Publish(message.Exception);

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