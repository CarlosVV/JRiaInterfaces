using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Model
{
    public static class DataReaderExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetSafeValue<T>(this IDataReader reader, string name)
        {
            try
            {
                return reader[name].GetSafeValue<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public static T GetSafeValue<T>(this object value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }


        }
    }
}
