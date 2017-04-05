using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Models.Enumerations;
using CES.CoreApi.Receipt_Main.Utilities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.ViewModels
{
    public class ServiceTaxGenerateRequest : BaseRequestModel
    {
        public string OrderNo { get; set; }

        public int? OrderId { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Amount { get; set; }

        public decimal? TaxPercentaje { get; set; }
        
    }

    public class ServiceTaxGenerateRequestValidator : AbstractValidator<ServiceTaxGenerateRequest>
    {
       public ServiceTaxGenerateRequestValidator()
        {
            RuleFor(r => r.OrderNo).NotEmpty().WithMessage("OrderNo is required").WithErrorCode(ValidationCode.IsRequired.ToNumberString());
        }
    }
}