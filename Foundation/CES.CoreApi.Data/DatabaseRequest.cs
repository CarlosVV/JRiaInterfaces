using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CES.CoreApi.Foundation.Data
{
    public class DatabaseRequest<TEntity>
    {
        public string ProcedureName { get; set; }
        public Func<IDataReader, TEntity> Shaper { get; set; }
        public Action<DbParameterCollection, TEntity> OutputShaper { get; set; }
        public ICollection<SqlParameter> Parameters { get; set; }
        public bool IncludeConventionParameters { get; set; }
        public bool IsCacheable { get; set; }
        public TimeSpan CacheDuration { get; set; }
        public string CacheRegion { get; set; }
    }
}
