using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Compliance.Service.Business.Contract.Configuration
{
    public class CountryConfiguration
    {
        public string CountryCode { get; set; }
        public List<DataProviderConfiguration> DataProviders { get; set; }
    }
}
