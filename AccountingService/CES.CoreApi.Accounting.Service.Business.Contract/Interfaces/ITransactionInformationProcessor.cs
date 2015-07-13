using CES.CoreApi.Accounting.Service.Business.Contract.Models;

namespace CES.CoreApi.Accounting.Service.Business.Contract.Interfaces
{
    public interface ITransactionInformationProcessor
    {
        TransactionSummaryModel GetTransactionSummary(GetTransactionSummaryRequestModel request);
    }
}