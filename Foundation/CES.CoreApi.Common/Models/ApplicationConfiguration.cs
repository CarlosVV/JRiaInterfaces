using System;
using System.Runtime.Serialization;

namespace CES.CoreApi.Common.Models
{
    [DataContract]
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            //if (string.IsNullOrEmpty(value))
            //    throw new ArgumentNullException("value");

            Name = name;
            Value = value;
        }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Value { get; private set; }
    }
}
