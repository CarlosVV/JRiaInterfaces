using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;
using CES.CoreApi.Receipt_Main.Service.Models.Enumerations;
using CES.CoreApi.Receipt_Main.Service.Utilities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Validators
{
    public class ServiceTaxCreateDocumentRequestViewModelValidator : AbstractValidator<ServiceTaxCreateDocumentRequestViewModel>
    {
        public ServiceTaxCreateDocumentRequestViewModelValidator()
        {
            RuleFor(r => r.Document).NotEmpty().WithMessage("Document is required").WithErrorCode(ValidationCode.IsRequired.ToNumberString());
            RuleFor(r => r.Document).SetValidator(new TaxDocumentValidator());            
        }
    }

    public class TaxDocumentValidator :  AbstractValidator<TaxDocument>
    {
        public TaxDocumentValidator()
        {
            RuleFor(taxDocument => taxDocument.fStoreName).NotEmpty();
            RuleFor(taxDocument => taxDocument.DocumentDetails).SetCollectionValidator(new TaxDocumentDetailValidator());
            RuleFor(taxDocument => taxDocument.EntityFrom).SetValidator(new TaxEntityValidator());
            RuleFor(taxDocument => taxDocument.EntityTo).SetValidator(new TaxEntityValidator());            
        }
    }

    public class TaxDocumentDetailValidator : AbstractValidator<TaxDocumentDetail>
    {
        public TaxDocumentDetailValidator()
        {
            RuleFor(taxDocument => taxDocument.fAmount).NotEmpty();
        }
    }

    public class TaxEntityValidator : AbstractValidator<TaxEntity>
    {
        public TaxEntityValidator()
        {
            RuleFor(taxEntity => taxEntity.fFirstName).NotEmpty();
            RuleFor(taxEntity => taxEntity.fMiddleName).NotEmpty();
            RuleFor(taxEntity => taxEntity.fLastName1).NotEmpty();
            RuleFor(taxEntity => taxEntity.fLastName2).NotEmpty();
            RuleFor(taxEntity => taxEntity.fFullName).NotEmpty();
        }
    }
}