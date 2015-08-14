namespace CES.CoreApi.Shared.Business.Contract.Enumerations
{
    public enum ReceivingAgentStatus
    {
        None = 0,
        Active = 1,
        Closed = 2,
        Suspended = 3,
        Pending = 10,
        Denied = 11,
        Collections = 15,
        NotReviewed = 20,
        ApprovedCredit = 30,
        DeniedSales = 32,
        DeniedCreditWc = 34,
        DeniedNewAccounts = 36,
        PendingInformation = 37,
        WorkingCredit = 38,
        WorkingSales = 39,
        WorkingNewAccounts = 40,
        SuspendedAr = 41,
        SuspendedCompliance = 42,
        SuspendedFraud = 43,
        InRiaSystem = 99
    }
}
