using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.CallLog.Tools
{
    public static class ExtensionMethods_ValueType
    {
        /// <summary>
        /// Returns a DBNull object if the value is an empty string
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static object DBNullIfEmpty<T>(this T arg) where T : struct
        {
            //@@2015-02-06 lb SCR# 2235011 Updated call

            if (arg.Equals(default(T)))
                return DBNull.Value;
            else
                return (object)arg;
        }
    }
}
