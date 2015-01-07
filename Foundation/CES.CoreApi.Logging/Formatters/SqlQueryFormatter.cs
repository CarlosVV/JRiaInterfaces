using System;
using System.Data;
using System.Globalization;
using System.Text;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Formatters
{
    public class SqlQueryFormatter : ISqlQueryFormatter
    {
        private const string StoredProcedureTemplate = "{0} {1}";
        private const string InParameterTemplate = "{0} = '{1}',";
        private const string OutParameterTemplate = "{0} = '{1}' OUTPUT,";
        private const string ParameterSeparator = ",";

        public string Format(DatabasePerformanceLogDataContainer container)
        {
            if (container == null) 
                throw new ArgumentNullException("container");

            if (container.CommandType != CommandType.StoredProcedure)
                return string.Empty;

            if (container.Parameters == null)
                return container.CommandText;

            var parameterListBuilder = new StringBuilder();

            foreach (var parameter in container.Parameters)
            {
                if (parameter.Direction == ParameterDirection.Input)
                    parameterListBuilder.AppendFormat(CultureInfo.InvariantCulture, InParameterTemplate, parameter.Name, parameter.Value);
                if(parameter.Direction == ParameterDirection.Output)
                    parameterListBuilder.AppendFormat(CultureInfo.InvariantCulture, OutParameterTemplate, parameter.Name, parameter.Value);
            }

            var parameterList = parameterListBuilder.Length > 0 
                ? parameterListBuilder.ToString().TrimEnd(ParameterSeparator.ToCharArray())
                : string.Empty;

            return string.Format(CultureInfo.InvariantCulture,
                StoredProcedureTemplate,
                container.CommandText,
                parameterList);
        }
    }
}
