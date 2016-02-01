using CES.CoreApi.Compliance.Service.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Contract.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(CheckOrderRequest request);
        void Validate(CheckPayoutRequest  request);
    }
}