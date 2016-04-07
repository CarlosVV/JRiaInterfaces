using System.ComponentModel;

using CES.CoreApi.Data.Constants;

namespace CES.CoreApi.Data.Enumerations
{
    public enum ServiceConfigurationType
    {
        [Description("")]
        Undefined,
        [Description(ServiceConfigurationItems.ServiceConfiguration)]
        ServiceEndpoints,
        [Description(ServiceConfigurationItems.ClientConfiguration)]
        ClientEndpoints
    }
}
