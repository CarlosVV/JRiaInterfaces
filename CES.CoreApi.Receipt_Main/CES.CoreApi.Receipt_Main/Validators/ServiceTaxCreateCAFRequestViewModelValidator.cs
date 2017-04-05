using CES.CoreApi.Receipt_Main.Models.Enumerations;
using CES.CoreApi.Receipt_Main.Utilities;
using CES.CoreApi.Receipt_Main.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Validators
{
    public class ServiceTaxCreateCAFRequestViewModelValidator : AbstractValidator<ServiceTaxCreateCAFRequestViewModel>
    {
        public ServiceTaxCreateCAFRequestViewModelValidator()
        {
            RuleFor(r => r.CAFContent).NotEmpty().WithMessage("CAFContent is required").WithErrorCode(ValidationCode.IsRequired.ToNumberString());
        }
    }
}