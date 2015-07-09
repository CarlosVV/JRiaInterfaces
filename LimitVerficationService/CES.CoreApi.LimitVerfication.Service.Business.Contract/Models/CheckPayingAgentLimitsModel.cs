using CES.CoreApi.LimitVerfication.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.LimitVerfication.Service.Business.Contract.Models
{
    public class CheckPayingAgentLimitsModel
    {
        public Payability CurrencyLimitsResult { get; set; }
        public Payability LocationCurrencyLimitsResult { get; set; }
    }
}