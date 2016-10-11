using CES.CoreApi.Compliance.Screening.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Attributes;
using CES.CoreApi.Compliance.Screening.Utilities;

namespace CES.CoreApi.Compliance.Screening.ViewModels
{
    [Validator(typeof(ScreeningRequestValidator))]
    public class ScreeningRequest
    {
        public long? OrderId { get; set; }
        public string OrderNo { get; set; }
        public string OrderPin { get; set; }
        public CallEventType CallEvent { get; set; }
        public ServiceIdType ServiceId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string EntryType { get; set; }
        public int ProductId { get; set; }
        public int RuntimeID { get; set; }
        public  DateTime TransDateTime { get; set; }
        public string CountryFrom { get; set; }
        public int CountryFromId { get; set; }
        public string StateFrom { get; set; }
        public string CountryTo { get; set; }
        public int CountryToId { get; set; }
        public string StateTo { get; set; }
        public string SendCurrency { get; set; }
        public double SendAmount { get; set; }
        public double SendTotalAmount {  get; set; }
        public string PayoutCurrency { get; set; }
        public double PayoutAmount { get; set; }
        public string ProductCode { get; set; }
        public string TransferReason { get; set; }
        public Agent ReceivingAgent { get; set; }
        public Agent PayAgent { get; set; }
        public IEnumerable<Party> Parties { get; set; }
    }
    class ScreeningRequestValidator : AbstractValidator<ScreeningRequest>
    {
        public ScreeningRequestValidator()
        {
            RuleFor(r => r.OrderId).NotNull().WithMessage("OrderId is required").GreaterThan(0).WithMessage("OrderId is required");
            RuleFor(r => r.OrderNo).NotNull().WithMessage("OrderNo is required").NotEmpty().WithMessage("OrderNo is required").Length(1, 50).WithMessage("OrderNo invalid lenght");
            RuleFor(x => x.CallEvent).Must(ViewModelUtil.ValidCallEventType).WithMessage("CallEvent is invalid");
            RuleFor(x => x.ServiceId).Must(ViewModelUtil.ValidServiceIdType).WithMessage("ServiceId is invalid");
            RuleFor(x => x.EntryType).NotEmpty().WithMessage("EntryType is required").Length(1, 100).WithMessage("EntryType invalid lenght");
            RuleFor(r => r.ProductId).NotNull().WithMessage("ProductId is required").GreaterThan(0).WithMessage("ProductId is required");
            RuleFor(r => r.RuntimeID).NotNull().WithMessage("RuntimeID is required").GreaterThan(0).WithMessage("RuntimeID is required");
            RuleFor(r => r.TransDateTime).NotNull().WithMessage("TransDateTime is required").Must(ViewModelUtil.ValidDate).WithMessage("TransDateTime is required");
            RuleFor(r => r.CountryFrom).NotNull().WithMessage("CountryFrom is required").NotEmpty().WithMessage("CountryFrom must not be empty").Length(2, 2).WithMessage("CountryFrom invalid lenght");
            RuleFor(r => r.CountryFromId).NotNull().WithMessage("CountryFromId is required").GreaterThan(0).WithMessage("CountryFromId is required");
            RuleFor(r => r.CountryTo).NotEmpty().WithMessage("CountryTo must not be empty").Length(2, 2).WithMessage("CountryTo invalid lenght").NotNull().WithMessage("CountryTo is required");
            RuleFor(r => r.CountryToId).NotNull().WithMessage("CountryToId is required").GreaterThan(0).WithMessage("CountryToId is required");
            RuleFor(r => r.ProductCode).Length(2, 2).WithMessage("ProductCode is required");

            //Receiving Agent
            RuleFor(r => r.ReceivingAgent).NotNull().WithMessage("ReceivingAgent is required");
            RuleFor(r => r.ReceivingAgent.ID).NotNull().WithMessage("ReceivingAgent.ID is required").GreaterThan(0).WithMessage("ReceivingAgent.ID is required");
            RuleFor(r => r.ReceivingAgent.LocID).NotNull().WithMessage("ReceivingAgent.LocID is required").GreaterThan(0).WithMessage("ReceivingAgent.LocID is required");

            //Pay Agent
            RuleFor(r => r.PayAgent).NotNull().WithMessage("PayAgent is required");
            RuleFor(r => r.PayAgent.ID).NotNull().WithMessage("PayAgent.ID is required").GreaterThan(0).WithMessage("PayAgent.ID is required");
            RuleFor(r => r.PayAgent.LocID).NotNull().WithMessage("PayAgent.LocID is required").GreaterThan(0).WithMessage("PayAgent.LocID is required");


            RuleFor(r => r.Parties).Must(p => p != null && p.Count() > 0).WithMessage("Please fill Party items");
            RuleForEach(request => request.Parties)
                .Must((request, party) => party.Type != PartyType.Undefined).WithMessage("Party Type is invalid. Id={0}, Type={1}", (request, party) => party.Id, (request, party) => party.Type)
                .Must((request, party) => party.Id > 0).WithMessage("Id in {0} must be greater than zero", (request, party) => party.Type)
                .Must((request, party) => !string.IsNullOrEmpty(party.Number)).WithMessage("Number in {0} is required", (request, party) => party.Type)
                .Must((request, party) => !string.IsNullOrEmpty(party.FirstName)).WithMessage("First Name in {0} is required", (request, party) => party.Type)
                .Must((request, party) => !string.IsNullOrEmpty(party.LastName1)).WithMessage("Last Name1 in {0} is required", (request, party) => party.Type);



            //Runtime/IDs validations
            //int RUNTIME_SEND = 1;
            //int RUNTIME_PAY = 2;
            //When(request => request.RuntimeID == RUNTIME_SEND, () =>
            //{
            //    RuleFor(r => r.Parties.First(p => p.Type == GetPartyTypeByRuntime(RUNTIME_SEND))).Must(x => x.IDs != null && x.IDs.Any()).WithMessage($"IDs of {GetPartyTypeByRuntime(RUNTIME_SEND).ToString()} party is required.");
            //});

            //When(request => request.RuntimeID == RUNTIME_PAY, () =>
            //{
            //    RuleFor(r => r.Parties.First(p => p.Type == GetPartyTypeByRuntime(RUNTIME_PAY))).Must(x => x.IDs != null && x.IDs.Any()).WithMessage($"IDs of {GetPartyTypeByRuntime(RUNTIME_PAY).ToString()} party is required.");
            //});

            //When(request => !(new int[] { RUNTIME_SEND, RUNTIME_PAY }).Contains(request.RuntimeID ), () =>
            //{
            //    RuleForEach(request => request.Parties).Must((request, party) => party.IDs != null && party.IDs.Any()).WithMessage("IDs of party {0} is required.", (request, party) => party.Type);
            //});
        }
        


    }

   
}