using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.OrderValidation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderValidationDataContractNamespace)]
    public class OrderValidateResponse : BaseResponse
    {
        public OrderValidateResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }
    }
}