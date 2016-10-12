using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.BankAccount.Api.Utilities
{
    public static class TextUtil
    {
        public  static string GetStr(object ob, string sDefault = "")
        {
            string sOut = "";
            if ((ob != null) && (ob != DBNull.Value)) sOut = ob.ToString();
            if (string.IsNullOrWhiteSpace(sOut)) sOut = sDefault;

            return sOut;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}