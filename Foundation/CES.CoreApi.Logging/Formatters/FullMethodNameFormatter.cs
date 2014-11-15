using System;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
    public class FullMethodNameFormatter : IFullMethodNameFormatter
    {
        private const string MethodName = "{0}.{1}";

        #region Public methods

        /// <summary>
        /// Gets full method name including fully qualified assembly name
        /// </summary>
        /// <param name="fullTypeName">Fully qualified type name</param>
        /// <param name="methodName">Method name</param>
        /// <returns></returns>
        public string Format(string fullTypeName, string methodName)
        {
            if (string.IsNullOrEmpty(fullTypeName))
                throw new ArgumentNullException("fullTypeName");
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException("methodName");

            return string.Format(CultureInfo.InvariantCulture,
                                 MethodName,
                                 fullTypeName,
                                 methodName);
        }

        #endregion //Public methods
    }
}