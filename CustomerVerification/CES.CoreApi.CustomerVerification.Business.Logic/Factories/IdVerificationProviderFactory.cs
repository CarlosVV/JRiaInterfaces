using System;
using System.Collections.Generic;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.CustomerVerification.Business.Contract.Interfaces;
using CES.CoreApi.CustomerVerification.Business.Logic.Providers;

namespace CES.CoreApi.CustomerVerification.Business.Logic.Factories
{
    public class IdVerificationProviderFactory : Dictionary<string, Func<BaseCustomerIdVerificationProvider>>, IIdVerificationProviderFactory
    {
        #region Core

        private readonly ICountryConfigurationProvider _countryConfigurationProvider;

        public IdVerificationProviderFactory(ICountryConfigurationProvider countryConfigurationProvider)
        {
            if (countryConfigurationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerVerificationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "countryConfigurationProvider");

            _countryConfigurationProvider = countryConfigurationProvider;
        }

        #endregion

        #region IIdVerificationProviderFactory implementation

        public BaseCustomerIdVerificationProvider GetInstance(string country)
        {
            var configuration = _countryConfigurationProvider.GetConfiguration(country);
            return this[configuration.Country]();
        } 

        #endregion
    }
}