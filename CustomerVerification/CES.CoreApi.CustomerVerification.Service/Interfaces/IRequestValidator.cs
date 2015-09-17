using CES.CoreApi.CustomerVerification.Service.Contract.Models;

namespace CES.CoreApi.CustomerVerification.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(VerifyCustomerIdentityRequest request);
    }
}