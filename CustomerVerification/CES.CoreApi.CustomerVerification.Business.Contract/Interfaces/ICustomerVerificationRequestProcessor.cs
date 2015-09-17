using System.Threading.Tasks;
using CES.CoreApi.CustomerVerification.Business.Contract.Models;

namespace CES.CoreApi.CustomerVerification.Business.Contract.Interfaces
{
    public interface ICustomerVerificationRequestProcessor
    {
        Task<VerifyCustomerIdentityResponseModel> VerifyCustomerIdentity(VerifyCustomerIdentityRequestModel request);
    }
}