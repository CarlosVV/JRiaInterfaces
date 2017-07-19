using CES.CoreApi.Receipt_Main.Service.Filters.Responses;
using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    public class ApiResponse
    {
        private IErrorManager _errorManager = new ErrorManager();
        private long _persistenceID = 0L;
        private IPersistenceHelper _persistenceHelper;
        public ApiResponse(IPersistenceHelper persistenceHelper, long persistenceID)
        {
            _persistenceID = persistenceID;
            _persistenceHelper = persistenceHelper;
        }
        public HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
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
                        modelStateErrors = _errorManager.GetErrors(validationResult);

                    if (taxCreateCAFResponse != null && taxCreateCAFResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxCreateCAFResponse.ReturnInfo);

                    if (taxCreateDocumentResponse != null && taxCreateDocumentResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxCreateDocumentResponse.ReturnInfo);

                    if (taxDeleteCAFResponse != null && taxDeleteCAFResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxDeleteCAFResponse.ReturnInfo);

                    if (taxGenerateReceiptResponse != null && taxGenerateReceiptResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxGenerateReceiptResponse.ReturnInfo);

                    if (taxGetFolioResponse != null && taxGetFolioResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxGetFolioResponse.ReturnInfo);

                    if (taxSearchCAFByTypeResponse != null && taxSearchCAFByTypeResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSearchCAFByTypeResponse.ReturnInfo);

                    if (taxSearchDocumentResponse != null && taxSearchDocumentResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSearchDocumentResponse.ReturnInfo);

                    if (taxSIISendDocumentRequest != null && taxSIISendDocumentRequest.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSIISendDocumentRequest.ReturnInfo);

                    if (taxSIIGetDocumentResponse != null && taxSIIGetDocumentResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSIIGetDocumentResponse.ReturnInfo);

                    if (taxUpdateFolioResponse != null && taxUpdateFolioResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxUpdateFolioResponse.ReturnInfo);

                    if (taxSIIGetDocumentBatchResponse != null && taxSIIGetDocumentBatchResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSIIGetDocumentBatchResponse.ReturnInfo);

                    if (erroResponse != null)
                        modelStateErrors = _errorManager.GetErrors(erroResponse);

                    break;
                case System.Net.HttpStatusCode.InternalServerError:

                    message = $"An error occurred while processing your request";

                    if (taxCreateCAFResponse != null && taxCreateCAFResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxCreateCAFResponse.ReturnInfo);

                    if (taxCreateDocumentResponse != null && taxCreateDocumentResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxCreateDocumentResponse.ReturnInfo);

                    if (taxDeleteCAFResponse != null && taxDeleteCAFResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxDeleteCAFResponse.ReturnInfo);

                    if (taxGenerateReceiptResponse != null && taxGenerateReceiptResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxGenerateReceiptResponse.ReturnInfo);

                    if (taxGetFolioResponse != null && taxGetFolioResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxGetFolioResponse.ReturnInfo);

                    if (taxSearchCAFByTypeResponse != null && taxSearchCAFByTypeResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSearchCAFByTypeResponse.ReturnInfo);

                    if (taxSearchDocumentResponse != null && taxSearchDocumentResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSearchDocumentResponse.ReturnInfo);

                    if (taxSIISendDocumentRequest != null && taxSIISendDocumentRequest.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSIISendDocumentRequest.ReturnInfo);

                    if (taxSIIGetDocumentResponse != null && taxSIIGetDocumentResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSIIGetDocumentResponse.ReturnInfo);

                    if (taxUpdateCAFResponse != null && taxUpdateCAFResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxUpdateCAFResponse.ReturnInfo);

                    if (taxUpdateFolioResponse != null && taxUpdateFolioResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxUpdateFolioResponse.ReturnInfo);

                    if (taxSIIGetDocumentBatchResponse != null && taxSIIGetDocumentBatchResponse.ReturnInfo != null)
                        modelStateErrors = _errorManager.GetErrors(taxSIIGetDocumentBatchResponse.ReturnInfo);

                    break;

            }

            var errorResponse = new ErrorResponse(message, (int)response.StatusCode, modelStateErrors, request.GetCorrelationId(), _persistenceID);

            _persistenceHelper.CreatePersistence<ErrorResponse>(errorResponse, _persistenceID, PersistenceEventType.ReceiptGenerationError);

            newResponse = request.CreateResponse(response.StatusCode, errorResponse);

            foreach (var header in response.Headers) //Add back the response headers
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }

    }
}