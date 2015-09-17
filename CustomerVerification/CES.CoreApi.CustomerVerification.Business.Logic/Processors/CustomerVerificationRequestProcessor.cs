using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.CustomerVerification.Business.Contract.Interfaces;
using CES.CoreApi.CustomerVerification.Business.Contract.Models;
using CES.CoreApi.CustomerVerification.Business.Logic.Factories;

namespace CES.CoreApi.CustomerVerification.Business.Logic.Processors
{
    public class CustomerVerificationRequestProcessor : ICustomerVerificationRequestProcessor
    {
        #region Core

        private readonly IIdVerificationProviderFactory _providerFactory;

        public CustomerVerificationRequestProcessor(IIdVerificationProviderFactory providerFactory)
        {
            
            if (providerFactory == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerVerificationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "providerFactory");

            _providerFactory = providerFactory;
        }

        #endregion

        #region ICustomerVerificationRequestProcessor implementation

        public Task<VerifyCustomerIdentityResponseModel> VerifyCustomerIdentity(VerifyCustomerIdentityRequestModel request)
        {
            var provider = _providerFactory.GetInstance(request.Country);
            var result = provider.Verify(request);
            return result;
        } 

        #endregion
    }
}
