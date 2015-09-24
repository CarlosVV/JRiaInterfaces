namespace CES.CoreApi.LimitVerification.Service.Business.Contract.Enumerations
{
    public enum Payability
    {
        Approved = 0,
        CurrencyNotPaid = 1,
        BelowTransactionMinimum = 2,
        AboveTransactionMaximum = 3,
        AboveDailyMaximum = 4
    }
}
