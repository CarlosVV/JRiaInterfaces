using CES.CoreApi.LimitVerification.Service.Contract.Models;

namespace CES.CoreApi.LimitVerification.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(CheckPayingAgentLimitsRequest request);
        void Validate(CheckReceivingAgentLimitsRequest request);
    }
}
