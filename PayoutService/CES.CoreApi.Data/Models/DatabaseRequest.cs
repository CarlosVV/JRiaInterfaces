using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
//using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Data.Enumerations;

namespace CES.CoreApi.Data.Models
{
    public class DatabaseRequest<TEntity>
    {
        public string ProcedureName { get; set; }
        public Func<IDataReader, TEntity> Shaper { get; set; }
        public Func<DbParameterCollection, TEntity> OutputFuncShaper { get; set; }
        public Action<DbParameterCollection, TEntity> OutputShaper { get; set; }
        public ICollection<SqlParameter> Parameters { get; set; }
        public bool IncludeConventionParameters { get; set; }
        public bool IsCacheable { get; set; }
        public TimeSpan CacheDuration { get; set; }
        public string CacheRegion { get; set; }
        public DatabaseType DatabaseType { get; set; }
        public int DatabaseId { get; set; }
        public Func<TEntity, bool> CacheInvalidator { get; set; }
        public string CacheKeySuffix { get; set; }
    }
}
