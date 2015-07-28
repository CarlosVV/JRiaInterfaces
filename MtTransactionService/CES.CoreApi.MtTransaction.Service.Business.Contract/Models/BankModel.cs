using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Models
{
    public class BankModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BranchNumber { get; set; }
        public string BranchName { get; set; }
        public string SwiftCode { get; set; }
        public string RoutingCode { get; set; }
        public int RoutingType { get; set; }
        public AddressModel Address { get; set; }
        public int LocationId { get; set; }
        public int ServiceLevelId { get; set; }
    }
}