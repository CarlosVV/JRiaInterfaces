using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static int ToI(this string str)
        {
            int val;
            if(int.TryParse(str, out val))
            {
                return val;
            }
            return val;
        }
    }
}
