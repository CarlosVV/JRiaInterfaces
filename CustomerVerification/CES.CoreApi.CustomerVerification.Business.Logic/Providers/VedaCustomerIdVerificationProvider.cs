using System.Threading.Tasks;
using CES.CoreApi.CustomerVerification.Business.Contract.Interfaces;
using CES.CoreApi.CustomerVerification.Business.Contract.Models;

namespace CES.CoreApi.CustomerVerification.Business.Logic.Providers
{
    public class VedaCustomerIdVerificationProvider: BaseCustomerIdVerificationProvider, IVedaCustomerIdVerificationProvider
    {
        public override Task<VerifyCustomerIdentityResponseModel> Verify(VerifyCustomerIdentityRequestModel request)
        {
            return null;
        }
    }
}
