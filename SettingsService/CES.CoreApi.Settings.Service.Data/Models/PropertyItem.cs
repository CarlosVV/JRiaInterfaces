using System;
using System.Reflection;

namespace CES.CoreApi.Settings.Service.Data.Models
{
    internal class PropertyItem
    {
        public object OwnerObject { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public Type OwnerType { get; set; }
        public string Code { get; set; }
        public bool IsList { get; set; }
        public string ListDelimiter { get; set; }
    }
}