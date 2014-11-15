using System;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Utilities;

namespace CES.CoreApi.Logging.Formatters
{
    public class GenericArgumentListFormatter : IGenericArgumentListFormatter
    {
        private const string GenericArgumentList = "<{0}>";

        #region Public methods
        
        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="argumentList">Type argument list</param>
        /// <returns></returns>
        public string Format(Type[] argumentList)
        {
            return argumentList != null
                       ? string.Format(CultureInfo.InvariantCulture,
                                       GenericArgumentList,
                                       argumentList.ToStringList())
                       : string.Empty;
        }

        #endregion //Public methods
    }
}