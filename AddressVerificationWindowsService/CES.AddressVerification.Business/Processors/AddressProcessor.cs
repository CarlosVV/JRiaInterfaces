using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CES.AddressVerification.Business.Contract.Interfaces;
using CES.AddressVerification.Business.Contract.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Models;

namespace CES.AddressVerification.Business.Processors
{
    public class AddressProcessor : IAddressProcessor
    {
        private readonly IAddressRepository _repository;
        private readonly IServiceHelper _serviceHelper;

        public AddressProcessor(IAddressRepository repository, IServiceHelper serviceHelper)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");

            _repository = repository;
            _serviceHelper = serviceHelper;
        }

        public async Task Start()
        {
            //Get next record batch
            var recordList = await _repository.GetAddressList();

            if (!recordList.Any())
                return;

            //Process record batch
            ProcessBatch(recordList);
        }

        private void ProcessBatch(IEnumerable<Address> recordList)
        {
            //Validate addresses
            var resultList = from address in recordList
                             select ProcessAddress(address);
            
            var 
        }

        private ValidationResult ProcessAddress(Address address)
        {
            //Validate address
            var validateAddressResponse = ValidateAddress(address);

            //Process validation response
            return ProcessAddressValidationResponse(address, validateAddressResponse);
        }

        private ValidateAddressResponse ValidateAddress(Address address)
        {
            var validateAddressRequest = new ValidateAddressRequest
            {
                MinimumConfidence = Confidence.Medium,
                Address = new AddressRequest
                {
                    Address1 = address.Address1,
                    AdministrativeArea = address.State,
                    City = address.City,
                    Country = address.Country
                }
            };
            return _serviceHelper.Execute<IAddressService, ValidateAddressResponse>(p => p.ValidateAddress(validateAddressRequest));
        }

        private ValidationResult ProcessAddressValidationResponse(Address address, ValidateAddressResponse validateAddressResponse)
        {
            throw new NotImplementedException();
        }

        private DataTable ConvertValidationResult(IEnumerable<ValidationResult> validationResults)
        {
            
        }
    }
}
