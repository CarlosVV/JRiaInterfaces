namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class MonetaryInformationModel
    {
        public string CountryFrom { get; set; }
        public string CountryTo { get; set; }
        public CommissionDetailsModel CommissionDetails { get; set; }
        public AmountDetailsModel AmountDetails { get; set; }
        public RateDetailsModel RateDetails { get; set; }
        public CurrencyDetailsModel CurrencyDetails { get; set; }        
        public decimal Surcharge { get; set; }
        public int ProgramId { get; set; }
    }
}