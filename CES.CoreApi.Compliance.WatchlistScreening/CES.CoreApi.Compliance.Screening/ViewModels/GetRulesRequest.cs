using CES.CoreApi.Compliance.Screening.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Attributes;
using CES.CoreApi.Compliance.Screening.Utilities;

namespace CES.CoreApi.Compliance.Screening.ViewModels
{
    [Validator(typeof(GetRulesRequestValidator))]
    public class GetRulesRequest
    {
        public DateTime TransDateTime { get; set; }
        public int RuntimeID { get; set; }
        public ServiceIdType ServiceId { get; set; }
        public int ProductId { get; set; }
        public int CountryFromId { get; set; }
        public int CountryToId { get; set; }
        public int ReceivingAgentID { get; set; }
        public int ReceivingAgentLocID { get; set; }
        public int PayAgentID { get; set; }
        public int PayAgentLocID { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string EntryType { get; set; }
        public string SendCurrency { get; set; }
        public double SendAmount { get; set; }
        public double SendTotalAmount { get; set; }
        public PartyType PartyType { get; set; }


    }

    class GetRulesRequestValidator : AbstractValidator<GetRulesRequest>
    {
        public GetRulesRequestValidator()
        {
            RuleFor(r => r.TransDateTime).NotNull().WithMessage("TransDateTime is required").Must(ViewModelUtil.ValidDate).WithMessage("TransDateTime is required");
            RuleFor(r => r.RuntimeID).NotNull().WithMessage("RuntimeID is required").GreaterThan(0).WithMessage("RuntimeID is required");
            RuleFor(x => x.ServiceId).Must(ViewModelUtil.ValidServiceIdType).WithMessage("ServiceId is invalid");
            RuleFor(r => r.ProductId).NotNull().WithMessage("ProductId is required").GreaterThan(0).WithMessage("ProductId is required");
            RuleFor(r => r.CountryFromId).NotNull().WithMessage("CountryFromId is required").GreaterThan(0).WithMessage("CountryFromId is required");
            RuleFor(r => r.CountryToId).NotNull().WithMessage("CountryToId is required").GreaterThan(0).WithMessage("CountryToId is required");
            //Receiving Agent            
            RuleFor(r => r.ReceivingAgentID).NotNull().WithMessage("ReceivingAgentID is required").GreaterThan(0).WithMessage("ReceivingAgentID is required");
            RuleFor(r => r.ReceivingAgentLocID).NotNull().WithMessage("ReceivingAgentLocID is required").GreaterThan(0).WithMessage("ReceivingAgentLocID is required");
            //Pay Agent          
            RuleFor(r => r.PayAgentID).NotNull().WithMessage("PayAgentID is required").GreaterThan(0).WithMessage("PayAgentID is required");
            RuleFor(r => r.PayAgentLocID).NotNull().WithMessage("PayAgentLocID is required").GreaterThan(0).WithMessage("PayAgentLocID is required");
            RuleFor(x => x.DeliveryMethod).Must(ViewModelUtil.ValidSDeliveryMethod).WithMessage("DeliveryMethod is invalid");
            RuleFor(x => x.EntryType).NotEmpty().WithMessage("EntryType is required").Length(1, 100).WithMessage("EntryType invalid lenght");
            RuleFor(x => x.SendCurrency).NotEmpty().WithMessage("SendCurrency is required").Length(3, 3).WithMessage("SendCurrency invalid lenght");
            RuleFor(r => r.SendAmount).NotNull().WithMessage("SendAmount is required").GreaterThan(0).WithMessage("SendAmount is required");
            RuleFor(r => r.SendTotalAmount).NotNull().WithMessage("SendTotalAmount is required").GreaterThan(0).WithMessage("SendTotalAmount is required");
            RuleFor(x => x.PartyType).Must(ViewModelUtil.ValidPartyType).WithMessage("PartyType is invalid");

        }


    }
}

