using CES.CoreApi.Receipt_Main.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Utilities
{
    public static class Enumerations
    {
        public static string Description(this Enum value)
        {
            // variables  
            var enumType = value.GetType();
            var sValue = value.ToString();
            var field = enumType.GetField(sValue);

            if (field == null)
            {
                field = enumType.GetField("CouldNotTranslateErrorMessage");
            }

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // return  
            return attributes.Length == 0 ? value.ToString() : $"{((DescriptionAttribute)attributes[0]).Description}";
        }
        public static string ToNumberString(this CoreApiBadRequest value)
        {
            return ((int)value).ToString();
        }
        public static string ToNumberString(this ValidationCode value)
        {
            return ((int)value).ToString();
        }

        public static string ToNumberString(this CoreApiErrorCode value)
        {
            return ((int)value).ToString();
        }

        public static int ToNumber(this CoreApiBadRequest value)
        {
            return (int)value;
        }
        public static int ToNumber(this ValidationCode value)
        {
            return (int)value;
        }

        public static int ToNumber(this CoreApiErrorCode value)
        {
            return (int)value;
        }
    }
}