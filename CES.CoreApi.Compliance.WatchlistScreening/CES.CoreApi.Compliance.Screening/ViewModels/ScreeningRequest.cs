using CES.CoreApi.Compliance.Screening.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Attributes;

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
            RuleFor(x => x.CallEvent).Must(ValidCallEventType).WithMessage("CallEvent is invalid");
            RuleFor(x => x.ServiceId).Must(ValidServiceIdType).WithMessage("ServiceId is invalid");
            RuleFor(r => r.ProductId).NotNull().WithMessage("ProductId is required").GreaterThan(0).WithMessage("ProductId is required");
            RuleFor(r => r.RuntimeID).NotNull().WithMessage("RuntimeID is required").GreaterThan(0).WithMessage("RuntimeID is required");
            RuleFor(r => r.TransDateTime).NotNull().WithMessage("TransDateTime is required").Must(ValidDate).WithMessage("TransDateTime is required");
            RuleFor(r => r.CountryFrom).NotNull().WithMessage("CountryFrom is required").NotEmpty().WithMessage("CountryFrom must not be empty").Length(2, 2).WithMessage("CountryFrom invalid lenght");
            RuleFor(r => r.CountryFromId).NotNull().WithMessage("CountryFromId is required").GreaterThan(0).WithMessage("CountryFromId is required");
            RuleFor(r => r.StateFrom).NotEmpty().WithMessage("StateFrom must not be empty").Length(2, 2).WithMessage("StateFrom invalid lenght").NotNull().WithMessage("StateFrom is required");
            RuleFor(r => r.CountryTo).NotEmpty().WithMessage("CountryTo must not be empty").Length(2, 2).WithMessage("CountryTo invalid lenght").NotNull().WithMessage("CountryTo is required");
            RuleFor(r => r.CountryToId).NotNull().WithMessage("CountryToId is required").GreaterThan(0).WithMessage("CountryToId is required");
            RuleFor(r => r.StateTo).NotEmpty().WithMessage("StateTo must not be empty").Length(2, 2).WithMessage("StateTo invalid lenght").NotNull().WithMessage("StateFrom is required");
            RuleFor(r => r.SendCurrency).Length(3, 3).WithMessage("SendCurrency is required").Length(3, 3).WithMessage("SendCurrency invalid lenght");
            RuleFor(r => r.SendAmount).NotNull().WithMessage("SendAmount is required").GreaterThan(0).WithMessage("SendAmount is required");
            RuleFor(r => r.SendTotalAmount).NotNull().WithMessage("SendTotalAmount is required").GreaterThan(0).WithMessage("SendTotalAmount is required");
            RuleFor(r => r.PayoutCurrency).NotNull().WithMessage("PayoutCurrency is required").NotEmpty().WithMessage("PayoutCurrency is required").Length(3, 3).WithMessage("PayoutCurrency invalid lenght");
            RuleFor(r => r.PayoutAmount).GreaterThan(0).WithMessage("PayoutAmount is required");
            RuleFor(r => r.ProductCode).Length(2, 2).WithMessage("ProductCode is required");



            
            RuleFor(r => r.Parties).Must(p => p != null && p.Count() > 0).WithMessage("Please fill Party items");
            RuleForEach(request => request.Parties).Must((request, party) => party.Id > 0).WithMessage("Party.Id must be greater than zero")
                .Must((request, party) => party.Type != PartyType.Undefined).WithMessage("Party Type is invalid. Id={0}, Type={1}",
                (request, party) => party.Id,
                (request, party) => party.Type)
                .Must((request, party) => !string.IsNullOrEmpty(party.FirstName)).WithMessage("First Name in {0} is required",
                (request, party) => party.Type)
                .Must((request, party) => !string.IsNullOrEmpty(party.LastName1)).WithMessage("Last Name1 in {0} is required",
                (request, party) => party.Type)
                 .Must((request, party) => !string.IsNullOrEmpty(party.Number)).WithMessage("Number in {0} is required",
                (request, party) => party.Type);


            //Runtime/IDs validations
            int RUNTIME_SEND = 1;
            int RUNTIME_PAY = 2;
            When(request => request.RuntimeID == RUNTIME_SEND, () =>
            {
                RuleFor(r => r.Parties.First(p => p.Type == GetPartyTypeByRuntime(RUNTIME_SEND))).Must(x => x.IDs != null && x.IDs.Any()).WithMessage($"IDs of {GetPartyTypeByRuntime(RUNTIME_SEND).ToString()} party is required.");
            });

            When(request => request.RuntimeID == RUNTIME_PAY, () =>
            {
                RuleFor(r => r.Parties.First(p => p.Type == GetPartyTypeByRuntime(RUNTIME_PAY))).Must(x => x.IDs != null && x.IDs.Any()).WithMessage($"IDs of {GetPartyTypeByRuntime(RUNTIME_PAY).ToString()} party is required.");
            });

            When(request => !(new int[] { RUNTIME_SEND, RUNTIME_PAY }).Contains(request.RuntimeID ), () =>
            {
                RuleForEach(request => request.Parties).Must((request, party) => party.IDs != null && party.IDs.Any()).WithMessage("IDs of party {0} is required.", (request, party) => party.Type);
            });
        }


        private bool ValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool ValidCallEventType(CallEventType value)
        {
            return value != CallEventType.Undefined ;
        }

        private bool ValidServiceIdType(ServiceIdType value)
        {
            return  value != ServiceIdType.Undefined;
        }
        
        private PartyType GetPartyTypeByRuntime(int runTime)
        {  
            switch(runTime)
            {
                case 1:
                    return PartyType.Customer;
                case 2:
                    return PartyType.Beneficiary ;
                default:
                    return PartyType.Undefined;
            }

        }


    }

   
}