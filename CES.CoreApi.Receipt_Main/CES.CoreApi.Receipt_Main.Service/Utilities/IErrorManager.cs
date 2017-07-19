using System.Collections.Generic;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Service.Filters.Responses;
using FluentValidation.Results;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public interface IErrorManager
    {
        List<Error> ValidateHeaders(IClient client);
        List<Error> GetErrors(ValidationResult validationResult);
        List<Error> GetErrors(ReturnInfo returnInfo);
        List<Error> GetErrors(ErrorResponse errorResponse);
    }
}