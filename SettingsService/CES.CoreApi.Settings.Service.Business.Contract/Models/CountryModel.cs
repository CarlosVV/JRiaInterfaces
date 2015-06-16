namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class CountryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Code { get; set; }

        public string IsoCode { get; set; }

        public string Note { get; set; }

        public int Order { get; set; }

        public bool UseCityList { get; set; }

        public string IsoCodeAlpha3 { get; set; }

        public string Currency { get; set; }
    }
}