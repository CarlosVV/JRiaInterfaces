using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;
using CES.CoreApi.Foundation.Data.Utility;

namespace CES.CoreApi.OrderProcess.Service.Data.Repositories
{
    public class TransactionRepository : BaseGenericRepository, ITransactionRepository
    {
        #region Core

        public TransactionRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager,
            IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        } 

        #endregion

        #region ITransactionRepository implementation

        public TransactionDetailsModel GetOrder(int orderId, int databaseId = 0)
        {
            var request = new DatabaseRequest<TransactionDetailsModel>
            {
                ProcedureName = "ol_sp_oltblOrdersToPost_GetByOrderID",
                IsCacheable = true,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>()
                    .Add("@orderId", orderId),
                Shaper = reader => GetTransactionDetails(reader)
            };

            return Get(request);
        }
        
        #endregion

        #region Private methods

        private TransactionDetailsModel GetTransactionDetails(IDataReader reader)
        {
            throw new System.NotImplementedException();
        } 

        #endregion
    }
}
