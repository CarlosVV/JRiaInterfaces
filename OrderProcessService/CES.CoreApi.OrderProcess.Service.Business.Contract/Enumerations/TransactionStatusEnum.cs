namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Enumerations
{
    public enum TransactionStatusEnum
    {
        Unknown = 0,
		Pending = 1,
		Processing = 3,
		Posted = 4,
		Canceled = 5,
		Voided = 6,
		Released = 7
    }
}
