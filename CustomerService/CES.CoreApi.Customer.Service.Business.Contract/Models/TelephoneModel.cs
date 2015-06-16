using CES.CoreApi.Customer.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Customer.Service.Business.Contract.Models
{
    public class TelephoneModel
    {
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string Number { get; set; }
        public TelephoneKind Type { get; set; }
    }
}