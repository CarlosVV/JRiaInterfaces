using System;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int PreOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public DateTime PaymentAvailableDate { get; set; }
        public BeneficiaryModel Beneficiary { get; set; }
    }
}