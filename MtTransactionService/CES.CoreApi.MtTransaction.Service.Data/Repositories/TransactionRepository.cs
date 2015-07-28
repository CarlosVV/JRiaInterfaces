using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Interfaces;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Data.Repositories
{
    public class TransactionRepository : BaseGenericRepository, ITransactionRepository
    {
        private readonly ITransactionDetailsMaterializer _transactionDetailsMaterializer;

        #region Core

        private const string GetDetailsCacheKeySuffixTemplate = "Detalization_Level_{0}";

        public TransactionRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager,
            IDatabaseInstanceProvider instanceProvider, ITransactionDetailsMaterializer transactionDetailsMaterializer)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
            if (transactionDetailsMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.MtTransactionService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "transactionDetailsMaterializer");
            _transactionDetailsMaterializer = transactionDetailsMaterializer;
        }

        #endregion

        #region ITransactionRepository implementation

        public TransactionDetailsModel GetDetails(int orderId, InformationGroup detalizationLevel, int databaseId = 0)
        {
            var request = new DatabaseRequest<TransactionDetailsModel>
            {
                ProcedureName = "ol_sp_oltblOrdersToPost_GetByOrderID",
                IsCacheable = true,
                DatabaseType = databaseId != 0
                    ? DatabaseType.FrontEnd
                    : DatabaseType.Main,
                DatabaseId = databaseId,
                Parameters = new Collection<SqlParameter>()
                    .Add("@orderId", orderId),
                Shaper = reader => _transactionDetailsMaterializer.GetTransactionDetails(reader, detalizationLevel),
                //CacheInvalidator = entity => entity.DetalizationLevel == detalizationLevel,
                CacheKeySuffix = string.Format(GetDetailsCacheKeySuffixTemplate, detalizationLevel)
            };

            return Get(request);
        }
        
        #endregion
    }
}