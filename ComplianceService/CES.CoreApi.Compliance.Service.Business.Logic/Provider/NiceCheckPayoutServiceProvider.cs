using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Compliance.Service.Business.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;
using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;
namespace CES.CoreApi.Compliance.Service.Business.Logic.Provider
{
    public class NiceCheckPayoutServiceProvider :BaseCheckProvider , ICheckPayoutProvider
    {
        #region Core

        private readonly ICheckPayoutProvider _checkPayoutProvider;
       
        public NiceCheckPayoutServiceProvider(ICheckPayoutProvider checkPayoutProvider)
            : base(CheckPayoutProviderType.Ria)
        {
            if (checkPayoutProvider == null)
                throw new CoreApiException(TechnicalSubSystem.ComplianceService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "checkPayoutProvider");

            _checkPayoutProvider = checkPayoutProvider;
         
        }

        #endregion
      

        public CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
