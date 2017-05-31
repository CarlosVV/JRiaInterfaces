using CES.CoreApi.Receipt_Main.Service.Models.Enumerations;
using CES.CoreApi.Receipt_Main.Service.Utilities;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Validators
{
    public class ServiceTaxCreateCAFRequestViewModelValidator : AbstractValidator<ServiceTaxCreateCAFRequestViewModel>
    {
        public ServiceTaxCreateCAFRequestViewModelValidator()
        {
            RuleFor(r => r.CAFContent).NotEmpty().WithMessage("CAFContent is required").WithErrorCode(ValidationCode.IsRequired.ToNumberString());
        }
    }
}