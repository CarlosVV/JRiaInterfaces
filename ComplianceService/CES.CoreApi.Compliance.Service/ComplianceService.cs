using System;
using System.ServiceModel;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Business.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;
using CES.CoreApi.Compliance.Service.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Contract.Models;
using CES.CoreApi.Compliance.Service.Contract.Constants;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Enumerations;

namespace CES.CoreApi.Compliance.Service
{
    [ServiceBehavior(Namespace = Namespaces.ComplianceServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ComplianceService: ICheckingService 
    {
        #region Core
        private readonly IRequestValidator _validator;
        private readonly ICheckPayoutRequestProcessor _checkingServiceRequestProcessor;
        private readonly IMappingHelper _mapper;

        public ComplianceService(IRequestValidator validator, ICheckPayoutRequestProcessor checkingServiceRequestProcessor, IMappingHelper mapper)
        {
            if (validator == null) throw new ArgumentNullException("validator");
            if (checkingServiceRequestProcessor == null) throw new ArgumentNullException("_checkingServiceRequestProcessor");
            if (mapper == null) throw new ArgumentNullException("mapper");

            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.ComplianceService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (validator == null)
                throw new CoreApiException(TechnicalSubSystem.ComplianceService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "validator");

            _validator = validator;
            _checkingServiceRequestProcessor = checkingServiceRequestProcessor;
            _mapper = mapper;
        }

        #endregion

        #region ICheckingService
        public CheckOrderResponse CheckOrder(CheckOrderRequest request)
        {
            throw new NotImplementedException();
          }

        public CheckPayoutResponse CheckPayout(CheckPayoutRequest request)
        {
            _validator.Validate(request);

            var requestModel = _mapper.ConvertTo<CheckPayoutRequest, CheckPayoutRequestModel>(request);
            var responseModel = _checkingServiceRequestProcessor.CheckPayout(requestModel);

            return _mapper.ConvertToResponse<CheckPayoutResponseModel, CheckPayoutResponse>(responseModel);

            //_validator.Validate(request);

            //var responseModel = _addressServiceRequestProcessor.GetAutocompleteList(
            //    _mapper.ConvertTo<AddressRequest, AutocompleteAddressModel>(request.Address),
            //    request.MaxRecords,
            //    _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            //return _mapper.ConvertToResponse<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(responseModel);

        }

        #endregion

    }
}
