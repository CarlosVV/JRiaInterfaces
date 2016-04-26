using System;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Compliance.Service.Business.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;
using CES.CoreApi.Compliance.Service.Business.Logic.Provider;
using CES.CoreApi.Compliance.Service.Business.Logic.Constants;
using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Compliance.Service.Business.Logic.Processors
{
    public class CheckPayoutRequestProcessor :BaseServiceRequestProcessor, ICheckPayoutRequestProcessor
    {
        #region Core

        private readonly ICheckPayoutServiceProvider _checkPayoutServiceProvider;


        public CheckPayoutRequestProcessor(ICountryConfigurationProvider configurationProvider, ICheckPayoutServiceProvider checkPayoutServiceProvider)
            : base(configurationProvider)
        {
            if (checkPayoutServiceProvider == null)
                throw new CoreApiException(TechnicalSubSystem.ComplianceService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "checkPayoutServiceProvider");

            _checkPayoutServiceProvider = checkPayoutServiceProvider;

        }

        #endregion
        public OrderModel GetOrderByNumber(string orderNumber)
        {

            string body = "body of order - body of order - body of order - body of order - body of order - body of order - body of order - ";
            return new OrderModel { OrderNumber = "AB000001", OrderBody = body };
        }


        public CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request)
        {
            //var settingDefaultCheckPayoutProvider = CountryConfigurationProvider.ConfigurationProvider.Read<string>(ConfigurationConstants.CheckPayoutProvider);
            var providerConfiguration = GetProviderConfigurationByCountry(request.Country, DataProviderServiceType.CheckPayout).FirstOrDefault();

            if (providerConfiguration == null)
                throw new CoreApiException(TechnicalSubSystem.ComplianceService,
                    SubSystemError.ComplianceDataProviderNotFound,
                    DataProviderServiceType.CheckPayout);

            return _checkPayoutServiceProvider.CheckPayout(request, providerConfiguration.DataProviderType);

            //var servicesProvider = new CheckPayoutServiceProvider(request);
            //var service = servicesProvider.GetResponse()

            //var compliancePayoutCheck = new CheckPayoutServiceProvider(service);
            //return compliancePayoutCheck.Check();
            //throw new NotImplementedException();
        }
    }
}
