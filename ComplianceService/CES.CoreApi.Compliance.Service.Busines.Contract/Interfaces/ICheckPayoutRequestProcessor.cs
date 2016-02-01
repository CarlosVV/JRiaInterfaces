using CES.CoreApi.Compliance.Service.Business.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Business.Contract.Interfaces
{
    public interface ICheckPayoutRequestProcessor
    {
       
        OrderModel GetOrderByNumber(string orderNumber);
        CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request);
    }
}