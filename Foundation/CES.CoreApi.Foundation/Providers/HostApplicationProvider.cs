using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Configuration;
using CES.CoreApi.Foundation.Contract.Constants;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Foundation.Providers
{
    public class HostApplicationProvider : IHostApplicationProvider
    {
        #region Core

        private readonly IApplicationRepository _repository;

        public HostApplicationProvider(IApplicationRepository repository)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");
            _repository = repository;
        }

        #endregion

        #region Core

        /// <summary>
        /// Gets host application instance and validates that:
        /// 1. ApplicationId defined in config file
        /// </summary>
        public IApplication GetApplication()
        {
            //Get host application ID from config file and validate them
            var applicationId = ConfigurationTools.ReadAppSettingsValue<int>(ServiceConfigurationItems.AppplicationId);
            if (applicationId == 0)
                throw new CoreApiException(Organization.Ria, TechnicalSystem.CoreApi,
                    TechnicalSubSystem.Authentication, SubSystemError.ApplicationIdNotFoundInConfigFile);

            return _repository.GetApplication(applicationId);
        }

        #endregion
    }
}
