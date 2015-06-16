﻿using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Data
{
    public class ValidationMainRepository : BaseGenericRepository, IValidationMainRepository
    {
        public ValidationMainRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager)
            : base(cacheProvider, monitorFactory, identityManager, DatabaseType.Main)
        {
        }

        public bool IsDuplicateOrderExist(OrderDuplicateValidationRequestModel model)
        {
            var request = new DatabaseRequest<bool>
            {
                ProcedureName = "coreapi_sp_OrderDuplicateValidation",
                IsCacheable = false,
                Parameters = new Collection<SqlParameter>()
                    .Add("@RecAgentID", model.RecAgentId)
                    .Add("@RecAgentLocationID", model.RecAgentLocationId)
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