using System;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class TransactionDetailsModel
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionNumber { get; set; }
        public TransactionStatusEnum Status { get; set; }
        public CustomerModel Customer { get; set; }
        public BeneficiaryModel Beneficiary { get; set; }
        public MoneyTransferDetailsModel MoneyTransferDetails { get; set; }
        public ProcessingInformationModel ProcessingInformation { get; set; }
        public ComplianceInformationModel ComplianceInformation { get; set; }
        public TransactionStatusModel TransactionStatus { get; set; }
        public AgentModel ReceivingAgent { get; set; }
        public AgentModel PayingAgent { get; set; }
    }
}