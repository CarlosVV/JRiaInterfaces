using System;

namespace CES.CoreApi.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ErrorMessageAttribute : Attribute
    {
        public ErrorMessageAttribute(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
