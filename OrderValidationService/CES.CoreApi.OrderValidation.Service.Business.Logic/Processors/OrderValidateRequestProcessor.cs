using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Customer.Service.Contract.Interfaces;
using CES.CoreApi.Customer.Service.Contract.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Processors
{
    public class OrderValidateRequestProcessor : IOrderValidateRequestProcessor
    {
        private readonly IServiceHelper _serviceHelper;

        public OrderValidateRequestProcessor(IServiceHelper serviceHelper)
        {
            if (serviceHelper == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "serviceHelper");

            _serviceHelper = serviceHelper;
        }

        public void ValidateOrder(int customerId)
        {
            var request = new CustomerGetRequest { CustomerId = customerId };
            var customer = _serviceHelper.Execute<ICustomerService, CustomerGetResponse>(p => p.Get(request));

            //validate customer onhold status
            //CustomerStatusValidator

            //Validate Ben is Onhold for correspondent
            //BeneficiaryStatusValidator

            //Validate customer OFAC
            //OfacValidator

            //Validate beneficiary OFAC
            //OfacValidator

            //Validate SAR
            //SarValidator

            //Order duplicate 
            //OrderDuplicateValidator


        }
    }
}