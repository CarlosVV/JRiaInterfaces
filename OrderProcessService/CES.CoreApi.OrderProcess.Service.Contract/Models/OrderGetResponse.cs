using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
    public class OrderGetResponse: BaseResponse
    {
        public OrderGetResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }
    }
}