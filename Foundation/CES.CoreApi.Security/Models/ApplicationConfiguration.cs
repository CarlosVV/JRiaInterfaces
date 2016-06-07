using System;


namespace CES.CoreApi.Security.Models
{

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

  
        public string Name { get; private set; }

     
        public string Value { get; private set; }
    }
}
