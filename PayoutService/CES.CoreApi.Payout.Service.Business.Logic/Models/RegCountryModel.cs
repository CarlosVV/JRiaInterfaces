using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class RegCountryModel
    {
        public int CountryID { get; set; }
        public string Country { get; set; }
        public string Abbrev { get; set; }
        public string Code { get; set; }
        public string ISOCode { get; set; }
        public string Note { get; set; }
        public int Order { get; set; }
        public bool UseCityList { get; set; }
        public string ISOCode_Alpha3 { get; set; }
        public string CountryCurrency { get; set; }
    }
}
