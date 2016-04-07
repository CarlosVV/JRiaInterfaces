using System;

namespace CES.CoreApi.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConnectionNameAttribute : Attribute
    {
        public ConnectionNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}