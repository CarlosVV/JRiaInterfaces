using System;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Receipt.Service.Business.Contract.Interfaces;
using CES.CoreApi.Receipt.Service.Business.Contract.Models;
using CES.CoreApi.Receipt.Service.Contract.Interfaces;
using CES.CoreApi.Receipt.Service.Contract.Models;
using CES.CoreApi.Receipt.Service.Contract.Interfaces;

namespace CES.CoreApi.Receipt.Service
{
    public class ReceiptService: IReceiptService
    {
        private readonly IRequestValidator _validator;
        private readonly IReceiptProcessor _processor;
        private readonly IMappingHelper _mappingHelper;

        public ReceiptService(IRequestValidator validator, IReceiptProcessor processor, IMappingHelper mappingHelper)
        {
            if (validator == null) throw new ArgumentNullException("validator");
            if (processor == null) throw new ArgumentNullException("processor");
            if (mappingHelper == null) throw new ArgumentNullException("mappingHelper");

            _validator = validator;
            _processor = processor;
            _mappingHelper = mappingHelper;
        }

        public ReceiptResponse Test(ReceiptRequest request)
        {
            _validator.Validate(request);

            var responseModel = _processor.GenerateReceipt(request.Id);

            return _mappingHelper.ConvertToResponse<ReceiptModel, ReceiptResponse>(responseModel);

            //_validator.Validate(request);

            //var responseModel = _addressServiceRequestProcessor.GetAutocompleteList(
            //    _mapper.ConvertTo<AddressRequest, AutocompleteAddressModel>(request.Address),
            //    request.MaxRecords,
            //    _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            //return _mapper.ConvertToResponse<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(responseModel);
        }
    }
}
