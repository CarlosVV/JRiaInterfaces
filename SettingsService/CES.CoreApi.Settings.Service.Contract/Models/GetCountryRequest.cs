using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class GetCountryRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public string CountryAbbreviation { get; set; }

        [DataMember(IsRequired = false)]
        public int LanguageId { get; set; }

    }
}