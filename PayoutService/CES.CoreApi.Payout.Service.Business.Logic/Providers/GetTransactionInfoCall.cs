using AutoMapper;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Service.Business.Logic.Exceptions;
using CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using CES.CoreApi.Shared.Providers.Helper.Interfaces;
using CES.CoreApi.Shared.Providers.Helper.Model.Public.Enums;
using System;


namespace CES.CoreApi.Payout.Service.Business.Logic.Providers
{
    public class GetTransactionInfoCall
    {
        private static readonly Log4NetProxy _logger = new Log4NetProxy();
        private static int _providerID = 0;
        private static int _persistenceID = 0;
        private static  IPersistenceHelper _persistenceHelper;
        public static GetTransactionInfoResponseModel S_ProcessGetTransactionInfo(
            IPayoutServiceProviderFactory providerFactory, 
            IProviderHelper providerHelper, IPersistenceHelper persistenceHelper, IMapper mappingHelper,
            GetTransactionInfoRequestModel request)
        {
            _persistenceHelper = persistenceHelper;
            try
            {
                //Log the Request Data:
                try
                {
                    string requestInputs =
                        
                        "OrderPIN=" + request.OrderPIN + ";"
                        + "PersistenceID=" + request.PersistenceID + ";"
                        + "AgentID=" + request.RequesterInfo.AgentID + ";"
                        + "AgentLocID=" + request.RequesterInfo.AgentLocID + ";"
                        + "CountryTo=" + request.CountryTo + ";"
                        + "Date=" + request.RequesterInfo.UtcTime + ";"
                        + "OrderID=" + request.OrderID + ";"
                        + "PersistenceID=" + request.PersistenceID + ";"
                        + "RequesterInfo=" + request.RequesterInfo + ";"
                        + "StateTo=" + request.StateTo + ";"
                        + "UserID=" + request.RequesterInfo.UserLoginID + ";";
                    _logger.PublishInformation(requestInputs);

                }
                catch (Exception e)
                {
                    //NOTE: Don't stop the process if there is an error logging the request.
                    //Try to log the problem, but if the error is in writing to the log, this write will fail too.
                    _logger.PublishWarning("Could not log request data: " + e.Message);
                }

                //Check Order PIN.
                if (request.OrderPIN.Length < 1)
                {
                    return MakeErrorGetTransInfo(
                        Constants.PAYOUT_SERVICE_FAILURE_CODE,
                        Constants.PAYOUT_SERVICE_FAILURE_MESSAGE +
                        Messages.S_GetMessage("InvalidOrderPIN"));
                }


                //PROVIDER: Get the provider that should process this call:
                //Check the PIN to see if it is Ria transaction or needs to be send to a partner provider.
                var providerPayoutInfo = providerHelper.GetPayoutProvider(request.OrderPIN);
                if (providerPayoutInfo == null)
                {
                    return MakeErrorGetTransInfo(
                              Constants.PAYOUT_SERVICE_FAILURE_CODE,
                              Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                              + Messages.S_GetMessage("NoProviderFound"));
                }

                var interfaceName = providerPayoutInfo.GetConfiguration<string>(ConfigurationProviderKeys.InterfacePayout);
                if (string.IsNullOrEmpty(interfaceName))
                {
                    return MakeErrorGetTransInfo(
                              Constants.PAYOUT_SERVICE_FAILURE_CODE,
                              Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                              + Messages.S_GetMessage("NoProviderInterfaceFound"));
                }

                var providerInstance = providerFactory.GetInstance<ICorrespondentAPI>(interfaceName);
                providerInstance.SetProviderInfo(providerPayoutInfo);
                if (providerInstance == null)
                {
                    return MakeErrorGetTransInfo(
                              Constants.PAYOUT_SERVICE_FAILURE_CODE,
                              Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                              + Messages.S_GetMessage("CouldNotCreateProviderInstance"));
                }
                _logger.PublishInformation("Provider selected: " + providerPayoutInfo.Name);


                //GET THE ORDER DATA:
                _providerID = providerPayoutInfo.ProviderID;
                //Get the Transaction Info from Provider:
                GetTransactionInfoResponseModel responseData = new GetTransactionInfoResponseModel();
                try
                {
                    //Persistence (W1)
                    var persistenceEventGetTransactionInfoRequest = new PersistenceEventModel(0, _providerID, PersistenceEventType.PayoutPinRequestInfoRequest);//PersistenceID = 0,  to create new persistence record.
                    persistenceEventGetTransactionInfoRequest.SetContentObject<GetTransactionInfoRequestModel>(request);
                    persistenceEventGetTransactionInfoRequest.RequesterInfo = mappingHelper.Map<Contract.Models.RequesterInfoModel, Shared.Persistence.Model.RequesterInfoModel>(request.RequesterInfo);
                    persistenceHelper.CreatePersistenceEvent(persistenceEventGetTransactionInfoRequest); 
                    _persistenceID = persistenceEventGetTransactionInfoRequest.PersistenceID;
                    //End persistence (W1)

                    //Set provider and persistence ids
                    request.PersistenceID = _persistenceID;
                    request.ProviderID = _providerID;

                    responseData = providerInstance.GetTransactionInfo(request);
                    responseData.PersistenceID = _persistenceID;

                    responseData.ProviderInfo = new ProviderInfoModel()
                    {
                        ProviderID = providerPayoutInfo.ProviderID,
                        ProviderName = providerPayoutInfo.Name,
                        ProviderTypeID = providerPayoutInfo.ProviderTypeID
                    };
                    

                    //Persistence (W4)
                    var persistenceEventGetTransactionInfoResponse = new PersistenceEventModel(_persistenceID, _providerID, PersistenceEventType.PayoutPinRequestInfoResponse);
                    persistenceEventGetTransactionInfoResponse.SetContentObject<GetTransactionInfoResponseModel>(responseData);
                    persistenceHelper.CreatePersistenceEvent(persistenceEventGetTransactionInfoResponse);
                    //End persistence (W4) 

                }
                catch (Exception e)
                {
                    return MakeErrorGetTransInfo(
                        Constants.PNG_SERVICE_FAILURE_CODE,
                        Constants.PNG_SERVICE_FAILURE_MESSAGE +
                        e.Message);
                }

                //Check if order is not available and error code is undefined. Set message.
                if(responseData.ReturnInfo != null && !responseData.ReturnInfo.AvailableForPayout)
                {
                    if(responseData.ReturnInfo.ErrorCode ==(int) Contract.Enumerations.PayoutMessageCode.Undefined)
                    {
                        responseData.ReturnInfo.ErrorCode = (int)Contract.Enumerations.PayoutMessageCode.OrderNotAvailable;
                        responseData.ReturnInfo.ErrorMessage = Messages.S_GetMessage("OrderNoAvailable");
                    }

                    return responseData;                 
                }


                //Log response:
                string transInfosOutputs = "";
                transInfosOutputs +=
                    "PersistenceID=" + responseData.PersistenceID + ";" +
                    "OrderID=" + responseData.OrderID + ";" +
                    "TransferDate=" + responseData.TransferDate + ";" +
                    "TransferStatus=" + responseData.TransferStatus + ";" +
                    "SendAmount=" + responseData.SendAmount + ";" +
                    "PayoutAmount=" + responseData.PayoutAmount + ";" +
                    "ExchangeRate=" + responseData.ExchangeRate + ";" +
                    "Comission=" + responseData.Comission + ";" +
                    "ReceiverComission=" + responseData.ReceiverComission + ";" +
                    "SenderComission=" + responseData.SenderComission + ";" +
                    "FromAgentID=" + responseData.RecAgentID + ";" +
                    "CountryFrom=" + responseData.CountryFrom + ";" +
                    "CountryTo=" + responseData.CountryTo + ";" +
                    "PayDataMessage=" + responseData.PayDataMessage + ";" +
                    "SenderIsResident=" + responseData.SenderIsResident + ";" +
                    "ReceiverIsResident=" + responseData.ReceiverIsResident + ";" +
                    "SenderFullName=" + responseData.SenderInfo.Name + ";" +
                    "SenderFirstName=" + responseData.SenderInfo.FirstName + ";" +
                    "SenderMiddleName=" + responseData.SenderInfo.MiddleName + ";" +
                    "SenderLastName1=" + responseData.SenderInfo.LastName1 + ";" +
                    "SenderLastName2=" + responseData.SenderInfo.LastName2 + ";" +
                    "SenderAddress=" + responseData.SenderInfo.Address + ";" +
                    "SenderCity=" + responseData.SenderInfo.City + ";" +
                    "SenderState=" + responseData.SenderInfo.State + ";" +
                    "SenderCountry=" + responseData.SenderInfo.Country + ";" +
                    "SenderPhone=" + responseData.SenderInfo.PhoneNumber + ";" +
                    "SenderEmail=" + responseData.SenderInfo.EmailAddress + ";" +
                    "SenderIDTypeID=" + responseData.SenderInfo.IDTypeID + ";" +
                    "SenderIDTypeText=" + responseData.SenderInfo.IDType + ";" +
                    "SenderIDNumber=" + responseData.SenderInfo.IDNumber + ";" +
                    "SenderIDIssuer=" + responseData.SenderInfo.IDIssuedBy + ";" +
                    "SenderIDSerialNumber=" + responseData.SenderInfo.IDSerialNumber + ";" +
                    "ReceiverFullName=" + responseData.BeneficiaryInfo.Name + ";" +
                    "ReceiverFirstName=" + responseData.BeneficiaryInfo.FirstName + ";" +
                    "ReceiverMiddleName=" + responseData.BeneficiaryInfo.MiddleName + ";" +
                    "ReceiverLastName1=" + responseData.BeneficiaryInfo.LastName1 + ";" +
                    "ReceiverLastName2=" + responseData.BeneficiaryInfo.LastName2 + ";" +
                    "ReceiverAddress=" + responseData.BeneficiaryInfo.Address + ";" +
                    "ReceiverCity=" + responseData.BeneficiaryInfo.City + ";" +
                    "ReceiverState=" + responseData.BeneficiaryInfo.State + ";" +
                    "ReceiverCountry=" + responseData.BeneficiaryInfo.Country + ";" +
                    "ReceiverPhone=" + responseData.BeneficiaryInfo.PhoneNumber + ";" +
                    "ReceiverEmail=" + responseData.BeneficiaryInfo.EmailAddress + ";" +
                    "ReceiverIDTypeID=" + responseData.BeneficiaryInfo.IDTypeID + ";" +
                    "ReceiverIDTypeText=" + responseData.BeneficiaryInfo.IDType + ";" +
                    "ReceiverIDNumber=" + responseData.BeneficiaryInfo.IDNumber + ";" +
                    "ReceiverIDIssuer=" + responseData.BeneficiaryInfo.IDIssuer + ";" +
                    "ReceiverIDSerialNumber=" + responseData.BeneficiaryInfo.IDSerialNumber + ";" +
                    "PayDataNotAfterDate=" + responseData.PayDataNotAfterDate + ";" +
                    "PayDataNotAfterDateSpecified=" + responseData.PayDataNotAfterDateSpecified + ";" +
                    "PayDataNotBeforeDate=" + responseData.PayDataNotBeforeDate + ";" +
                    "PayDataNotBeforeDateSpecified=" + responseData.PayDataNotBeforeDateSpecified + ";" +
                    "OnLegalHold=" + responseData.OnLegalHold;
                _logger.PublishInformation("Returned MSG: " +
                    "Outputs: " + transInfosOutputs);

                //Return response;
              
                return responseData;
            }
            catch (Exception e)
            {
               return MakeErrorGetTransInfo(
                    Constants.PAYOUT_SERVICE_FAILURE_CODE,
                    Constants.PAYOUT_SERVICE_FAILURE_MESSAGE
                    + Messages.S_GetMessage("GetTransactionInfoFailed")
                    + e.Message);
            }
        }

        /// <summary>
        /// Create an Error Response.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="statusMessage"></param>
        /// <returns></returns>
        private static GetTransactionInfoResponseModel MakeErrorGetTransInfo(int statusCode, string statusMessage)
        {
          

            string errorMsg = statusCode + ":" + statusMessage;

            //Persistence (Error Persistence)
            var persistenceEventError = new PersistenceEventModel(_persistenceID, _providerID, PersistenceEventType.FaultException);
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

            return new GetTransactionInfoResponseModel(statusCode, statusMessage, _persistenceID);
        }



    }
}
