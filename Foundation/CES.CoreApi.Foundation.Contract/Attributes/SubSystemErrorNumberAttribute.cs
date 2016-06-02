using System;

namespace CES.CoreApi.Foundation.Contract.Attributes
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