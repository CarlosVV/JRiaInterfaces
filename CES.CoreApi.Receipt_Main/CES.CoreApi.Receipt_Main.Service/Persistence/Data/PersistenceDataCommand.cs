using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CES.Data.Sql;

namespace CES.CoreApi.Shared.Persistence.Data
{
    public class PersistenceDataCommand : PersistenceDataBase, IDataCommand
    {

        public readonly IDataCommand Command = null;
        private bool disposed = false;

        public PersistenceDataCommand(DataCommand command, string spName) : base(spName)
        {
            Command = command;
        }

        public void AddParam(string parameterName, object value)
        {
            _parameters.Add(new SqlParameter(parameterName, value));
            Command.AddParam(parameterName, value);
        }

        public SqlParameter AddParam(string parameterName, object value, ParameterDirection direction, SqlDbType dbType, int size = 0)
        {
            var parameter = new SqlParameter(parameterName, dbType, size)
            {
                Direction = direction,
                Value = value
            };
            _parameters.Add(parameter);
            return Command.AddParam(parameterName, value, direction, dbType, size);
        }



        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Command.Dispose();
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }

        public SqlParameter AddOutputParam(string parameterName, SqlDbType dbType, int size = 0)
        {
            var parameter = new SqlParameter(parameterName, dbType, size)
            {
                Direction = ParameterDirection.Output
            };

            _parameters.Add(parameter);
            return Command.AddOutputParam(parameterName, dbType, size);
        }
    }

}