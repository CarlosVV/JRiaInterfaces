using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Shared.Providers.Helper.Model.Public;
using CES.CoreApi.Payout.Service.Business.Logic.Compliance;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;
using CES.CoreApi.Shared.Persistence.Model;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents.Ria
{
    public class RiaPayoutProcess : BaseProviderProcess, ICorrespondentAPI
    {

        private readonly IRiaRepository _repository;
        private readonly IPersistenceHelper _persistenceHelper;


        /// <summary>
        /// CONSTRUCTOR:
        /// </summary>
        /// <param name="repository"></param>
        public RiaPayoutProcess(IRiaRepository repository, IPersistenceHelper persistenceHelper)
        {
            _repository = repository;
            _persistenceHelper = persistenceHelper;
        }

        public void SetProviderInfo(ProviderModel providerInfo)
        {
            this.ProviderInfo = providerInfo;
        }

        
        /// <summary>
        /// Get Transaction Info Call:
        /// Main Call.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetTransactionInfoResponseModel GetTransactionInfo(GetTransactionInfoRequestModel request)
        {
            return GetOrderInfo(request);
        }

        /// <summary>
        /// Get Transaction Info Call:
        /// Ria DB portion.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        private GetTransactionInfoResponseModel GetOrderInfo(GetTransactionInfoRequestModel requestModel)
        {
            var response = _repository.GetOrderInfo(requestModel);
            if(response.ReturnInfo.ErrorCode == 0)
            {
                if(response.TransferStatus.Trim().ToLower()!= "available for payment")
                {
                    switch(response.TransferStatus.Trim().ToLower())
                    {
                        case "voided":
                            response.ReturnInfo.ErrorCode = 101;
                            response.ReturnInfo.ErrorMessage = "Order Voided";
                            break;
                        case "paid":
                            response.ReturnInfo.ErrorCode = 106;
                            response.ReturnInfo.ErrorMessage = "Order Paid";
                            break;
                        case "canceled":
                            response.ReturnInfo.ErrorCode = 102;
                            response.ReturnInfo.ErrorMessage = "Order Canceled";
                            break;
                        case "not released":
                            response.ReturnInfo.ErrorCode = 99;
                            response.ReturnInfo.ErrorMessage = "Order Not Released";
                            break;
                        case "on hold":
                            response.ReturnInfo.ErrorMessage = "Order On Hold";
                            response.ReturnInfo.ErrorCode = 105;
                            break;
                       
                    }
                }
            }

            response.PersistenceID = requestModel.PersistenceID;
            return response;
        }


        /// <summary>
        /// Validate Payout Data Call:
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PayoutTransactionResponseModel ValidatePayoutData(PayoutTransactionRequestModel request)
        {
            //Validate Transaction by calling External SP:
            var log = string.Empty;

            var response = _repository.PayoutConfirmExternalValidate(request, ref log);

            //(W22) 
            var persistenceEventValidatePayoutDataRequest = new PersistenceEventModel(request.PersistenceID, request.ProviderID, PersistenceEventType.ValidateOrderRequest);
            persistenceEventValidatePayoutDataRequest.SetContentObject<string>(log);
            _persistenceHelper.CreatePersistenceEvent(persistenceEventValidatePayoutDataRequest);


            //(W23) 
            var persistenceEvenValidatePayoutDataResponse = new PersistenceEventModel(request.PersistenceID, request.ProviderID, PersistenceEventType.ValidateOrderResponse);
            persistenceEvenValidatePayoutDataResponse.SetContentObject<PayoutTransactionResponseModel>(response);
            _persistenceHelper.CreatePersistenceEvent(persistenceEvenValidatePayoutDataResponse);

            return response;
        }

        /// <summary>
        /// Payout Transaction Call:
        /// Main Call
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PayoutTransactionResponseModel PayoutTransaction(PayoutTransactionRequestModel request)
        {
            //Call Complaince on transaction by calling Ria SP:
            //TODO: Implement.

            //Call the Provider for payout response:
            return PayoutOrder(request);
        }

        private PayoutTransactionResponseModel PayoutOrder(PayoutTransactionRequestModel requestModel)
        {
            PayoutTransactionResponseModel reponses = null;


            try
            {
                    reponses = _repository.SaveTransactionAsPaid(requestModel);                     
                    if (reponses.ReturnInfo.ErrorCode == (int)Contract.Enumerations.PayoutMessageCode.PaidSuccessful)
                    {
                      //TODO: What should happen here?
                    }
                
            }
            catch
            {
                reponses = new PayoutTransactionResponseModel(9999, "Internal Error", requestModel.PersistenceID);
            }
           
            return reponses;

       
        }

        public PayoutTransactionResponseModel PayoutComplianceCheck(PayoutTransactionRequestModel requestModel)
        {
            ComplianceService complianceSvs = new ComplianceService(_repository,_persistenceHelper);

            return complianceSvs.CompliancePayout(requestModel, true); ;
        }

        public bool WriteComplianceReviewIssues()
        {
            //TODO: Implement!
            return true;
        }

        public CreateOrderFromProviderDataResponse CreateOrderFromProviderData(PayoutTransactionRequestModel request)
        {
            var response = new CreateOrderFromProviderDataResponse() {TransactionID = request.OrderID };
            return response;
        }
        public ConfirmPayoutResponseModel ConfirmPayout(int persistenceID, string orderPin, string payDocID, DateTime payDocDate, int providerID)
        {
            return new ConfirmPayoutResponseModel() { TransactionStatusCode = 0, TransactionStatusMesage = string.Empty };
        }


        public PayoutTransactionResponseModel SaveTransactionAsPaid(PayoutTransactionRequestModel request)
        {
           
            _persistenceHelper.SetOrderToPersistence(request.OrderID, request.PersistenceID);
            return null;
        }

        public PayoutTransactionResponseModel PlaceOrderLegalHold(PayoutTransactionRequestModel request)
        {
            var placeLegaHoldRequest = new PlaceOnLegalHoldRequestModel()
            {
                AppID = ConfigSettings.CoreAPIAppID(),
                AppObjtectID = ConfigSettings.CoreAPIAppObjectID(),
                Modified = DateTime.Now,
                ServiceID = 111,
                StatusNote = "Watchlist CoreAPI Payout Hold",
                TransactionID = request.OrderID,
                UserID = 1
            };

            var reponse = new PayoutTransactionResponseModel(0, string.Empty, request.PersistenceID);

            try
            {
                ComplianceService complianceSvs = new ComplianceService(_repository, _persistenceHelper);

                var placeLegaHoldResponse = complianceSvs.PlacOrderLegalHold(placeLegaHoldRequest, request.PersistenceID, request.ProviderID);
                reponse = new PayoutTransactionResponseModel(placeLegaHoldResponse.ReturnValue, placeLegaHoldResponse.ReturnMessage, request.PersistenceID);

            }
            catch
            {
                reponse = new PayoutTransactionResponseModel(99, "Error trying to place order On Hold", request.PersistenceID);
            }
            finally
            {
                reponse.Commit = true;
            }



            return reponse;
        }
    }
}
