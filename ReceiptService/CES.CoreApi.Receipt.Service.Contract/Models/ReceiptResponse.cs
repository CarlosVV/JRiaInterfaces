using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Receipt.Service.Contract.Constants;

namespace CES.CoreApi.Receipt.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ReceiptServiceDataContractNamespace)]
    public class ReceiptResponse: BaseResponse
    {
        public ReceiptResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public string ReceiptName { get; set; }

        [DataMember]
        public byte[] ReceiptBody { get; set; }
    }
}
