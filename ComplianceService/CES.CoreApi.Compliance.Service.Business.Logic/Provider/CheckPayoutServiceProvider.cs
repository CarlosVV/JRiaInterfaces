using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;
using CES.CoreApi.Compliance.Service.Business.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Business.Logic.Provider
{
    public class CheckPayoutServiceProvider : ICheckPayoutServiceProvider
    {
        #region Core

      
     
        private readonly ICheckPayoutProviderFactory _responseFactory;

        public CheckPayoutServiceProvider(ICheckPayoutProviderFactory responseFactory)
        {
           
            if (responseFactory == null)
                throw new CoreApiException(TechnicalSubSystem.ComplianceService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "responseFactory");

            _responseFactory = responseFactory;
        }



        #endregion


        public CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request, CheckPayoutProviderType providerType)
        {
            var provider = _responseFactory.GetInstance<ICheckPayoutProvider>(providerType);

            return provider.CheckPayout(request);
        }


    }
}
