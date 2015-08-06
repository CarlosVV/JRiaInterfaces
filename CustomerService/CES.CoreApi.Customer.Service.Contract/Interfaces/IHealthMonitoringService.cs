﻿using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;
using CES.CoreApi.Customer.Service.Contract.Models;

namespace CES.CoreApi.Customer.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.CustomerServiceContractNamespace)]
    public interface IHealthMonitoringService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        Task<ClearCacheResponse> ClearCache();

        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        Task<PingResponse> Ping();
    }
}
