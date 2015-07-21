using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using CES.CoreApi.Accounting.Service.Business.Contract.Interfaces;
using CES.CoreApi.Accounting.Service.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Accounting.Service.Data.Repositories
{
    public class TransactionInformationRepository : BaseGenericRepository, ITransactionInformationRepository
    {
        #region Core

        public TransactionInformationRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager,
            IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        } 

        #endregion

        #region ITransactionInformationRepository implementation

        public TransactionSummaryModel GetSummaryByAgentLocation(GetTransactionSummaryRequestModel requestData)
        {
            var request = new DatabaseRequest<TransactionSummaryModel>
            {
                ProcedureName = requestData.IsReceivingAgent
                    ? "ol_sp_compltblTransaction_ForFilters_GetSumByRecAgentLoc"
                    : "ol_sp_compltblTransaction_ForFilters_GetSumByCorrespLoc",
                IsCacheable = false,
                DatabaseType = DatabaseType.FrontEnd,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fRecAgentID", requestData.AgentId)
                    .Add("@fRecAgentLocID", requestData.LocationId)
                    .Add("@fCurrency", requestData.Currency)
                    .Add("@fVoid", requestData.IsVoided)
                    .Add("@fCancelled", requestData.IsCancelled)
                    .Add("@fOrderDate", requestData.OrderDate),
                Shaper = reader => GetTransactionSummary(reader)
            };

            return Get(request);
        }

        #endregion

        private static TransactionSummaryModel GetTransactionSummary(IDataReader reader)
        {
            return new TransactionSummaryModel
            {
                AmountTotal = reader.ReadValue<decimal>("fTotalAmount"),
                LocalUsdTotal = reader.ReadValue<decimal>("fLocalUSD"),
                TransferTotal = reader.ReadValue<decimal>("fTransAmount"),
                UsdTotal = reader.ReadValue<decimal>("fTotalUSD")
            };
        }
    }
}
