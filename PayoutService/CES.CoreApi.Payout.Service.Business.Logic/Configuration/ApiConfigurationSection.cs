using System;
using System.Configuration;


namespace CES.CoreApi.Payout.Service.Business.Logic.Configuration
{
    public class ApiConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ApiProviderConfigElementCollection Providers
        {
            get
            {
                return ((ApiProviderConfigElementCollection)(base["providers"]));
            }
            set
            {
                this["providers"] = value;
            }
        }    

      
    }
}
