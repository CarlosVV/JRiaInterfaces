using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Customer.Service.Contract.Interfaces;
using CES.CoreApi.Customer.Service.Contract.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using CES.CoreApi.OrderValidation.Service.Business.Logic.Validators;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Processors
{
    public class TransactionValidateRequestProcessor : ITransactionValidateRequestProcessor
    {
        #region Core

        private readonly IServiceHelper _serviceHelper;
        private readonly IValidatorFactory _validatorFactory;
        
        public TransactionValidateRequestProcessor(IServiceHelper serviceHelper, IValidatorFactory validatorFactory)
        {
            if (serviceHelper == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "serviceHelper");

            _serviceHelper = serviceHelper;
            _validatorFactory = validatorFactory;
        } 

        #endregion

        public async Task Validate(TransactionValidateRequestModel validateRequest)
        {
            //Get customer details
            var request = new CustomerGetRequest { CustomerId = validateRequest.CustomerId };
            var customer = await _serviceHelper.ExecuteAsync<ICustomerService, Task<CustomerGetResponse>>(p => p.Get(request));

            //Get paying agent details



            var paval = _validatorFactory.GetInstance<PayingAgentValidator>();
            
            var m1 = new PayingAgentValidationModel {PayingAgentId = 10};
            var r1 = paval.Validate(m1);

            var m2 = new PayingAgentValidationModel()
            {
                IsLocationDisabled = true,
                IsLocationOnHold = true
            };
            var r2 = paval.Validate(m2);


            //foreach (var name in _validatorFactory.RegisteredValidators())
            //{
            //    var val = _validatorFactory.GetInstance(name);
            //    var r3 = val.Validate(m1);
            //}

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

            //CustomerIdentificationCardValidator
            //CustomerNameAndAddressValidator
            //CustomerAmountValidator
            //BeneficiaryNameAndCountryValidator
        }

        //public Task<string> CallService(string Parameter)
        //{
        //    ProxyClient client = new ProxyClient(new BasicHttpBiding(), new EndpointAddress("http://host/service"));
        //    return Task<string>.Factory.FromAsync(
        //            ((Proxy)client.InnerChannel).BeginCall,
        //            ((Proxy)client.InnerChannel).EndCall,
        //            Parameter, null);
        //}

        private void ValidateCustomer(CustomerGetResponse customer)
        {
            //var validator = new CustomerValidator();
        }
    }
}