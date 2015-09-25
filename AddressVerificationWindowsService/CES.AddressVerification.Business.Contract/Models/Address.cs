namespace CES.AddressVerification.Business.Contract.Models
{
    public class Address
    {
        public long Id { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}