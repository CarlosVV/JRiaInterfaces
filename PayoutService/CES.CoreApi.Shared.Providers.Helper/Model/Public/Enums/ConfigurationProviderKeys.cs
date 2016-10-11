using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Providers.Helper.Model.Public.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConfigurationProviderKeys
    {
        //Common
        PinRegExp,
        InterfacePayout,
        RequiredFields,


        //Golden Crown
        GoldenCrownServerURL,
        GoldenCrownClientCertSubject,
        GoldenCrownServiceCertSubject,
        GoldenCrownInterfaceVersion,
        GoldenCrownRiaAgentID,


    }
}
