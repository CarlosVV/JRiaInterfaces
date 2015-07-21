using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Data
{
    public class ValidationMainRepository : BaseGenericRepository, IValidationMainRepository
    {
        public ValidationMainRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager,
            IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        }

        public bool IsDuplicateOrderExist(OrderDuplicateValidationRequestModel model)
        {
            var request = new DatabaseRequest<bool>
            {
                ProcedureName = "coreapi_sp_OrderDuplicateValidation",
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>()
                    .Add("@RecAgentID", model.ReceivingAgentId)
                    .Add("@RecAgentLocationID", model.ReceivingAgentLocationId)
                    .Add("@CustomerID", model.CustomerId)
                    .Add("@PayAgentID", model.PayAgentId)
                    .Add("@AmountLocal", model.AmountLocal)
                    .Add("@Currency", model.Currency)
                    .Add("@Interval", model.Interval),
                Shaper = reader => reader.ReadValue<bool>("IsDuplicated")
            };

            return Get(request);
        }
    }
}