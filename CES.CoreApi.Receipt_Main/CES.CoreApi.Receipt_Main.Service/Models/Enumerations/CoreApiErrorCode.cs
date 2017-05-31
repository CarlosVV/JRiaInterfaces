using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models.Enumerations
{
    public enum CoreApiErrorCode
    {
        [Description("Available for Payment")]
        AvailableForPayment = 1,

        [Description("Paid Successful")]
        PaidSuccessful = 2,

        [Description("Service not offered")]
        ServiceNotOffered = 100,

        [Description("Service Specifically excluded")]
        ServiceSpecificallyExcluded = 101,

        [Description("Missing App ID and App Object ID")]
        MissingAppIdAndAppObjectId = 102,

        [Description("Missing Agent Payout Location ID")]
        MissingAgentPayoutLocationId = 103,

        [Description("Missing Agent or User ID")]
        MissingAgentOrUserId = 104,

        [Description("Missing Order Number or PIN")]
        MissingOrderNumberOrPin = 105,

        [Description("Missing Agent Payout Local Time")]
        MissingAgentPayoutLocalTime = 106,

        [Description("Payout Local Time is not valid (too far in the past)")]
        PayoutLocalTimeIsNotValidTooFarInThePast = 107,

        [Description("Payout Local Time is not valid (too far in the future)")]
        PayoutLocalTimeIsNotValidTooFarInTheFuture = 108,

        [Description("Payout Country does not Match the Transaction Country")]
        PayoutCountryDoesNotMatchTheTransactionCountry = 109,

        [Description("Agent Country not supplied")]
        AgentCountryNotSupplied = 110,

        [Description("The Payout Location does not payout the selected Currency")]
        ThePayoutLocationDoesNotPayoutTheSelectedCurrency = 111,

        [Description("The Payout Amount is less than the Payout Location minimum")]
        ThePayoutAmountIsLessThanThePayoutLocationMinimum = 112,

        [Description("The Payout Amount is greater than the Payout Location maximum")]
        ThePayoutAmountIsGreaterThanThePayoutLocationMaximum = 113,

        [Description("The Payout Agent does not payout the selected Currency")]
        ThePayoutAgentDoesNotPayoutTheSelectedCurrency = 114,

        [Description("The Payout Amount is less than the Payout Agent minimu")]
        ThePayoutAmountIsLessThanThePayoutAgentMinimu = 115,

        [Description("The Payout Amount is greater than the Payout Agent maximum")]
        ThePayoutAmountIsGreaterThanThePayoutAgentMaximum = 116,

        [Description("Order is not Available for Payout")]
        OrderIsNotAvailableForPayout = 117,

        [Description("User Does not have permission to search for orders to pay not using the Order PIN")]
        UserDoesNotHavePermissionToSearchForOrdersToPayNotUsingTheOrderPin = 118,

        [Description("Unable to find the order. Please verify the number and try again")]
        UnableToFindTheOrder = 119,

        [Description("Order is not Available for Payout")]
        OrderIsNotAvailableForPayout2 = 120,

        [Description("Unable to find the order. Please verify the number and try again")]
        UnableToFindTheOrderPleaseVerifyTheNumberAndTryAgain = 121,

        [Description("Duplicate Orders Found")]
        DuplicateOrdersFound = 122,

        [Description("The order was sent to the same location that created it")]
        TheOrderWasSentToTheSameLocationThatCreatedIt = 123,

        [Description("This is a bank deposit order. You are not authorized to pay this transaction")]
        ThisIsABankDepositOrderYouAreNotAuthorizedToPayThisTransaction = 124,

        [Description("Agent is neither a paying or receiving agent for this order")]
        AgentIsNeitherAPayingOrReceivingAgentForThisOrder = 125,

        [Description("Order Does Not Exist")]
        OrderDoesNotExist = 126,

        [Description("Invalid Order ID Passed In")]
        InvalidOrderIdPassedIn = 127,

        [Description("Order Voided")]
        OrderVoided = 128,

        [Description("Order Canceled")]
        OrderCanceled = 129,

        [Description("Order Not Available")]
        OrderNotAvailable = 130,

        [Description("Order Not Available for Payout")]
        OrderNotAvailableForPayout = 131,

        [Description("Order Not Available (On Hold)")]
        OrderNotAvailableOnHold = 132,

        [Description("Order Paid")]
        OrderAlreadyPaid = 133,

        [Description("Unable to re-calculate the correspondent commission. Please contact your administrator the correspondent commission. Please contact your administrator")]
        UnableToReCalculateTheCorrespondentCommission = 134,

        [Description("Cannot pay out orders in the same city they are originated")]
        CannotPayOutOrdersInTheSameCityTheyAreOriginated = 135,

        [Description("Payout is blocked.  Please ask the sender to call the Compliance Department")]
        PayoutIsBlocked = 136,

        [Description("Cannot pay out orders in the same city they are originated.  Please ask the recipient to call the Compliance Department")]
        CannotPayOutOrdersInTheSameCityTheyAreOriginated2 = 137,

        [Description("Required Fields are Missing")]
        RequiredFieldsAreMissing = 138,

        [Description("Required Fields Have Invalid Data")]
        RequiredFieldsHaveInvalidData = 139,

        [Description("Non-Required Fields Have Invalid Data")]
        NonRequiredFieldsHaveInvalidData = 140,

        [Description("A Teller Drawer Must Be Open in Order to Payout This Transaction")]
        ATellerDrawerMustBeOpenInOrderToPayoutThisTransaction = 141,

        [Description("Order Cannot be Paid at this Agent/Location")]
        OrderCannotBePaidAtThisAgentLocation = 142,

        [Description("Order can not be paid")]
        OrderCanNotBePaid = 143,

        [Description("Order can not be paid")]
        OrderCanNotBePaid2 = 144,

        [Description("Order can not be paid")]
        OrderCanNotBePaid3 = 145,

        [Description("Order exists but is not currently available for payout")]
        OrderExistsButIsNotCurrentlyAvailableForPayout = 146,

        [Description("Order can not be paid")]
        OrderCanNotBePaid4 = 147,

        [Description("Order has been marked as paid to beneficiary")]
        OrderHasBeenMarkedAsPaidToBeneficiary = 148,

        [Description("Order can not be paid")]
        OrderCanNotBePaid5 = 149,

        [Description("Order is not available. Please contact sender")]
        OrderIsNotAvailable = 150,

        [Description("Confirm Payout Call Failed")]
        ConfirmPayoutCallFailed = 151,

        [Description("Could not create Provider Instance")]
        CouldNotCreateProviderInstance = 152,

        [Description("Error Country Code must be 2 characters long")]
        ErrorCountryCodeLength2 = 153,

        [Description("Error Country Code must be 3 characters long")]
        ErrorCountryCodeLength3 = 154,

        [Description("Error Country Code can only contain letters")]
        ErrorCountryCodeLetters = 155,

        [Description("Error Database Access")]
        ErrorDatabaseAccess = 156,

        [Description("Invalid Country Code")]
        ErrorInvalidCountryCode = 157,

        [Description("Money amount is not valid")]
        ErrorMoneyAmountInvalid = 158,

        [Description("Money has wrong # of decimal places.  Expected 2, but found")]
        ErrorMoneyBadPrecision = 159,

        [Description("Invalid Currency Code")]
        ErrorMoneyCurrencyCode = 160,

        [Description("Currency must be exactly 3 characters long")]
        ErrorMoneyCurrencyLength = 161,

        [Description("Failed to Create Money value")]
        ErrorMoneyFailedToCreate = 162,

        [Description("Cannot perform operations on Mis-Matched Currencies")]
        ErrorMoneyMismatchedCurrency = 163,

        [Description("Money Amount must be a postive number")]
        ErrorMoneyNegativeAmount = 164,

        [Description("Money Currency cannot be NULL")]
        ErrorMoneyNullCurrency = 165,

        [Description("GoldenCrown did not return exactly 1 order for the PIN")]
        GCNotExactlyOneOrderForPin = 166,

        [Description("GoldenCrown Response is null")]
        GCResponseNull = 167,

        [Description("Golden Crown Error Service")]
        GCServiceError = 168,

        [Description("GoldenCrown did not return any orders for the PIN")]
        GCZeroOrdersForPIN = 169,

        [Description("Get Transaction Info Call Failed")]
        GetTransactionInfoFailed = 170,

        [Description("Invalid ID Type")]
        InvalidIDType = 171,

        [Description("Invalid Order PIN and Order ID")]
        InvalidOrderPIN = 172,

        [Description("Invalid Request Message.  Missing required elements")]
        InvalidRequestMessage = 173,

        [Description("Missing Beneficiary Name")]
        MissingBeneficiaryName = 174,

        [Description("Transaction has no payout amount recorded")]
        MissingPayoutAmount = 175,

        [Description("No Provider found for this transaction")]
        NoProviderFound = 176,

        [Description("No Provider Interface found")]
        NoProviderInterfaceFound = 177,

        [Description("Payout Transaction Call Failed")]
        PayoutTransactionFailed = 178,

        [Description("Payout Transaction Call in Repository Failed")]
        PayoutTransactionRepositoryFailed = 179,

        [Description("The Order PIN entered does not match any Partner Orders")]
        PINDidNotMatchPartner = 180,

        [Description("Insufficient privileges")]
        InsufficientPrivileges = 181,

        [Description("Location is not registered in the system")]
        LocationIsNotRegisteredInTheSystem = 182,

        [Description("Operator is not registered in the system")]
        OperatorIsNotRegisteredInTheSystem = 183,

        [Description("Operator is locked")]
        OperatorIsLocked = 184,

        [Description("Large volume data set to reflect. Change selection criteria")]
        LargeVolumeDataSetToReflectChangeSelectionCriteria = 185,

        [Description("Operation does not correspond to tarriff limit")]
        OperationDoesNotCorrespondToTarriffLimit = 186,

        [Description("City %1 lacks payout locations")]
        CityLacksPayoutLocations = 187,

        [Description("Unknown country")]
        UnknownCountry = 188,

        [Description("Agent does not provide service")]
        AgentDoesNotProvideService = 189,

        [Description("Daily payment limit exceeded")]
        DailyPaymentLimitExceeded = 190,

        [Description("Insufficient funds in Originating Bank's account for payout")]
        InsufficientFundsInOriginatingBankAccountForPayout = 191,

        [Description("Sender's location is not registered in the system")]
        SenderLocationIsNotRegisteredInTheSystem = 192,

        [Description("Unknown transfer")]
        UnknownTransfer = 193,

        [Description("Invalid transfer transaction")]
        InvalidTransferTransaction = 194,

        [Description("Transfer cannot be paid out prior to the due da")]
        TransferCannotBePaidOutPriorToTheDueDa = 195,

        [Description("Transfer expired")]
        TransferExpired = 196,

        [Description("Transfer is being processed by another operator, transfer is not available")]
        TransferIsBeingProcessedByAnotherOperator, TransferIsNotAvailable = 197,

        [Description("Transfer payout is impossible, advise another location to a client")]
        TransferPayoutIsImpossible, AdviseAnotherLocationToAClient = 198,

        [Description("Unauthorised access")]
        UnauthorisedAccess = 199,

        [Description("Impossible to send a transfer. Insufficient funds in the account")]
        ImpossibleToSendATransferInsufficientFundsInTheAccount = 200,

        [Description("Transfer has not passed internal checks, please try again later")]
        TransferHasNotPassedInternalChecks, PleaseTryAgainLater = 201,

        [Description("Unknown direction")]
        UnknownDirection = 202,

        [Description("Amount of sender's transactions per month exceeds money transfer system limit. Total limit renewal will be 30 days past the date of the latest transaction. We apologize for the troubles caused")]
        AmountOfSenderTransactionsPerMonthExceedsMoneyTransferSystemLimit = 203,

        [Description("Amount of receiver's transactions per month exceeds money transfer system limit. Total limit renewal will be 30 days past the date of the latest transaction. We apologize for the troubles caused")]
        AmountOfReceiverTransactionsPerMonthExceedsMoneyTransferSystemLimit = 204,

        [Description("Maximum transfer amount equal to %amount% %cur% has been exceeded")]
        MaximumTransferAmountEqualToHasBeenExceeded = 205,

        [Description("Limit of transfers from resident has been exceeded. Maximum amount equals to %amount% %cur%")]
        LimitOfTransfersFromResidentHasBeenExceeded = 206,

        [Description("Exchange rate has been changed. Recalculate fee")]
        ExchangeRateHasBeenChangedRecalculateFee = 207,

        [Description("Current time is not included into location's operating hours")]
        CurrentTimeIsNotIncludedIntoLocationOperatingHours = 208,

        [Description("Transferred IP does not correspond to the agent's access settings")]
        TransferredIpDoesNotCorrespondToTheAgentAccessSettings = 209,

        [Description("Payout Local Time is not valid (too far in the past)")]
        PayoutLocalTimeIsNotValidTooFarInThePast2 = 21,

        [Description("Location's daily transfer limit has been exceeded")]
        LocationDailyTransferLimitHasBeenExceeded = 210,

        [Description("Amount of sender's large-sum transactions for 90 days period exceeds money transfer system limit")]
        AmountOfSenderLargeSumTransactionsFor90DaysPeriodExceedsMoneyTransferSystemLimit = 211,

        [Description("Amount of receiver's large-sum transactions for 90 days period exceeds money transfer system limit")]
        AmountOfReceiverLargeSumTransactionsFor90DaysPeriodExceedsMoneyTransferSystemLimit = 212,

        [Description("Conflicting data on client's residence")]
        ConflictingDataOnClientResidence = 213,

        [Description("Residence not established")]
        ResidenceNotEstablished = 214,

        [Description("Specified person is not indicated in the profile")]
        SpecifiedPersonIsNotIndicatedInTheProfile = 215,

        [Description("Wrong mobile phone format. Acceptable format +79requestModel.xxxx")]
        WrongMobilePhoneFormatAcceptableFormat = 216,

        [Description("Invalid receiver's mobile phone number")]
        InvalidReceiverMobilePhoneNumber = 217,

        [Description("External system not connected")]
        ExternalSystemNotConnected = 218,

        [Description("External system request error. %1")]
        ExternalSystemRequestError = 219,

        [Description("Transfer is under validation, please try again later")]
        TransferIsUnderValidationPleaseTryAgainLater = 220,

        [Description("Persistence number does not exist")]
        PersistenceNumberNotExist = 221,

        [Description("PersistenceID has expired")]
        PersistenceIDExpired = 222,

        [Description("Event does not exist")]
        EventNotExist = 223,

        [Description("Invalid ProviderID")]
        InvalidProviderID = 224,

        [Description("Persistence Object not found")]
        PersistenceObjectNotFound = 225,

        [Description("Invalid PersistenceID. Please get transaction info again")]
        InvalidPersistenceID = 226,

        [Description("Persistence ReturnInfo Object not found")]
        PersistenceReturnInfoObjectNotFound = 227,

        [Description("Order not available")]
        OrderNotAvailable2 = 228,

        [Description("Unreviewed issues")]
        UnreviewedIssues = 229,

        [Description("Missing required field")]
        MissingRequiredField = 230,

        [Description("Invalid persistence data")]
        InvalidPersistenceData = 231,

        [Description("Name NO Match")]
        NameNoMatch = 232,

        [Description("An error occurred while processing your request")]
        InternalError = 500,

        [Description("Not mapped code")]
        NotMappedCode = 998,


    }
}