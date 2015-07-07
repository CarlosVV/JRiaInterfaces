using CES.CoreApi.Accounting.Service.Business.Contract.Models;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Accounting.Service.Business.Contract.Interfaces
{
    public interface ITransactionInformationRepository
    {
        TransactionSummaryModel GetSummaryByAgentLocation(GetTransactionSummaryRequestModel requestData);
        DatabasePingModel Ping();
    }
}