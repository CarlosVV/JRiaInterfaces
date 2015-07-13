using System.ComponentModel;
using CES.CoreApi.Foundation.Contract.Constants;

namespace CES.CoreApi.Foundation.Contract.Enumerations
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
