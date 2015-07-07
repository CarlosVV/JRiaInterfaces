using CES.CoreApi.LimitVerfication.Service.Contract.Models;

namespace CES.CoreApi.LimitVerfication.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(CheckPayingAgentLimitsRequest request);
        void Validate(CheckReceivingAgentLimitsRequest request);
    }
}
