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
        public bool IsOnHold { get; set; }
        public string OnHoldReason { get; set; }
        public bool IsDisabled { get; set; }
        public string Rating { get; set; }
        public ContactModel Contact { get; set; }
        public string Note { get; set; }
        public string NoteEnglish { get; set; }
    }
}
