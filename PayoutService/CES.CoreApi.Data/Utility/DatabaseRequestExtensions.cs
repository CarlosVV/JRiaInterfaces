using System.Globalization;
using System.Text;
using CES.CoreApi.Data.Models;

namespace CES.CoreApi.Foundation.Data.Utility
{
    public static class DatabaseRequestExtensions
    {
        private const string KeyTemplate = "{0}_{1}_{2}";
        private const string SqlParameterKeyTemplate = "{0}_{1}_";

        public static string ToCacheKey<TEntity>(this DatabaseRequest<TEntity> request, string cacheKeySuffix = null)
        {
            var parameters = SerializeParameters(request);
            return string.Format(CultureInfo.InvariantCulture, KeyTemplate, request.ProcedureName, parameters, cacheKeySuffix);
        }

        private static string SerializeParameters<TEntity>(DatabaseRequest<TEntity> request)
        {
            if (request.Parameters == null)
                return string.Empty;

            var keyBuilder = new StringBuilder();

            foreach (var parameter in request.Parameters)
            {
                keyBuilder.AppendFormat(SqlParameterKeyTemplate, parameter.ParameterName, parameter.Value);
            }

            return keyBuilder.ToString();
        }
    }
}
