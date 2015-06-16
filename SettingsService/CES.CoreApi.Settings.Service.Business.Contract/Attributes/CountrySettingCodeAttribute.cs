using System;

namespace CES.CoreApi.Settings.Service.Business.Contract.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CountrySettingCodeAttribute : Attribute
    {
        public string Code { get; private set; }

        public CountrySettingCodeAttribute(string code)
        {
            Code = code;
        }
    }
}
