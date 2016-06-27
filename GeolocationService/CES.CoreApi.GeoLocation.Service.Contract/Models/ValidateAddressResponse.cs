using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class ValidateAddressResponse : BaseResponse
    {
        

        /// <summary>
        /// Address validation status.
        /// Returns true if address is found with requested MinimumConfidence level,
        /// otherwise false
        /// </summary>
        [DataMember]
        public bool IsValid { get; set; }

        /// <summary>
        /// Address geo location coordinates
        /// </summary>
        [DataMember]
        public Location Location { get; set; }

        /// <summary>
        /// Address which was really validated by data provider
        /// </summary>
        [DataMember(Name = "Address")]
        public ValidatedAddress Address { get; set; }

        /// <summary>
        /// A string specifying the confidence of the result.
        /// </summary>
        [DataMember]
        public Confidence Confidence { get; set; }

		//[DataMember]
		//public string ConfidenceText { get { return Confidence.ToString(); } }

		/// <summary>
		/// Specify data provider used to verify address
		/// </summary>
		[DataMember]
        public DataProvider DataProvider { get; set; }

		//[DataMember]
		//public string DataProviderText { get { return DataProvider.ToString(); } }
	}
}