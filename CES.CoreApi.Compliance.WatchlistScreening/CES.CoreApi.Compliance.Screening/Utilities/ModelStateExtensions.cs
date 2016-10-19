using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace CES.CoreApi.Compliance.Screening.Utilities
{
    public static class ModelStateExtensions
    {
        public static void AddModelErrors(this ModelStateDictionary modelState, ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}