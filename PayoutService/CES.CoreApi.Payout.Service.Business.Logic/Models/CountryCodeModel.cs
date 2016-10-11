using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class CountryCodeModel
    {
      
        public string Char2CountryCode {get;set;}
        public string Char3ISOCountryCode { get; set; }
        public int ISONumericCode { get; set; }
        public string CountryDescription { get; set; }
        public bool ResultSetHasRows { get; set; }

    }
}
