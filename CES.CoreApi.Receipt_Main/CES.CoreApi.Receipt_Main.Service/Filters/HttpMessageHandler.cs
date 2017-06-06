using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Service.Filters.Responses;
using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Service.Services;
using CES.CoreApi.Receipt_Main.Service.Utilities;
using CES.CoreApi.Shared.Persistence.Business;
using CES.CoreApi.Shared.Persistence.Data;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Service.Filters
{
    public class HttpMessageHandler : DelegatingHandler
    {
        private long persistenceID = 0;
        private readonly IPersistenceHelper _persistenceHelper = new PersistenceHelper(new PersistenceRepository());
        private LogService _logService = new LogService();
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var id = new Guid().ToString();
            var errorResponse = null as ErrorResponse;
            var isValid = true;

            try
            {
                var client = new Client();

                id = client.GetCorrelationId(request);
                Logging.Log.Id = id.ToString();
                Logging.Log.Info("------Start Calling-----");

                persistenceID = client.GetPersistenceID(out isValid, out errorResponse);

                var errors = new List<Error>();
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

                if (errors.Count > 0)
                {
                    var jsonContentRequest = request.Content.ReadAsStringAsync().Result;
                    _logService.LogInfo($"Exception: Invalid Headers. {jsonContentRequest}");
                    _persistenceHelper.CreatePersistence<string>(jsonContentRequest, persistenceID, PersistenceEventType.InvalidRequestModel);

                    errorResponse = new ErrorResponse("The headers values were not sent", (int)HttpStatusCode.BadRequest, errors, request.GetCorrelationId(), persistenceID);
                    _persistenceHelper.CreatePersistence<ErrorResponse>(errorResponse, persistenceID, PersistenceEventType.ErrorResponse);
                    _logService.LogInfoObjectToJson("Error Response: ", errorResponse);

                    Logging.Log.Info("------End Calling----" + Environment.NewLine);

                    return request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, errorResponse);
                }

                HeaderHelper.ApplicationId = client.ApplicationId;
                HeaderHelper.CesAppObjectId = client.CesAppObjectId;
                HeaderHelper.CesUserId = client.CesUserId;
                HeaderHelper.CesRequestTime = client.CesRequestTime;

                if (isValid)
                {
                    if (request.Content != null)
                    {
                        if (request.Content.Headers != null && request.Content.Headers.ContentType != null)
                        {
                            request.Content.Headers.ContentType.CharSet = request.Content.Headers.ContentType.CharSet.ToSafeDbString("iso-8859-1");
                        }

                        if (request.Method.Method == "GET")
                        {
                            Logging.Log.Info(request.RequestUri.AbsoluteUri);
                        }
                        else
                        {
                            await request.Content.ReadAsStringAsync().ContinueWith(task =>
                            {
                                Logging.Log.Info(FormatStringToJson(task.Result));
                            }, cancellationToken);
                        }
                    }

                    return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
                    {
                        var response = task.Result;
                        response.Headers.Add("ResponseId", id.ToString());
                        ObjectContent content = response.Content as ObjectContent;

                        if (content != null)
                        {
                            Logging.Log.Info(FormatStringToJson(content.ReadAsStringAsync().Result));
                        }
                        if (!response.IsSuccessStatusCode)
                        {
                            response = BuildApiResponse(request, response);
                        }

                        Logging.Log.Info("------End Calling----" + Environment.NewLine);

                        return response;
                    }, cancellationToken);
                }
                else
                {
                    return request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, errorResponse);
                }
            }
            catch (Exception ex)
            {
                var jsonContentRequest = request.Content.ReadAsStringAsync().Result;
                _logService.LogInfo($"Exception: Invalid Request Model. {jsonContentRequest}");
                _persistenceHelper.CreatePersistence<string>(jsonContentRequest, persistenceID, PersistenceEventType.InvalidRequestModel);
                var errors = new List<Error>() { new Error() { Message = ex.Message, Property = "400" } };
                errorResponse = new ErrorResponse("The request is not valid, possibly it is not well formed", (int)System.Net.HttpStatusCode.BadRequest, errors, request.GetCorrelationId(), persistenceID);
                _persistenceHelper.CreatePersistence<ErrorResponse>(errorResponse, persistenceID, PersistenceEventType.ErrorResponse);
                _logService.LogInfoObjectToJson("Error Response: ", errorResponse);

                Logging.Log.Info("------End Calling----" + System.Environment.NewLine);
                return request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }


        private HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {

            object content;
            List<Error> modelStateErrors = new List<Error>();
            var taxCreateCAFResponse = null as TaxCreateCafFResponse;
            var taxCreateDocumentResponse = null as TaxCreateDocumentResponse;
            var taxDeleteCAFResponse = null as TaxDeleteCAFResponse;
            var taxGenerateReceiptResponse = null as TaxGenerateReceiptResponse;
            var taxGetFolioResponse = null as TaxGetFolioResponse;
            var taxSearchCAFByTypeResponse = null as TaxSearchCAFByTypeResponse;
            var taxSearchDocumentResponse = null as TaxSearchDocumentResponse;
            var taxSIISendDocumentRequest = null as TaxSIISendDocumentRequest;
            var taxSIIGetDocumentResponse = null as TaxSIIGetDocumentResponse;
            var taxUpdateCAFResponse = null as TaxUpdateCafResponse;
            var taxUpdateFolioResponse = null as TaxUpdateFolioResponse;
            var taxSIIGetDocumentBatchResponse = null as TaxSIIGetDocumentBatchResponse;

            ValidationResult validationResult = null;
            ErrorResponse erroResponse = null;

            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                validationResult = content as ValidationResult;
                taxCreateCAFResponse = content as TaxCreateCafFResponse;

                taxCreateDocumentResponse = content as TaxCreateDocumentResponse;
                taxDeleteCAFResponse = content as TaxDeleteCAFResponse;
                taxGenerateReceiptResponse = content as TaxGenerateReceiptResponse;
                taxGetFolioResponse = content as TaxGetFolioResponse;
                taxSearchCAFByTypeResponse = content as TaxSearchCAFByTypeResponse;
                taxSearchDocumentResponse = content as TaxSearchDocumentResponse;
                taxSIISendDocumentRequest = content as TaxSIISendDocumentRequest;
                taxSIIGetDocumentResponse = content as TaxSIIGetDocumentResponse;
                taxUpdateCAFResponse = content as TaxUpdateCafResponse;
                taxUpdateFolioResponse = content as TaxUpdateFolioResponse;
                taxSIIGetDocumentBatchResponse = content as TaxSIIGetDocumentBatchResponse;
                
                erroResponse = content as ErrorResponse;
            }

            HttpResponseMessage newResponse = null;

            var message = string.Empty;
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    message = "Invalid request or required fields were not provided";
                    if (validationResult != null)
                        modelStateErrors = GetErrors(validationResult);

                    if (taxCreateCAFResponse != null && taxCreateCAFResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxCreateCAFResponse.ReturnInfo);

                    if (taxCreateDocumentResponse != null && taxCreateDocumentResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxCreateDocumentResponse.ReturnInfo);

                    if (taxDeleteCAFResponse != null && taxDeleteCAFResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxDeleteCAFResponse.ReturnInfo);

                    if (taxGenerateReceiptResponse != null && taxGenerateReceiptResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxGenerateReceiptResponse.ReturnInfo);

                    if (taxGetFolioResponse != null && taxGetFolioResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxGetFolioResponse.ReturnInfo);

                    if (taxSearchCAFByTypeResponse != null && taxSearchCAFByTypeResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSearchCAFByTypeResponse.ReturnInfo);

                    if (taxSearchDocumentResponse != null && taxSearchDocumentResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSearchDocumentResponse.ReturnInfo);

                    if (taxSIISendDocumentRequest != null && taxSIISendDocumentRequest.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSIISendDocumentRequest.ReturnInfo);

                    if (taxSIIGetDocumentResponse != null && taxSIIGetDocumentResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSIIGetDocumentResponse.ReturnInfo);

                    if (taxUpdateFolioResponse != null && taxUpdateFolioResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxUpdateFolioResponse.ReturnInfo);

                    if (taxSIIGetDocumentBatchResponse != null && taxSIIGetDocumentBatchResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSIIGetDocumentBatchResponse.ReturnInfo);

                    if (erroResponse != null)
                        modelStateErrors = GetErrors(erroResponse);

                    break;
                case System.Net.HttpStatusCode.InternalServerError:

                    message = $"An error occurred while processing your request";

                    if (taxCreateCAFResponse != null && taxCreateCAFResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxCreateCAFResponse.ReturnInfo);

                    if (taxCreateDocumentResponse != null && taxCreateDocumentResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxCreateDocumentResponse.ReturnInfo);

                    if (taxDeleteCAFResponse != null && taxDeleteCAFResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxDeleteCAFResponse.ReturnInfo);

                    if (taxGenerateReceiptResponse != null && taxGenerateReceiptResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxGenerateReceiptResponse.ReturnInfo);

                    if (taxGetFolioResponse != null && taxGetFolioResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxGetFolioResponse.ReturnInfo);

                    if (taxSearchCAFByTypeResponse != null && taxSearchCAFByTypeResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSearchCAFByTypeResponse.ReturnInfo);

                    if (taxSearchDocumentResponse != null && taxSearchDocumentResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSearchDocumentResponse.ReturnInfo);

                    if (taxSIISendDocumentRequest != null && taxSIISendDocumentRequest.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSIISendDocumentRequest.ReturnInfo);

                    if (taxSIIGetDocumentResponse != null && taxSIIGetDocumentResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSIIGetDocumentResponse.ReturnInfo);

                    if (taxUpdateCAFResponse != null && taxUpdateCAFResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxUpdateCAFResponse.ReturnInfo);

                    if (taxUpdateFolioResponse != null && taxUpdateFolioResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxUpdateFolioResponse.ReturnInfo);

                    if (taxSIIGetDocumentBatchResponse != null && taxSIIGetDocumentBatchResponse.ReturnInfo != null)
                        modelStateErrors = GetErrors(taxSIIGetDocumentBatchResponse.ReturnInfo);

                    break;

            }

            var errorResponse = new ErrorResponse(message, (int)response.StatusCode, modelStateErrors, request.GetCorrelationId(), persistenceID);

            _persistenceHelper.CreatePersistence<ErrorResponse>(errorResponse, persistenceID, PersistenceEventType.ReceiptGenerationError);

            newResponse = request.CreateResponse(response.StatusCode, errorResponse);

            foreach (var header in response.Headers) //Add back the response headers
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }

        private List<Error> GetErrors(ValidationResult validationResult)
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

        private List<Error> GetErrors(ReturnInfo returnInfo)
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

        private List<Error> GetErrors(ErrorResponse errorResponse)
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

        private static string FormatStringToJson(string responseString)
        {
            try
            {
                var responseJsonObj = JsonConvert.DeserializeObject(responseString);
                var responseJsonString = JsonConvert.SerializeObject(responseJsonObj, Formatting.Indented);
                return responseJsonString;
            }
            catch (JsonSerializationException ex)
            {
                Logging.Log.Error($"Error parsing response: {ex.Message}", ex);
                return responseString;
            }

        }

    }
}