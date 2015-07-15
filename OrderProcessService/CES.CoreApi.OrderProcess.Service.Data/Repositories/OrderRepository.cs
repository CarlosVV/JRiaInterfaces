using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;
using CES.CoreApi.Foundation.Data.Utility;

namespace CES.CoreApi.OrderProcess.Service.Data.Repositories
{
    public class OrderRepository : BaseGenericRepository, IOrderRepository
    {
        #region Core

        public OrderRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager)
            : base(cacheProvider, monitorFactory, identityManager, DatabaseType.Main)
        {
        } 

        #endregion

        #region IOrderRepository implementation

        public OrderModel GetOrder(int orderId)
        {
            var request = new DatabaseRequest<OrderModel>
            {
                ProcedureName = "ol_sp_oltblOrdersToPost_GetByOrderID",
                IsCacheable = true,
                Parameters = new Collection<SqlParameter>()
                    .Add("@orderId", orderId),
                Shaper = reader => GetOrderDetails(reader)
            };

            return Get(request);
        }
        
        #endregion

        #region Private methods

        private OrderModel GetOrderDetails(IDataReader reader)
        {
            throw new System.NotImplementedException();
        } 

        #endregion
    }
}
