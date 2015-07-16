namespace CES.CoreApi.Shared.Business.Contract.Enumerations
{
    // select s.fkey2, s.fName from systblconst2 s (nolock) where s.fkey1 = 7112  and s.fDisabled = 0
    public enum DeliveryMethodType 
    {
        Undefined = 0,
        OfficePickup = 1,
        BankDeposit = 2,
        HomeDelivery = 3,
        MoneyOrder = 4,
        GiftCard = 5,
        RuralPickUp = 6,
        BillPayment = 10,
        BillPayment2 = 11,
        BillPaymentXPress = 12,
        PhoneCard = 20,
        DebitCard = 30,
        BankTransfer = 31,
        AtmPickup = 32,
        MobilePayment = 34
    }
}