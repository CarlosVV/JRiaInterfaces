using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Compliance.Service.Contract.Constants;


namespace CES.CoreApi.Compliance.Service.Contract.Models
{
	[DataContract(Namespace = Namespaces.ComplianceServiceDataContractNamespace)]
	public class CheckPayoutResponse : BaseResponse
	{


		[DataMember]
		public int Value { get; set; }
		

	}
}
