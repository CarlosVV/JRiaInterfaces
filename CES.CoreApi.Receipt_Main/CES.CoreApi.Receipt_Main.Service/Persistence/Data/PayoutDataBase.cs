using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace CES.CoreApi.Shared.Persistence.Data
{
    public abstract class PersistenceDataBase : IDisposable
    {
        protected Collection<SqlParameter> _parameters = new Collection<SqlParameter>();
        protected string _sProcName = string.Empty;
        private bool disposed = false;

        public PersistenceDataBase(string spName)
        {
            _sProcName = spName;
        }

        public string LogSpCall()
        {
            return DatabaseName.LogSPCall(_sProcName, _parameters);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }

                disposed = true;
            }
        }

    }

}