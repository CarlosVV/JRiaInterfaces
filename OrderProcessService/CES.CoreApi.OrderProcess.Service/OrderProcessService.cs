using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.OrderProcess.Service.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service
{
    [ServiceBehavior(Namespace = Namespaces.OrderProcessServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class OrderProcessService : IOrderProcessService
    {
        #region Core

        public OrderProcessService()
        {
            
        }

        #endregion

        #region IOrderProcessService implementation

        public OrderCreateResponse CreateOrder(OrderCreateRequest request)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}