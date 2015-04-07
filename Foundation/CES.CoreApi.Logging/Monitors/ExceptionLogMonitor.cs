using System;
using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Monitors
{
    public class ExceptionLogMonitor : BaseLogMonitor, IExceptionLogMonitor
    {
        private readonly IServiceCallInformationProvider _serviceCallDetailsProvider;

        #region Core

        public ExceptionLogMonitor(ExceptionLogDataContainer dataContainer, IServiceCallInformationProvider serviceCallDetailsProvider, 
            ILoggerProxy logProxy, ILogConfigurationProvider configuration) 
            : base(logProxy, configuration)
        {
            if (dataContainer == null) throw new ArgumentNullException("dataContainer");
            if (serviceCallDetailsProvider == null) throw new ArgumentNullException("serviceCallDetailsProvider");

            _serviceCallDetailsProvider = serviceCallDetailsProvider;
            DataContainer = dataContainer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets performance log data container instance
        /// </summary>
        public ExceptionLogDataContainer DataContainer { get; private set; }

        #endregion //Properties

        /// <summary>
        /// Adds service call details to exception log data contianer 
        /// </summary>
        /// <param name="context">Service operation context</param>
        /// <param name="getClientDetails"></param>
        /// <returns></returns>
        public void AddServiceCallDetails(OperationContext context,
            Func<IDictionary<string, object>> getClientDetails)
        {
            if (context == null) return;

            //Calling application should not fail if logging failed
            try
            {
                //Collect web service call details
                _serviceCallDetailsProvider.AddDetails(DataContainer, context, getClientDetails);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        public void Publish(Exception exception, string customMessage = null)
        {
            //Calling application should not fail if logging failed
            try
            {
                customMessage = string.IsNullOrEmpty(customMessage) ? string.Empty : customMessage;

                DataContainer.SetException(exception);
                DataContainer.CustomMessage = customMessage;

                Publish(DataContainer);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }
    }
}
