using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.RiaDatabase
{
    /// <summary>
    /// Utilities for handling SQL formatting with DB.
    /// 
    /// Author  : David Go
    /// </summary>
    public class SQLUtils
    {

        /// <summary>
        /// Converts any SQL null into a C# null:
        /// 
        /// Checks the object for a DB null type value.
        /// If the object is DBNull, then return a C# null.
        /// If the object is not DBNull, then just return the object back.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object ConvertDBNullToCNull(object o)
        {
            if (o == DBNull.Value)
            {
                return null;
            }
            else
            {
                return o;
            }
        }

    }
}
