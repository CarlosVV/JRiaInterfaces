using System.Configuration;


namespace CES.CoreApi.Payout.Service.Business.Logic.Configuration
{
    public class MethodConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("urlBase", DefaultValue = "", IsRequired = true)]
        public string UrlBase
        {
            get
            {
                return (string)this["urlBase"];
            }
            set
            {
                this["urlBase"] = value;
            }
        }

        [ConfigurationProperty("methodName", DefaultValue = "", IsRequired = true)]
        public string MethodName
        {
            get
            {
                return (string)this["methodName"];
            }
            set
            {
                this["methodName"] = value;
            }
        }
        [ConfigurationProperty("uri", DefaultValue = "", IsRequired = true)]
        public string Uri
        {
            get
            {
                return (string)this["uri"];
            }
            set
            {
                this["uri"] = value;
            }
        }
    }
}
