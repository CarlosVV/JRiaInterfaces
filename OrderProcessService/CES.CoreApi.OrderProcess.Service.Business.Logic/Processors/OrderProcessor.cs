using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Business.Logic.Processors
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IOrderRepository _orderRepository;

        #region Core

        public OrderProcessor(IOrderRepository orderRepository)
        {
            if (orderRepository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderProcessService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "orderRepository");
            _orderRepository = orderRepository;
        }

        #endregion

        #region IOrderProcessor implementation
        
        public OrderModel GetOrder(int orderId, bool checkMainDatabase, int databaseId)
        {
            return _orderRepository.GetOrder(orderId);
        } 

        #endregion
    }
}
