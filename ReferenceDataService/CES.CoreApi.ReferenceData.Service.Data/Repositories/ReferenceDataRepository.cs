using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Enumerations;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Interfaces;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Models;

namespace CES.CoreApi.ReferenceData.Service.Data.Repositories
{
    public class ReferenceDataRepository : BaseGenericRepository, IReferenceDataRepository
    {
        #region Core

        private const int OneHourTimeout = 1;

        public ReferenceDataRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager)
            : base(cacheProvider, monitorFactory, identityManager, DatabaseType.Main)
        {
        }

        #endregion

        #region Public methods

        public IEnumerable<IdentificationTypeModel> GetByDataType(int locationDepartmentId, ReferenceDataType dataType)
        {
            var request = new DatabaseRequest<IdentificationTypeModel>
            {
                ProcedureName = "ol_sp_systblUserConst_GetAllByfKey1",
                IsCacheable = true,
                CacheDuration = TimeSpan.FromHours(OneHourTimeout),
                Parameters = new Collection<SqlParameter>()
                    .Add("@fID", locationDepartmentId)
                    .Add("@fKey1", (int) dataType),
                Shaper = reader => GetIdentificationTypeDetails(reader)
            };

            return GetList(request);
        }

        public DatabasePingModel Ping()
        {
            return PingDatabase();
        }

        #endregion

        #region Private methods
        
        private static IdentificationTypeModel GetIdentificationTypeDetails(IDataReader reader)
        {
            var description = reader.ReadValue<string>("fVal");
            return new IdentificationTypeModel
            {
                Id = reader.ReadValue<int>("fKey2"),
                LocationDepartmentId = reader.ReadValue<int>("fID"),
                Category = reader.ReadValue<string>("fDesc"),
                Name = reader.ReadValue<string>("fName"),
                Country = reader.ReadValue<string>("fVal2"),
                IsExpirationNotRequired = !string.IsNullOrEmpty(description) && description.Contains("No Exp;")
            };
        } 

        #endregion
    }
}
