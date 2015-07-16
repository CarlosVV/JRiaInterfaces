namespace CES.CoreApi.Shared.Business.Contract.Models
{
    public class AgentLocationModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string BranchNumber { set; get; }
        public AddressModel Address { get; set; }
        public int TimeZoneId { set; get; }
        public double TimeZoneOffset { get; set; }
    }
}
