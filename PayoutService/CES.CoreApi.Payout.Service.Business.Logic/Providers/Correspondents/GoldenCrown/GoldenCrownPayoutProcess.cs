using System;
using AutoMapper;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Providers.Helper.Model.Public;
using CES.CoreApi.Shared.Persistence.Interfaces;

using CES.CoreApi.Payout.Service.Business.Logic.Compliance;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;
using CES.CoreApi.Shared.Persistence.Model;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents.GoldenCrown
{
    public class GoldenCrownPayoutProcess : BaseProviderProcess, ICorrespondentAPI
    {
        public const int CANCELED = 0;
        public const int NOT_PAID_1 = 1;
        public const int NOT_PAID_2 = 2;
        public const int NOT_PAID_3 = 3;
        public const int READY_FOR_PAYOUT = 4;
        public const int REQUESTED_FOR_PAYOUT = 5;
        public const int REFUNDED_6 = 6;
        public const int PAID_OUT = 7;
        public const int REFUNDED_8 = 8;
        public const int ORDER_EXPIRED = -1;

        public const string CANCELED_DESC = "Canceled";
        public const string NOT_PAID_1_DESC = "Paid";
        public const string NOT_PAID_2_DESC = "Paid";
        public const string NOT_PAID_3_DESC = "Paid";
        public const string READY_FOR_PAYOUT_DESC = "Ready for Payout";
        public const string REQUESTED_FOR_PAYOUT_DESC = "Requested for Payout";
        public const string REFUNDED_6_DESC = "Refunded";
        public const string PAID_OUT_DESC = "Paid Out";
        public const string REFUNDED_8_DESC = "Refunded";
        public const string ORDER_EXPIRED_DESC = "Expired";
        public const string OUTDATED_EXPIRED_TAG = ".1";// This will be added to the status if the order is expired. Ex: 4.1

        private readonly IDataHelper _dataHelper;
        private readonly IRiaRepository _repository;
        private readonly IPersistenceHelper _persistenceHelper;
        private readonly IMapper _mappingHelper;


        /// <summary>
        /// CONSTRUCTOR:
        /// </summary>
        /// <param name="dataHelper"></param>
        /// <param name="repository"></param>
        public GoldenCrownPayoutProcess(IDataHelper dataHelper, IRiaRepository repository, IPersistenceHelper persistenceHelper, IMapper mappingHelper)
        {
            _dataHelper = dataHelper;
            _repository = repository;
            _persistenceHelper = persistenceHelper;
            _mappingHelper = mappingHelper;

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
            //Call Golden Crown for transaction info:
            GetTransactionInfoResponseModel gcResp = 
                new GoldenCrownAPI(
                    ProviderInfo,
                    _dataHelper, 
                    _persistenceHelper, 
                    _mappingHelper).
                GetTransactionInfo(request);
            
            //Take that GC response and pass it on to the Ria DB for a response:
            string xmlReqFields = this.ProviderInfo.GetXmlConfigRequiredFields();

            var response = _repository.GetOrderInfoExternal(request, gcResp, xmlReqFields);


            if (response.ReturnInfo.ErrorCode == 0)
            {
                if (gcResp.ReturnInfo.ErrorCode != READY_FOR_PAYOUT)
                {
                    switch (gcResp.ReturnInfo.ErrorCode)
                    {
                        case NOT_PAID_1:
                            response.ReturnInfo.ErrorCode = NOT_PAID_1;
                            response.ReturnInfo.ErrorMessage = NOT_PAID_1_DESC;
                            break;
                        case NOT_PAID_2:
                            response.ReturnInfo.ErrorCode = NOT_PAID_2;
                            response.ReturnInfo.ErrorMessage = NOT_PAID_2_DESC;
                            break;
                        case NOT_PAID_3:
                            response.ReturnInfo.ErrorCode = NOT_PAID_3;
                            response.ReturnInfo.ErrorMessage = NOT_PAID_2_DESC;
                            break;
                        case REQUESTED_FOR_PAYOUT:
                            response.ReturnInfo.ErrorCode = REQUESTED_FOR_PAYOUT;
                            response.ReturnInfo.ErrorMessage = REQUESTED_FOR_PAYOUT_DESC;
                            break;
                        case REFUNDED_6:
                            response.ReturnInfo.ErrorCode = REFUNDED_6;
                            response.ReturnInfo.ErrorMessage = REFUNDED_6_DESC;
                            break;
                        case PAID_OUT:
                            response.ReturnInfo.ErrorCode = PAID_OUT;
                            response.ReturnInfo.ErrorMessage = PAID_OUT_DESC;
                            break;        
                        case REFUNDED_8:
                            response.ReturnInfo.ErrorCode = REFUNDED_8;
                            response.ReturnInfo.ErrorMessage = REFUNDED_8_DESC;
                            break;
                        case ORDER_EXPIRED:
                            response.ReturnInfo.ErrorCode = ORDER_EXPIRED;
                            response.ReturnInfo.ErrorMessage = ORDER_EXPIRED_DESC;
                            break;
                      

                    }
                }
            }
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
            response.PersistenceID = request.PersistenceID;
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
        /// Payout Transaction with Provider service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PayoutTransactionResponseModel PayoutTransaction(PayoutTransactionRequestModel request)
        {
            //Call the Provider for payout response:
            return new GoldenCrownAPI(ProviderInfo, _dataHelper, _persistenceHelper, _mappingHelper).PayoutTransaction(request);
        }
        
        /// <summary>
        /// Payout Compliance
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public PayoutTransactionResponseModel PayoutComplianceCheck(PayoutTransactionRequestModel requestModel)
        {
            ComplianceService complianceSvs = new ComplianceService(_repository,_persistenceHelper);
                      
            return complianceSvs.CompliancePayout(requestModel, true); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WriteComplianceReviewIssues()
        {
            //TODO: Implement!
            //ComplianceService complianceSvs = new ComplianceService(_repository);
            //complianceSvs.writeIssue(requestModel);
            return true;
        }

        /// <summary>
        /// Create an order record in Ria DB for provider order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreateOrderFromProviderDataResponse CreateOrderFromProviderData(PayoutTransactionRequestModel request)
        {
            return _repository.OrderCreateFromProviderData(request);
        }

        /// <summary>
        /// Confirm Payout with Provider service
        /// </summary>
        /// <param name="orderPin"></param>
        /// <param name="payDocID"></param>
        /// <param name="payDocDate"></param>
        /// <returns></returns>
        public ConfirmPayoutResponseModel ConfirmPayout(int persistenceID, string orderPin, string payDocID, DateTime payDocDate, int providerID)
        {
            return new GoldenCrownAPI(ProviderInfo, _dataHelper, _persistenceHelper, _mappingHelper).ConfirmPayout(persistenceID, orderPin, payDocID, payDocDate, providerID);
        }

        /// <summary>
        /// Save Transaction as paid to the Ria DB.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PayoutTransactionResponseModel SaveTransactionAsPaid(PayoutTransactionRequestModel request)
        {
            _persistenceHelper.SetOrderToPersistence(request.OrderID, request.PersistenceID);
            return _repository.SaveTransactionAsPaid(request);
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
