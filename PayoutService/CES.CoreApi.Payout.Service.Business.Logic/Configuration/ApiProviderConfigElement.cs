using System.Configuration;


namespace CES.CoreApi.Payout.Service.Business.Logic.Configuration
{
    public class ApiProviderConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("providerName", DefaultValue = "", IsRequired = true)]
        public string ProviderName
        {
            get
            {
                return (string)this["providerName"];
            }
            set
            {
                this["providerName"] = value;
            }
        }

        [ConfigurationProperty("methods")]
        public MethodConfigElementCollection Methods
        {
            get
            {
                return ((MethodConfigElementCollection)(base["methods"]));
            }
            set
            {
                this["methods"] = value;
            }
        }
    }
}
