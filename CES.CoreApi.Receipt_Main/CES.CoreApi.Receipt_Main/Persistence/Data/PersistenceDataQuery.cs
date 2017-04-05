using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CES.Data.Sql;

namespace CES.CoreApi.Shared.Persistence.Data
{
    public class PersistenceDataQuery : PersistenceDataBase, IDataQuery
    {

        public readonly IDataQuery DataQuery = null;
        public readonly IDataQueryAgain DataQueryAgain = null;
        private bool disposed = false;

        public PersistenceDataQuery(DataQuery query, string spName) : base(spName)
        {
            DataQuery = query;
            DataQueryAgain = query;
        }

        public void AddParam(string parameterName, object value)
        {
            _parameters.Add(new SqlParameter(parameterName, value));
            DataQuery.AddParam(parameterName, value);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DataQuery.Dispose();
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }

        public void AddParam(string parameterName, IEnumerable<int> ids)
        {

            throw new NotImplementedException();
        }

        public SqlParameter AddOutputParam(string parameterName, SqlDbType dbType, int size = 0)
        {
            var parameter = new SqlParameter(parameterName, dbType, size)
            {
                Direction = ParameterDirection.Output
            };

            _parameters.Add(parameter);

            return DataQuery.AddOutputParam(parameterName, dbType, size);
        }

        public Task<SqlDataReader> ExecuteReaderAsync()
        {
            throw new NotImplementedException();
        }

        public SqlDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }
    }

}