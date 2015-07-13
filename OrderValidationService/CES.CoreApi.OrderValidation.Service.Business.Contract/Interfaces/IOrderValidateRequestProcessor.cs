namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces
{
    public interface IOrderValidateRequestProcessor
    {
        void ValidateOrder(int customerId);
    }
}
