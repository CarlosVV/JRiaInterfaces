using System;
using System.Configuration;

namespace CES.CoreApi.Payout.Service.Business.Logic.Configuration
{
    public class ConfigStrinElement : ConfigurationSection
    {
        [ConfigurationProperty("value", DefaultValue = "", IsRequired = false)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
    }
}
