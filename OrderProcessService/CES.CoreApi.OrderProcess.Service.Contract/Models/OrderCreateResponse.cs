using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderProcessDataContractNamespace)]
    public class OrderCreateResponse : BaseResponse
    {
        public OrderCreateResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }
    }
}