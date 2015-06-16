namespace CES.CoreApi.Settings.Service.Business.Contract.Enumerations
{
    //select * from systblConst2 where fKey1 = 7310
    public enum PossibleDuplicateCustomerActionSettingType
    {
        Reject = 1,
        RejectWithoutInformation = 2,
        OnHold = 10,
        OnHoldWithoutInformation = 11,
        LogOnly = 20
    }
}
