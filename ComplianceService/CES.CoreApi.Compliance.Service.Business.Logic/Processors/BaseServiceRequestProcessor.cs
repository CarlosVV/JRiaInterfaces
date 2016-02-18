﻿using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using MoreLinq;
using CES.CoreApi.Compliance.Service.Business.Contract.Configuration;
using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;
using CES.CoreApi.Compliance.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.Compliance.Service.Business.Logic.Processors
{
    public abstract class BaseServiceRequestProcessor
    {
        protected BaseServiceRequestProcessor(ICountryConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.ComplianceService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");

            CountryConfigurationProvider = configurationProvider;
        }

        protected ICountryConfigurationProvider CountryConfigurationProvider { get; private set; }

        /// <summary>
        /// Gets providers by country and service type ordered by priority
        /// </summary>
        /// <param name="country"></param>
        /// <param name="serviceType"></param>
        /// <param name="numberOfProviders">Number of data provider configurations to return. Default is 1.</param>
        /// <returns></returns>
        protected IEnumerable<DataProviderConfiguration> GetProviderConfigurationByCountry(string country, DataProviderServiceType serviceType, int numberOfProviders = 1)
        {
            var addressVerificationProviders = CountryConfiguration(country).DataProviders
                .Where(p => p.DataProviderServiceType == serviceType)
                .OrderBy(p => p.Priority)
                .DistinctBy(p => new { p.DataProviderType, p.DataProviderServiceType })  //in case if same provider defined multiple times in configuration
                .Take(numberOfProviders);

            return addressVerificationProviders;
        }

        protected CountryConfiguration CountryConfiguration(string country)
        {
            return CountryConfigurationProvider.GetProviderConfigurationByCountry(country);
        }
    }
}