using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class CustomerStatusValidationModel
    {
        public bool IsOnHold { get; set; }
        public int CustomerId { get; set; }
        public CustomerValidationEntityType CustomerType { get; set; }
    }
}
