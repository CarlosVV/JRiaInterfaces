using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IServiceConfigurationProvider
    {
        ServiceConfiguration GetConfiguration(ServiceConfigurationType configurationType = ServiceConfigurationType.ServiceEndpoints);
    }
}