using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class GetCountryListRequest: BaseRequest
    {
        [DataMember(IsRequired = false)]
        public string Culture { get; set; }

        [DataMember(IsRequired = false)]
        public int LanguageId { get; set; }

        [DataMember(IsRequired = false)]
        public IEnumerable<string> CountryAbbreviationList { get; set; }
    }
}