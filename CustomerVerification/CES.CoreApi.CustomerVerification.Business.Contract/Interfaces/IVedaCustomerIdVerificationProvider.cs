using System.Threading.Tasks;
using CES.CoreApi.CustomerVerification.Business.Contract.Models;

namespace CES.CoreApi.CustomerVerification.Business.Contract.Interfaces
{
    public interface IVedaCustomerIdVerificationProvider
    {
        Task<VerifyCustomerIdentityResponseModel> Verify(VerifyCustomerIdentityRequestModel request);
    }
}