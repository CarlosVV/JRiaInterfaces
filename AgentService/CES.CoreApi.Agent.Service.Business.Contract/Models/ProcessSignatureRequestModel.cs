namespace CES.CoreApi.Agent.Service.Business.Contract.Models
{
    public class ProcessSignatureRequestModel
    {
        public int AgentId { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public byte[] Signature { get; set; }
    }
}
