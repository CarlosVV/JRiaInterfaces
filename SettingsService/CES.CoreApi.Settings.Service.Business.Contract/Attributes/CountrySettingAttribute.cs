using System;

namespace CES.CoreApi.Settings.Service.Business.Contract.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CountrySettingAttribute : Attribute
    {
        public string Code { get; private set; }
        public bool IsList { get; private set; }

        public string ListDelimiter { get; private set; }

        public CountrySettingAttribute(string code, bool isList = false, string listDelimiter = null)
        {
            Code = code;
            IsList = isList;
            ListDelimiter = listDelimiter;
        }
    }
}
