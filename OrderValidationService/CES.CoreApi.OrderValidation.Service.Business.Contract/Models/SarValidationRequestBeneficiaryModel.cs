namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class SarValidationRequestBeneficiaryModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Telephone { get; set; }
        public string AccountRoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string TaxId { get; set; }
        public string TaxIdCountry { get; set; }
        public int IdType { get; set; }
        public string IdNumber { get; set; }
    }
}