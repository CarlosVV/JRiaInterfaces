using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Validation;
using CES.CoreApi.Compliance.Service.Contract.Models;
using CES.CoreApi.Compliance.Service.Contract.Interfaces;

namespace CES.CoreApi.Compliance.Service.Utilites
{
    public class RequestValidator : IRequestValidator
    {
        #region Public methods
        public void Validate(CheckOrderRequest request)
        {
            ContractValidation.Requires(request != null
                , TechnicalSubSystem.ComplianceService
                , SubSystemError.GeneralRequiredParameterIsUndefined, "request");

            //add validations required
            ValidateOrderNumber(request.OrderNumber);

        }

        public void Validate(CheckPayoutRequest request)
        {
            ContractValidation.Requires(request != null
                , TechnicalSubSystem.ComplianceService
                , SubSystemError.GeneralRequiredParameterIsUndefined, "request");

            //add validations required
           

        }
        #endregion

        #region Private methods

        private static void ValidateOrderNumber(string orderNumber)
        {
            ContractValidation.Requires(!string.IsNullOrEmpty(orderNumber), TechnicalSubSystem.ComplianceService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.OrderNumber");
           
        }
        #endregion
    }
}
  