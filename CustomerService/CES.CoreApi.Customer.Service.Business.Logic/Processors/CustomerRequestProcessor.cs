﻿using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Customer.Service.Business.Contract.Interfaces;
using CES.CoreApi.Customer.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Customer.Service.Business.Logic.Processors
{
    public class CustomerRequestProcessor : ICustomerRequestProcessor
    {
        #region Core

        private readonly ICustomerRepository _customerRepository;

        public CustomerRequestProcessor(ICustomerRepository customerRepository)
        {
            if (customerRepository == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "customerRepository");
            _customerRepository = customerRepository;
        }

        #endregion

        #region Public methods

        public CustomerModel GetCustomer(int customerId)
        {
            return _customerRepository.GetCustomer(customerId);
        }

        public ProcessSignatureModel ProcessSignature(int orderId, byte[] signature)
        {
            //put logic here and use _imageRepository to access database

            return new ProcessSignatureModel {IsCompleted = true};
        }

        #endregion
    }
}