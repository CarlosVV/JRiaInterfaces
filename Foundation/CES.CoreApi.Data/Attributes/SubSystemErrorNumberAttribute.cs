using System;

namespace CES.CoreApi.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SubSystemErrorNumberAttribute : Attribute
    {
        public SubSystemErrorNumberAttribute(string errorNumber)
        {
            ErrorNumber = errorNumber;
        }

        public string ErrorNumber { get; private set; }
    }
}