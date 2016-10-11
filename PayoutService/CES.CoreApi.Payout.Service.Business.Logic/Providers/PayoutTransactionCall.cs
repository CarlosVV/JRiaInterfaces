using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents;
using CES.CoreApi.Shared.Providers.Helper.Interfaces;
using CES.CoreApi.Shared.Providers.Helper.Model.Public.Enums;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;
using CES.CoreApi.Payout.Service.Business.Logic.Exceptions;

using System.ComponentModel;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using AutoMapper;
using CES.CoreApi.Payout.Service.Business.Contract.Enumerations;
using System.Transactions;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers
{
    public class PayoutTransactionCall
    {
        private static readonly Log4NetProxy _logger = new Log4NetProxy();
        
        
        private static IPersistenceHelper _persistenceHelper;

        public static PayoutTransactionResponseModel S_ProcessPayoutTransaction(
            IPayoutServiceProviderFactory providerFactory,
            IProviderHelper providerHelper, 
            IPersistenceHelper persistenceHelper,
            IMapper mappingHelper,
            IEmailHelper emailHelper,
            PayoutTransactionRequestModel request)
        {
            _persistenceHelper = persistenceHelper;

            int PERSISTENCE_ID = 0;
            int PROVIDER_ID = 0;

            //Set IDs
            PERSISTENCE_ID = request.PersistenceID;

            //Get fields for checks.
            string inputOrderPIN = request.OrderPIN ?? "";
            DateTime inputIDExpDate = request.Beneficiary.IDExpDate;
            string inputIDNumber = request.Beneficiary.IDNumber ?? "";


            //Check Order PIN.
            if (inputOrderPIN.Length < 1)
            {
                return MakeErrorPayTrans(
                    Constants.PAYOUT_SERVICE_FAILURE_CODE,
                    Constants.PAYOUT_SERVICE_FAILURE_MESSAGE +
                    Messages.S_GetMessage("InvalidOrderPIN"), PERSISTENCE_ID, PROVIDER_ID);


            }


            //PROVIDER: Get the provider that should process this call:
            //Check the PIN to see if it is Ria transaction or needs to be send to a partner provider.
            var providerPayoutInfo = providerHelper.GetPayoutProvider(inputOrderPIN);          
          
            if (providerPayoutInfo == null)
            {               
                return MakeErrorPayTrans(
                          Constants.PAYOUT_SERVICE_FAILURE_CODE,
                          Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                          + Messages.S_GetMessage("NoProviderFound"), PERSISTENCE_ID, PROVIDER_ID);
            }

            //Set provider ID
            PROVIDER_ID = providerPayoutInfo.ProviderID;
            request.ProviderID = PROVIDER_ID;


            try
            {
#region Log
                //Log the Request Data:
                try
                {
                    string requestInputs =
                        "AgentID=" + request.RequesterInfo.AgentID + ";"
                        + "AgentLocID=" + request.RequesterInfo.AgentLocID + ";"
                        + "PersistenceID=" + request.PersistenceID + ";"
                        + "OrderID=" + request.OrderID + ";"
                        + "OrderPIN=" + request.OrderPIN + ";"
                        + "AgentCity=" + request.PayAgentCity + ";"
                        + "AgentCountry=" + request.PayAgentCountry + ";"
                        + "AgentCountryID=" + request.PayAgentCountryID + ";"
                        + "AgentState=" + request.PayAgentState + ";"
                        + "ApproverID=" + request.ApproverID + ";"
                        + "BeneficiaryAddress=" + request.Beneficiary.Address + ";"
                        + "BeneficiaryCity=" + request.Beneficiary.City + ";"
                        + "BeneficiaryCountry=" + request.Beneficiary.Country + ";"
                        + "BeneficiaryIDNumber=" + request.Beneficiary.IDNumber + ";"
                        + "BeneficiaryIDSerialNumber=" + request.Beneficiary.IDSerialNumber + ";"
                        + "BeneficiaryIDType=" + request.Beneficiary.IDType + ";"
                        + "BeneficiaryIDIssuer=" + request.Beneficiary.IDIssuer + ";"
                        + "BeneficiaryPhoneNumber=" + request.Beneficiary.PhoneNumber + ";"
                        + "BeneficiaryFirstName=" + request.Beneficiary.FirstName + ";"
                        + "BeneficiaryLastName1=" + request.Beneficiary.LastName1 + ";"
                        + "BeneficiaryLastName2=" + request.Beneficiary.LastName2 + ";"
                        + "BeneficiaryMiddleName=" + request.Beneficiary.MiddleName + ";"
                        + "OrderLookupCode=" + request.OrderLookupCode + ";"
                        + "ComplianceRun=" + request.ComplianceRun + ";"
                        + "ConvertedCurrency=" + request.ConvertedCurrency + ";"
                        + "ConvertedRate=" + request.ConvertedRate + ";"
                        + "CustomerRelationShip=" + request.CustomerRelationShip + ";"
                        + "CustomerRelationShipID=" + request.CustomerRelationShipID + ";"
                        + "Date=" + request.RequesterInfo.UtcTime + ";"
                        + "Override=" + request.Override + ";"
                        + "PayoutMethodID=" + request.PayoutMethodID + ";"
                        + "PersistenceID=" + request.PersistenceID + ";"
                        + "TellerDrawerInstanceID=" + request.TellerDrawerInstanceID + ";"
                        + "TransferReason=" + request.TransferReason + ";"
                        + "TransferReasonID=" + request.TransferReasonID + ";"
                        + "UserID=" + request.RequesterInfo.UserID;
                    _logger.PublishInformation(requestInputs);
                }
                catch (Exception e)
                {
                    //NOTE: Don't stop the process if there is an error logging the request.
                    //Try to log the problem, but if the error is in writing to the log, this write will fail too.
                    _logger.PublishWarning("Could not log request data: " + e.Message);
                }

 #endregion
                //Check Required Data
                var errorMessage = string.Empty;
                var requiredFields = new List<PayoutFieldsModel>().GetRequiredPayoutFiledsByPersistenceID(request.PersistenceID, persistenceHelper);

                if (requiredFields != null && requiredFields.Count > 0)
                {
                    if (!S_CheckAllPayoutRequeridFields(requiredFields, request, ref errorMessage))
                        return MakeErrorPayTrans(
                            Constants.PAYOUT_SERVICE_FAILURE_CODE,
                            Constants.PAYOUT_SERVICE_FAILURE_MESSAGE +
                            errorMessage, PERSISTENCE_ID, PROVIDER_ID);

                }

              
                

                var interfaceName = providerPayoutInfo.GetConfiguration<string>(ConfigurationProviderKeys.InterfacePayout);
                if (string.IsNullOrEmpty(interfaceName))
                {
                    return MakeErrorPayTrans(
                              Constants.PAYOUT_SERVICE_FAILURE_CODE,
                              Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                              + Messages.S_GetMessage("NoProviderInterfaceFound"), PERSISTENCE_ID, PROVIDER_ID);
                }

                var providerInstance = providerFactory.GetInstance<ICorrespondentAPI>(interfaceName);
                providerInstance.SetProviderInfo(providerPayoutInfo);
                if (providerInstance == null)
                {
                    return MakeErrorPayTrans(
                              Constants.PAYOUT_SERVICE_FAILURE_CODE,
                              Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                              + Messages.S_GetMessage("CouldNotCreateProviderInstance"), PERSISTENCE_ID, PROVIDER_ID);
                }
                _logger.PublishInformation("Provider selected: " + providerPayoutInfo.Name);

                
               

                //PAYOUT the Transaction:
                PayoutTransactionResponseModel payoutResp = new PayoutTransactionResponseModel();

                //PersistenceValidate
                payoutResp = ValidatePesistence(request);
             
                if (payoutResp.ReturnInfo.ErrorCode !=0)
                {
                    return MakeErrorPayTrans(
                              Constants.PAYOUT_SERVICE_FAILURE_CODE,
                              Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                              + payoutResp.ReturnInfo.ErrorMessage, PERSISTENCE_ID, PROVIDER_ID);
                }


                try
                {
                    //Record the request:
                    //Persistence (W5)
                    var persistenceEventPayoutTransactionRequest = new PersistenceEventModel(PERSISTENCE_ID, PROVIDER_ID, PersistenceEventType.PayoutRequest);
                    persistenceEventPayoutTransactionRequest.SetContentObject<PayoutTransactionRequestModel>(request);
                    persistenceEventPayoutTransactionRequest.RequesterInfo = mappingHelper.Map<Contract.Models.RequesterInfoModel, Shared.Persistence.Model.RequesterInfoModel>(request.RequesterInfo);
                    persistenceHelper.CreatePersistenceEvent(persistenceEventPayoutTransactionRequest);
                    //End persistence (W5)

                   

                    //// Validate Transaction by calling Ria SP: ////
                    //Different for each provider: Handled within the provider processing.
                    payoutResp = providerInstance.ValidatePayoutData(request);
                   
                    //If there was a failure return response as is. Otherwise continue to process:
                    //If response is null means that  is not implemented (continue)
                    if (payoutResp != null)
                    {
                        if (payoutResp.ReturnInfo.ErrorCode.ToString() != "1")
                        {
                            LogPayoutTransactionResponse(payoutResp);
                            return payoutResp;
                        }
                    }

                    //// Create Order in Ria DB: ////
                    try
                    {
                        var  createOrderResponse = providerInstance.CreateOrderFromProviderData(request);
                        _logger.PublishInformation("OrderID= " + createOrderResponse.TransactionID);
                        //Set the orderID in the request, so that it will be used in all the next events.
                        request.OrderID = createOrderResponse.TransactionID;

                        //0= Create OK
                        //2= Order Already exists, so continue
                        var validStatus = new List<int>{ 0, 2 };

                        if (!validStatus.Contains(createOrderResponse.ReturnValue))
                        {
                            throw new InvalidDataException("OrderCreateFromProviderData: " + createOrderResponse.ReturnValue + ":" + createOrderResponse.ReturnMessage);
                        }
                        
                    }
                    catch (Exception e)
                    {
                        _logger.PublishInformation("Error in Create Order from Provider Data Call: " + e.Message);
                        throw e;
                    }


                    //TODO: Persistence (W6): Compliance Request [There will be no request initially until ComplianceService is built]

                    //// Call Complaince on transaction by calling Ria SP: ////
                    //Different for each provider: Handled within the provider processing.
                   
                    
                  
                    //Persistence (W6)
                    var persistenceEventComplianceCheckPayoutRequest = new PersistenceEventModel(PERSISTENCE_ID, PROVIDER_ID, PersistenceEventType.ComplianceCheckPayoutRequest);
                    persistenceEventComplianceCheckPayoutRequest.SetContentObject<PayoutTransactionRequestModel>(request);
                    persistenceHelper.CreatePersistenceEvent(persistenceEventComplianceCheckPayoutRequest);
                    //End persistence (W6) 

                    //We exclude code from this transaction because some issue may be created and the transaction must not undo this. (Later, when the call is WebAPI compliance service, this will not be a problem)
                   
                    payoutResp = providerInstance.PayoutComplianceCheck(request);
                    
                    payoutResp.PersistenceID = PERSISTENCE_ID;                      


                    //Persistence (W9)
                    var persistenceEventComplianceCheckPayoutResponse = new PersistenceEventModel(PERSISTENCE_ID, PROVIDER_ID, PersistenceEventType.ComplianceCheckPayoutResponse);
                    persistenceEventComplianceCheckPayoutResponse.SetContentObject<PayoutTransactionResponseModel>(payoutResp);
                    persistenceHelper.CreatePersistenceEvent(persistenceEventComplianceCheckPayoutResponse);
                    //End persistence (W9) 


                    //ReviewIssuesStatus posible values: 1,2 or 0
                   
                    //Unreviewed issues (1)
                    if (payoutResp.ReviewIssuesStatus == 1)
                    {
                        return payoutResp;
                    }

                    //(2) All Issues Closed, Goto Pay: https://msdn.microsoft.com/en-us/library/13940fs2.aspx
                    if (payoutResp.ReviewIssuesStatus == 2)
                    {
                        goto Pay;
                    }

                  
                    //(0) Process Rules
                    if (payoutResp.IssuesFound)
                    {


                        //1 - Reject
                        //2 - Reject Without Information
                        var rejectSataus = new List<int>() { 1, 2 };
                        if (rejectSataus.Contains(payoutResp.ActionID))
                        {
                            //TODO: Send Mail
                            var sendEmailRequest = GetEmailRejectOrder(request);
                            using (var tx = new TransactionScope(TransactionScopeOption.Suppress))
                            {
                                var sendEmailResponse = emailHelper.EnviarMail(sendEmailRequest);
                                sendEmailRequest.MessageID = sendEmailResponse.ReturnMessageID;
                            }
                            
                      
                            //Persistence (W18)
                            var persistenceEventsendEmailRequest = new PersistenceEventModel(PERSISTENCE_ID, PROVIDER_ID, PersistenceEventType.SendEmailRequest);
                            persistenceEventsendEmailRequest.SetContentObject<SendEmailRequestModel>(sendEmailRequest);
                            persistenceHelper.CreatePersistenceEvent(persistenceEventsendEmailRequest);
                            //End persistence (W18)                                 

                        }


                        //10 - On Hold
                        //11 - On Hold Without Information
                        var holdStatus = new List<int>() { 10, 11 };
                        if (holdStatus.Contains(payoutResp.ActionID))
                        {                               
                            payoutResp = providerInstance.PlaceOrderLegalHold(request);                                 
                        }   

                        return payoutResp;
                    }


                    Pay:

                    //// Call Payout event with PROVIDER: ////
                    payoutResp = providerInstance.PayoutTransaction(request);
                    payoutResp.PersistenceID = request.PersistenceID;
                    payoutResp.PIN = request.OrderPIN;
                    payoutResp.OrderID = request.OrderID;
                    payoutResp.BeneficiaryPayout = new MoneyModel(request.PayoutAmount, request.PayoutCurrency);

                    //If there was a failure return response as is. Otherwise continue to process:                   
                    if (payoutResp.ReturnInfo.ErrorCode !=(int) PayoutMessageCode.PaidSuccessful)
                    {
                        
                        LogPayoutTransactionResponse(payoutResp);
                        return payoutResp;
                    }

                    //// Call Confirm Payout event with provider: ////
                    ConfirmPayoutResponseModel confPayData = providerInstance.ConfirmPayout(PERSISTENCE_ID, inputOrderPIN, inputIDNumber, inputIDExpDate, PROVIDER_ID);
                    //If there was a failure return response as is. Otherwise continue to process:
                    if (confPayData.TransactionStatusCode != 0)
                    {
                        string eMsg = "Error in Confirm Order with Provider Service: " + confPayData.TransactionStatusCode + ":" + confPayData.TransactionStatusMesage;
                        _logger.PublishInformation(eMsg);
                        throw new Exception(eMsg);
                    }
                    //Check response to this to make sure confirm went OK.
                    //TODO: do we need to unwind things if there was a failure?
                    //TODO: Need to reset values in payout response object at this step or not?

                    //// Save the Transaction on the Ria DB as Confirmed: ////
                    //When it is not necessary to implement returns null
                    var payoutRespTemp = providerInstance.SaveTransactionAsPaid(request);


                    if (payoutRespTemp != null)
                    {
                     

                        payoutRespTemp.PersistenceID = PERSISTENCE_ID;
                        payoutRespTemp.BeneficiaryFee = payoutResp.BeneficiaryFee;
                        payoutRespTemp.BeneficiaryPayout = payoutResp.BeneficiaryPayout;
                        payoutRespTemp.ConfirmationNumber = payoutResp.ConfirmationNumber;
                        payoutRespTemp.OrderID = payoutResp.OrderID;
                        payoutRespTemp.PIN = payoutResp.PIN;
                     

                        payoutResp = payoutRespTemp;
                        if (payoutResp.ReturnInfo.ErrorCode.ToString() != "1")
                        {
                            //Log the error code, but don't stop the process since the next step is to return the response object any way.
                            LogPayoutTransactionResponse(payoutResp);
                        }
                    }

                }
                catch (Exception e)
                {
                    return MakeErrorPayTrans(
                        Constants.PNG_SERVICE_FAILURE_CODE,
                        Constants.PNG_SERVICE_FAILURE_MESSAGE +
                        e.Message, PERSISTENCE_ID, PROVIDER_ID);
                }


                //////////////////////////////////////
                //RESPONSE:
                //Log response:
                string transInfosOutputs = "";
                //Check Response to log correct items:
                if (payoutResp == null)
                {
                    transInfosOutputs +=
                            "ProviderName = " + providerPayoutInfo.Name + ";"
                            + "ErroCode=" + payoutResp.ReturnInfo.ErrorCode ?? "" + ";"
                            + "ErroMessage=" + payoutResp.ReturnInfo.ErrorMessage ?? "" + ";";
                    _logger.PublishInformation("Returned MSG: Outputs: " + transInfosOutputs);

                    return MakeErrorPayTrans(Constants.PAYOUT_SERVICE_FAILURE_CODE,
                    Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                    + Messages.S_GetMessage("PayoutTransactionFailed"), PERSISTENCE_ID, PROVIDER_ID);
                }

               
                if (payoutResp.ReturnInfo != null)
                {
                    if (payoutResp.ReturnInfo.ErrorCode !=(int) Contract.Enumerations.PayoutMessageCode.PaidSuccessful)
                    {
                        transInfosOutputs +=
                            "ProviderName = " + providerPayoutInfo.Name + ";"
                            + "ErroCode=" + payoutResp.ReturnInfo.ErrorCode ?? "" + ";"
                            + "ErroMessage=" + payoutResp.ReturnInfo.ErrorMessage ?? "" + ". " + payoutResp.GetInfoFields() + ";";

                        _logger.PublishInformation("Returned MSG: " + "Outputs: " + transInfosOutputs);
                        return MakeErrorPayTrans(
                            Constants.PAYOUT_SERVICE_FAILURE_CODE,
                            Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                            + payoutResp.ReturnInfo.ErrorMessage + ". "
                            + payoutResp.GetInfoFields(), PERSISTENCE_ID, PROVIDER_ID);
                    }
                }
			
				var successfulMessage = PayoutMessageCode.PaidSuccessful.Description();

				payoutResp.ReturnInfo = new ReturnInfoModel((int)PayoutMessageCode.PaidSuccessful, successfulMessage);
                
              

                transInfosOutputs +=
                    "ProviderName = " + providerPayoutInfo.Name + ";"
                    + "PersistenceID=" + payoutResp.PersistenceID ?? "" + ";"
                    + "ConfirmationNumber=" + payoutResp.ConfirmationNumber ?? "" + ";"
                    + "BeneficiaryFeeAmount=" + payoutResp.BeneficiaryFee.Amount ?? "" + ";"
                    + "BeneficiaryFee=Currency" + payoutResp.BeneficiaryFee.CurrencyCode ?? "" + ";";
                _logger.PublishInformation("Returned MSG: Outputs: " + transInfosOutputs);


                //Persistence (W24)
                var persistenceEventPayoutTransactionResponse = new PersistenceEventModel(PERSISTENCE_ID, PROVIDER_ID, PersistenceEventType.PayoutResponseSuccess);
                persistenceEventPayoutTransactionResponse.SetContentObject<PayoutTransactionResponseModel>(payoutResp);
                persistenceHelper.CreatePersistenceEvent(persistenceEventPayoutTransactionResponse);
                //End persistence (W24) 


                //Return response;
                payoutResp.OrderStatus = payoutResp.ReturnInfo.ErrorMessage;
                return payoutResp;
            }
            catch (Exception e)
            {
                return MakeErrorPayTrans(Constants.PAYOUT_SERVICE_FAILURE_CODE,
                    Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                    + Messages.S_GetMessage("PayoutTransactionFailed")
                    + e.Message, PERSISTENCE_ID, PROVIDER_ID);
            }
        }

        /// <summary>
        /// Create an Error Response.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="statusMessage"></param>
        /// <returns></returns>
        private static PayoutTransactionResponseModel MakeErrorPayTrans(int statusCode, string statusMessage, int persistenceID, int providerID)
        {
            string errorMsg = statusCode + ":" + statusMessage;

            //Persistence (Error Persistence)
            var persistenceEventError = new PersistenceEventModel(persistenceID, providerID, PersistenceEventType.FaultException);
            persistenceEventError.SetContentObject<string>(errorMsg);
            _persistenceHelper.CreatePersistenceEvent(persistenceEventError);
            //End persistence (Error Persistence)

            //Log the Error in the info log too. It does get logged as the fault response in the Error log, but
            //it is much harder to find there and is not inline with the request and other steps in the process.
            _logger.PublishInformation(errorMsg);

            //NOTE: Any exception that is thrown will get passed up to the coreAPI foundation.
            //It automatically formats it into a fault code response object.
            //It also automatically logs it.
            //throw new PayoutServiceProcessingException(errorMsg);

            return new  PayoutTransactionResponseModel(statusCode, statusMessage, persistenceID);
        }



        private static bool S_CheckAllPayoutRequeridFields(
            List<PayoutFieldsModel> payoutRequiredFields, 
            PayoutTransactionRequestModel request, 
            ref string message)
        {
            var stbMsgs = new StringBuilder();
            message = string.Empty;
            payoutRequiredFields.ForEach(field => stbMsgs.Append(S_CheckPayoutField(field, request)));
            message = stbMsgs.ToString();

            if (message.Length > 0)
                return false;

            return true;
        }

        private static string S_CheckPayoutField(PayoutFieldsModel field, PayoutTransactionRequestModel request)
        {
            if (!field.FieldRequired) return string.Empty;

            var msgFieldNotSupplied = Messages.S_GetMessage("FieldNotSupplied");
            var msg = string.Empty;

            switch (field.FieldName)
            {
                case "fBenTaxID":
                    if (string.IsNullOrEmpty(request.Beneficiary.TaxID))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.TaxID");
                    break;
                case "fBenOccupation":
                    if (string.IsNullOrEmpty(request.Beneficiary.Occupation))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.Occupation");
                    break;
                case "fBenIDNo":
                    if (string.IsNullOrEmpty(request.Beneficiary.IDNumber))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.IDNumber");
                    break;
                case "fBenIDIssuedByState":
                    if (string.IsNullOrEmpty(request.Beneficiary.IDIssuedByState))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.IDIssuedByState");
                    break;
                case "fBenIDIssuedByCountry":
                    if (string.IsNullOrEmpty(request.Beneficiary.IDIssuedByCountry))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.IDIssuedByCountry");
                    break;
                case "fBenIDIssuedBy":
                    if (string.IsNullOrEmpty(request.Beneficiary.IDIssuedBy))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.IDIssuedBy");
                    break;
                case "fBenIDExp":
                    if (request.Beneficiary.IDExpDate == null)
                        return string.Format(msgFieldNotSupplied, "Beneficiary.IDExpDate");
                    break;
                case "fBenIDType":
                    if (string.IsNullOrEmpty(request.Beneficiary.IDType))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.IDType");
                    break;
                case "fBenBirthDate":
                    if (request.Beneficiary.DateOfBirth == null)
                        return string.Format(msgFieldNotSupplied, "Beneficiary.DateOfBirth");
                    break;
                case "fTransferReason":
                    if (string.IsNullOrEmpty(request.TransferReason))
                        return string.Format(msgFieldNotSupplied, "TransferReason");
                    break;
                case "fBenTelNo":
                    if (string.IsNullOrEmpty(request.Beneficiary.PhoneNumber))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.PhoneNumber");
                    break;
                case "fBenCountryOfBirth":
                    if (string.IsNullOrEmpty(request.Beneficiary.CountryOfBirth))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.CountryOfBirth");
                    break;
                case "fBenNationality":
                    if (string.IsNullOrEmpty(request.Beneficiary.Nationality))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.Nationality");
                    break;
                case "fBenCustRelationshipID":
                    //if (string.IsNullOrEmpty(request.Beneficiary.Curp))
                    //    return string.Format(msgFieldNotSupplied, field.FieldName);
                    break;
                case "fBenStateOfBirth":
                    if (request.Beneficiary.BirthStateID == null || request.Beneficiary.BirthStateID == 0)
                        return string.Format(msgFieldNotSupplied, "Beneficiary.BirthStateID");
                    break;
                case "fBenCityOfBirth":
                    if (request.Beneficiary.BirthCityID == null || request.Beneficiary.BirthCityID == 0)
                        return string.Format(msgFieldNotSupplied, "Beneficiary.BirthCityID");
                    break;
                case "fBenSex":
                    if (string.IsNullOrEmpty(request.Beneficiary.Gender))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.Gender");
                    break;
                case "fCountryOfResidence":
                    if (string.IsNullOrEmpty(request.Beneficiary.CountryOfResidence))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.CountryOfResidence");
                    break;
                case "fBenIDIssuedDate":
                    if (request.Beneficiary.IDIssuedDate == null)
                        return string.Format(msgFieldNotSupplied, "Beneficiary.IDIssuedDate");
                    break;
                case "BeneficiaryFullAddress":
                    if (string.IsNullOrEmpty(request.Beneficiary.Address))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.Address");
                    break;
                case "pmtBenCity":
                    if (request.Beneficiary.CityID == null || request.Beneficiary.CityID == 0)
                        msg = string.Format(msgFieldNotSupplied, "Beneficiary.CityID ");
                    if (string.IsNullOrEmpty(request.Beneficiary.City))
                        msg = msg + string.Format(msgFieldNotSupplied, "Beneficiary.City");
                    return msg;
                case "pmtBenAddress":
                    if (string.IsNullOrEmpty(request.Beneficiary.Address))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.TaxID");
                    break;
                case "pmtBenPostalCode":
                    if (string.IsNullOrEmpty(request.Beneficiary.PostalCode))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.PostalCode");
                    break;
                case "pmtBenState":
                    if (request.Beneficiary.StateID == null || request.Beneficiary.StateID == 0)
                        msg = string.Format(msgFieldNotSupplied, "Beneficiary.StateID");

                    if (string.IsNullOrEmpty(request.Beneficiary.State))
                        msg = msg + string.Format(msgFieldNotSupplied, "Beneficiary.State");

                    return msg;

                case "pmtBenCountry":
                    if (string.IsNullOrEmpty(request.Beneficiary.Country))
                        return string.Format(msgFieldNotSupplied, "Beneficiary.Country");
                    break;
            }

            return string.Empty; ;
        }

        /// <summary>
        /// Log the Response:
        /// </summary>
        /// <param name="response"></param>
        private static void LogPayoutTransactionResponse(PayoutTransactionResponseModel response)
        {
            string logMsg = "Response:  ";

            if (response == null)
            {
                logMsg += "Response object is null.";
            }
            else
            {
                if (response.ReturnInfo == null)
                {
                    logMsg += "Return Code and Message are null.";
                }
                else
                {
                    logMsg += "ErrorCode=" + response.ReturnInfo.ErrorCode ?? "" + ";";
                    logMsg += "ErrorMessage=" + response.ReturnInfo.ErrorMessage ?? "" + ";";
                    logMsg += "AvailableForPayout=" + response.ReturnInfo.AvailableForPayout + ";";
                    logMsg += "AllowUnusualOrderReporting=" + response.ReturnInfo.AllowUnusualOrderReporting + ";";
                    logMsg += "RemainingBalanceWarningMsg=" + response.ReturnInfo.RemainingBalanceWarningMsg ?? "" + ";";
                    logMsg += "UsePayoutGateway=" + response.ReturnInfo.UsePayoutGateway + ";";
                    if (response.BeneficiaryFee == null)
                    {
                        logMsg += "BeneficiaryFee=NULL;";
                    }
                    else
                    {
                        logMsg += "BeneficiaryFeeAMT=" + response.BeneficiaryFee.Amount + ";";
                        logMsg += "BeneficiaryFeeCUR=" + response.BeneficiaryFee.CurrencyCode ?? "" + ";";
                    }
                    logMsg += "ConfirmationNumber=" + response.ConfirmationNumber ?? "" + ";";
                    logMsg += "PersistenceID=" + response.PersistenceID + ";";
                    if (response.PayoutRequiredFields != null)
                    {
                        foreach (PayoutFieldsModel f in response.PayoutRequiredFields)
                        {
                            logMsg += "FieldID=" + f.FieldID + "; ";
                            logMsg += "FieldName=" + f.FieldName ?? "" + "; ";
                            logMsg += "FieldRequired=" + f.FieldRequired + "; ";
                            //logMsg += "DataErrorCode=" + f.DataErrorCode ?? "" + ";";
                            //logMsg += "DataExists=" + f.DataExists + ";";
                            //logMsg += "DataInvalid=" + f.DataInvalid + ";";
                            //logMsg += "DisplayName=" + f.DisplayName ?? "" + ";";
                            //logMsg += "NoteOnData=" + f.NoteOnData ?? "" + ";";
                        }
                    }
                }
            }
            _logger.PublishInformation(logMsg);
        }

        private  static PayoutTransactionResponseModel ValidatePesistence(PayoutTransactionRequestModel request)
        {

            var getTransactionInfoPersistence = _persistenceHelper.GetPersistence(request.PersistenceID);


            var persistenceEventPayoutW14 = getTransactionInfoPersistence.PersistenceEvents.FirstOrDefault(ev => ev.PersistenceEventTypeID == PersistenceEventType.PayoutResponseSuccess);

            if(persistenceEventPayoutW14!=null)
            {
                var transactionInfoPayout = persistenceEventPayoutW14.GetPersistenceObject<GetTransactionInfoResponseModel>();

                if(transactionInfoPayout!=null)
                    return new PayoutTransactionResponseModel(325, "Order is already Paid", request.PersistenceID);
            }

            //Persistence number exists
            if (getTransactionInfoPersistence == null)
                return new PayoutTransactionResponseModel(301, "Persistence number does not exist", request.PersistenceID);

           

            //validate lifeTime            
            var lifeTime = ConfigSettings.PersistenceLifeTime();
   
            if(getTransactionInfoPersistence.Time.AddMinutes(lifeTime)<DateTime.Now)
                return new PayoutTransactionResponseModel(302, "PersistenceID has expired", request.PersistenceID);

            //Validate Provider
            var persistenceEvent = getTransactionInfoPersistence.PersistenceEvents.Find(ev => ev.PersistenceEventTypeID == PersistenceEventType.PayoutPinRequestInfoResponse);

            if (persistenceEvent == null)
                return new PayoutTransactionResponseModel(303, "Event does not exist", request.PersistenceID);

            //Validate Provider          
            if (persistenceEvent.ProviderID != request.ProviderID)
                return new PayoutTransactionResponseModel(304, "Invalid ProviderID", request.PersistenceID);

            var transactionInfo = persistenceEvent.GetPersistenceObject<GetTransactionInfoResponseModel>();
            if(transactionInfo==null)
                return new PayoutTransactionResponseModel(306, "Persistence Object not found", request.PersistenceID);

            //Orden Pin
            if (transactionInfo.OrderPIN != request.OrderPIN)
                return new PayoutTransactionResponseModel(326, "Invalid PersistenceID. Please get transaction info again.", request.PersistenceID);

            if (transactionInfo.TransferStatus.Contains("paid"))
                return new PayoutTransactionResponseModel(325, "Order is already Paid", request.PersistenceID);

            if (transactionInfo.ReturnInfo == null)
                return new PayoutTransactionResponseModel(307, "Persistence ReturnInfo Object not found", request.PersistenceID);

            if (!transactionInfo.ReturnInfo.AvailableForPayout)
                return new PayoutTransactionResponseModel(305, "Order not available", request.PersistenceID);

            //Sender Name
            var senderNameSimilarPercent = ConfigSettings.SenderNameSimilarPercent();

            //if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.SenderInfo.Name,request.Sender.Name) < senderNameSimilarPercent)
            //    return new PayoutTransactionResponseModel(310, "Too difference in Sender.Name", request.PersistenceID);

            //////////////////if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.SenderInfo.FirstName, request.Sender.FirstName) < senderNameSimilarPercent)
            //////////////////{
            //////////////////    return new PayoutTransactionResponseModel(311, "Too difference in Sender.FirstName " + 
            //////////////////        transactionInfo.SenderInfo.FirstName + " vs " + request.Sender.FirstName, request.PersistenceID);
            //////////////////}

            //////////////////if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.SenderInfo.LastName1, request.Sender.LastName1) < senderNameSimilarPercent)
            //////////////////{
            //////////////////    return new PayoutTransactionResponseModel(312, "Too difference in Sender.LastName1 " +
            //////////////////        transactionInfo.SenderInfo.LastName1 + " vs " + request.Sender.LastName1, request.PersistenceID);
            //////////////////}

            //if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.SenderInfo.LastName2, request.Sender.LastName2) < senderNameSimilarPercent)
            //    return new PayoutTransactionResponseModel(313, "Too difference in Sender.LastName2", request.PersistenceID);

            //if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.SenderInfo.MiddleName, request.Sender.MiddleName) >< senderNameSimilarPercent)
            //    return new PayoutTransactionResponseModel(314, "Too difference in Sender.MiddleName", request.PersistenceID);

            //Sender Name
            var beneficiaryNameSimilarPercent = ConfigSettings.BeneficiaryNameSimilarPercent();

            //if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.BeneficiaryInfo.Name, request.Beneficiary.Name) < beneficiaryNameSimilarPercent)
            //    return new PayoutTransactionResponseModel(320, "Too difference in Beneficiary.Name", request.PersistenceID);

            ////////////////////if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.BeneficiaryInfo.FirstName, request.Beneficiary.FirstName) < beneficiaryNameSimilarPercent)
            ////////////////////{
            ////////////////////    return new PayoutTransactionResponseModel(321, "Too difference in Beneficiary.FirstName " +
            ////////////////////        transactionInfo.BeneficiaryInfo.FirstName + " vs " + request.Beneficiary.FirstName, request.PersistenceID);
            ////////////////////}

            ////////////////////if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.BeneficiaryInfo.LastName1, request.Beneficiary.LastName1) < beneficiaryNameSimilarPercent)
            ////////////////////{
            ////////////////////    return new PayoutTransactionResponseModel(322, "Too difference in Beneficiary.LastName1 " + 
            ////////////////////        transactionInfo.BeneficiaryInfo.LastName1 + " vs " + request.Beneficiary.LastName1, request.PersistenceID);

            ////////////////////}

            //if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.BeneficiaryInfo.LastName2, request.Beneficiary.LastName2) < beneficiaryNameSimilarPercent)
            //    return new PayoutTransactionResponseModel(323, "Too difference in Beneficiary.LastName2", request.PersistenceID);

            //if (DamerauLevenshteinDistance.GetSimilPercent(transactionInfo.BeneficiaryInfo.MiddleName, request.Beneficiary.MiddleName) < beneficiaryNameSimilarPercent)
            //    return new PayoutTransactionResponseModel(324, "Too difference in Beneficiary.MiddleName", request.PersistenceID);

            return new PayoutTransactionResponseModel(0, string.Empty , request.PersistenceID); ;
        }

        private static SendEmailRequestModel GetEmailRejectOrder(PayoutTransactionRequestModel requestModel)
        {
            var sendEmailRequest = new SendEmailRequestModel()
            {
                Message = string.Format("Your order PIN {0} was rejected for payout due to a Watch List hit.", requestModel.OrderPIN),
                MessageType = "Ext Payout Reject WL",
                MessageFormat = "Text/HTML",
                MessageFrom =  ConfigSettings.EmailMessageFrom(),
                MessageTo = ConfigSettings.EmailMessageTo(),
                MessageCc=string.Empty,
                MessageBcc =string.Empty,
                MessageSubject = "Order Rejected during payout",
                MessageSendMethod = "Email",
                UserNameID= requestModel.RequesterInfo.UserID,
                PersistenceID = requestModel.PersistenceID,
                ProviderID = requestModel.ProviderID,
            };

            return sendEmailRequest;
        }



    }
}
