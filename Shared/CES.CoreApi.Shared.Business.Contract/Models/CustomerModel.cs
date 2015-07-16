using System;

namespace CES.CoreApi.Shared.Business.Contract.Models
{
    public class CustomerModel: PersonModel
    {
        public bool? IsChanged { get; set; }
        public DateTime LastUsed { get; set; }
        public DateTime DateCreated { get; set; }
        public string CustomerId { get; set; }
        public string ExternalCustomerId { get; set; }
        public string ReferredBy { get; set; }
        public int AgentId { get; set; }
        public int StatusId { get; set; }
    }
}
