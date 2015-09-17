using System.Threading.Tasks;
using CES.CoreApi.CustomerVerification.Business.Contract.Models;

namespace CES.CoreApi.CustomerVerification.Business.Logic.Providers
{
    public abstract class BaseCustomerIdVerificationProvider
    {
        public abstract Task<VerifyCustomerIdentityResponseModel> Verify(VerifyCustomerIdentityRequestModel request);
    }
}
