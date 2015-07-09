using CES.CoreApi.Accounting.Service.Contract.Models;

namespace CES.CoreApi.Accounting.Service.Interfaces
{
    public interface IRequestValidator
    {
        //void Validate(GetAgentCurrencyRequest request);
        void Validate(GetTransactionSummaryRequest request);
    }
}