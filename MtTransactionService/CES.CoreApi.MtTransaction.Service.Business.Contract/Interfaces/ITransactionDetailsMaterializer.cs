using System.Data;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Interfaces
{
    public interface ITransactionDetailsMaterializer
    {
        TransactionDetailsModel GetTransactionDetails(IDataReader reader, InformationGroup detalizationLevel);
    }
}