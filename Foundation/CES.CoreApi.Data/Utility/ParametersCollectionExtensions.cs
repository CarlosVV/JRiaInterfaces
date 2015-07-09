using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CES.CoreApi.Common.Tools;

namespace CES.CoreApi.Foundation.Data.Utility
{
    public static class ParametersCollectionExtensions
    {
        public static ICollection<SqlParameter> Add(this ICollection<SqlParameter> parameters, string name, object value)
        {
            var parameter = new SqlParameter(name, value) {Direction = ParameterDirection.Input};
            parameters.Add(parameter);
            return parameters;
        }

        public static ICollection<SqlParameter> AddVarCharOut(this ICollection<SqlParameter> parameters, string name, int size)
        {
            var parameter = new SqlParameter(name, SqlDbType.VarChar, size) {Direction = ParameterDirection.Output};
            parameters.Add(parameter);
            return parameters;
        }

        public static ICollection<SqlParameter> AddIntOut(this ICollection<SqlParameter> parameters, string name)
        {
            var parameter = new SqlParameter(name, SqlDbType.Int) { Direction = ParameterDirection.Output };
            parameters.Add(parameter);
            return parameters;
        }

        public static ICollection<SqlParameter> AddBitOut(this ICollection<SqlParameter> parameters, string name)
        {
            var parameter = new SqlParameter(name, SqlDbType.Bit) { Direction = ParameterDirection.Output };
            parameters.Add(parameter);
            return parameters;
        }

        public static T ReadValue<T>(this DbParameterCollection parameters, string name)
        {
            return parameters[name].Value.ConvertValue<T>();
        }
    }
}