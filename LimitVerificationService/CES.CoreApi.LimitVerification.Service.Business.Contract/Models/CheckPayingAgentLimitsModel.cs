using CES.CoreApi.LimitVerification.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.LimitVerification.Service.Business.Contract.Models
{
    public class CheckPayingAgentLimitsModel
    {
        public Payability CurrencyLimitsResult { get; set; }
        public Payability LocationCurrencyLimitsResult { get; set; }
    }
}