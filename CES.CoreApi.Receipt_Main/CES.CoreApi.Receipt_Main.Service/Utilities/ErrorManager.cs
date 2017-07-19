using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
using CES.CoreApi.Receipt_Main.Service.Filters.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public class ErrorManager : IErrorManager
    {
        public List<Error> ValidateHeaders(IClient client)
        {
            List<Error> errors = new List<Error>();

            if (AppSettings.IsStandAloneApplication) return errors;

            if (string.IsNullOrWhiteSpace(client.ApplicationId))
            {
                errors.Add(new Error { Message = "Header Error: ApplicationId not sent", Property = "400" });
            }

            if (string.IsNullOrWhiteSpace(client.CesAppObjectId))
            {
                errors.Add(new Error { Message = "Header Error: ces-appObjectId not sent", Property = "400" });
            }

            if (string.IsNullOrWhiteSpace(client.CesUserId))
            {
                errors.Add(new Error { Message = "Header Error: ces-userId not sent", Property = "400" });
            }

            if (string.IsNullOrWhiteSpace(client.CesRequestTime))
            {
                errors.Add(new Error { Message = "Header Error: ces-requestTime not sent", Property = "400" });
            }

            return errors;
        }
        public List<Error> GetErrors(ValidationResult validationResult)
        {
            List<Error> modelStateErrors = new List<Error>();
            if (validationResult != null)
            {

                if (validationResult.Errors != null)
                {
                    var code = 0;
                    foreach (var item in validationResult.Errors)
                    {
                        int.TryParse(item.ErrorCode, out code);
                        modelStateErrors.Add(new Error
                        {
                            Code = code,
                            Property = item.PropertyName,
                            Message = item.ErrorMessage,
                        });
                    }
                }

            }

            return modelStateErrors;
        }

        public List<Error> GetErrors(ReturnInfo returnInfo)
        {

            List<Error> modelStateErrors = new List<Error>();
            if (returnInfo != null)
            {

                if (returnInfo.Errors != null && returnInfo.Errors.Any())
                {
                    foreach (var error in returnInfo.Errors)
                    {

                        modelStateErrors.Add(new Error
                        {
                            Code = error.Code,
                            Property = error.Property,
                            Message = error.Message
                        });
                    }

                    return modelStateErrors;
                }

                modelStateErrors.Add(new Error
                {
                    Code = returnInfo.ErrorCode,
                    Property = string.Empty,
                    Message = returnInfo.ErrorMessage
                });
            }

            return modelStateErrors;
        }

        public List<Error> GetErrors(ErrorResponse errorResponse)
        {
            List<Error> modelStateErrors = new List<Error>();
            if (errorResponse != null)
            {

                if (errorResponse.Errors != null)
                {
                    foreach (var item in errorResponse.Errors)
                    {
                        modelStateErrors.Add(new Error
                        {
                            Property = item.Property,
                            Message = $"{item.Message}",
                        });
                    }
                }

            }

            return modelStateErrors;
        }
    }
}