using System.Runtime.Serialization;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class GetCountrySettingsRequest
    {
        [DataMember(IsRequired = true)]
        public string CountryAbbreviation { get; set; }
    }
}