namespace CES.CoreApi.Compliance.Screening.Models
{
    public class Agent
    {
        public int ID { get; set; }
        public int LocID { get; set; }
        public string Branch { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

    }

}