using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CES.CoreApi.Payout.Service.Business.Contract.Enumerations
{
    public enum PayoutMessageCode
    {
        [Description("Paid Successful")]
        PaidSuccessful = 1,
        [Description("Undefined")]
        Undefined = 99,
        [Description("Missing App ID and App Object ID")]
        MissingAppID = 10,
        [Description("Missing Agent or User ID")]
        MissingAgentOrUserID = 11,
        [Description("Missing Agent Payout Location ID")]
        MissingAgentPayoutLocationID = 13,
        [Description("Missing Agent Payout Local Time")]
        MissingAgentPayoutLocalTime = 20,
        [Description("Payout Local Time is not valid (too far in the past).")]
        PayoutLocalTimeIsNotValidTooFarInThePast= 21,
        [Description("Payout Local Time is not valid (too far in the future).")]
        PayoutLocalTimeIsNotValidTooFarInTheFuture = 22,
        [Description("Agent Country not supplied")]
        AgentCountryNotSupplied = 30,
        [Description("Unable to find the order. Please verify the number and try again.")]
        UnableToFindTheOrder = 80,
        [Description("Order Does Not Exist")]
        OrderDoesNotExist = 90,
        [Description("Order Voided")]
        OrderVoided = 101,
        [Description("Order Canceled")]
        OrderCanceled = 102,
        [Description("Order Not Available")]
        OrderNotAvailable = 103,
        [Description("Order Not Available for Payout")]
        OrderNotAvailableLegalHold = 104,
        [Description("Order Not Available (On Hold)")]
        OrderNotAvailableOnHold = 105,
        [Description("Order Paid")]
        OrderPaid = 106,
        [Description("Unable to re-calculate the correspondent commission. Please contact your administrator the correspondent commission. Please contact your administrator")]
        UnableToReCalculateTheCorrespondentCommission = 108,
        [Description("Cannot pay out orders in the same city they are originated.")]
        CannotPayoutOrdersInTheSameCityTheyAreOriginated = 115,
        [Description("Payout is blocked.  Please ask the sender to call the Compliance Department.")]
        PayoutIsBlocked = 116,
        [Description("Cannot pay out orders in the same city they are originated.  Please ask the recipient to call the Compliance Department.")]
        CannotPayoutOrdersInTheSameCityTheyAreOriginated1 = 117,
        [Description("Required Fields are Missing")]
        RequiredFieldsAreMissing = 140,
        [Description("Required Fields Have Invalid Data")]
        RequiredFieldsHaveInvalidData = 141,
        [Description("Non-Required Fields Have Invalid Data")]
        NonRequiredFieldsHaveInvalidData = 142,
        [Description("A Teller Drawer Must Be Open in Order to Payout This Transaction.")]
        ATellerDrawerMustBeOpeninOrdertoPayoutThisTransaction = 150,


    }
}
