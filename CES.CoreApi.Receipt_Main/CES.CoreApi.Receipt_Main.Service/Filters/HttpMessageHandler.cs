using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Infrastructure.Core;
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
        private IPersistenceHelper _persistenceHelper = PersistenceHelperFactory.GetPersistenceHelper();
        private LogService _logService = new LogService();
        private IErrorManager _errorManager = new ErrorManager();
        private ApiResponse _apiResponse = null;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var id = new Guid().ToString();
            var errorResponse = null as ErrorResponse;
            var isValid = true;

            try
            {
                IClient client = ClientFactory.GetClient();

                id = client.GetCorrelationId(request);

                Logging.Log.Id = id.ToString();
                Logging.Log.Info("------Start Calling-----");

                persistenceID = client.GetPersistenceID(out isValid, out errorResponse);

                _apiResponse = new ApiResponse(_persistenceHelper, persistenceID);

                if(request.Method.Method == "GET" && request.RequestUri.ToString().ToLower().Contains("swagger"))
                {
                    isValid = true;
                }
                else
                {
                    var errors = _errorManager.ValidateHeaders(client);

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

                    AssignHeaderValues(client);
                }

                if (isValid && request.Content != null)
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
                            response = _apiResponse.BuildApiResponse(request, response);
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

        private static void AssignHeaderValues(IClient client)
        {
            HeaderHelper.ApplicationId = client.ApplicationId;
            HeaderHelper.CesAppObjectId = client.CesAppObjectId;
            HeaderHelper.CesUserId = client.CesUserId;
            HeaderHelper.CesRequestTime = client.CesRequestTime;
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