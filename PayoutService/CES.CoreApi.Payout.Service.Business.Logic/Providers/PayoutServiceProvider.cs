using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Providers.Helper.Interfaces;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using AutoMapper;
using System.Transactions;
using CES.CoreApi.Payout.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers
{
    public class PayoutServiceProvider : IPayoutServiceProvider
    {
        #region Core

        private readonly IPayoutServiceProviderFactory _providerFactory;
        private readonly IProviderHelper _providerHelper;
        private readonly IPersistenceHelper _persistenceHelper;
        private readonly IMapper _mappingHelper;
        private readonly IEmailHelper _emailHelper;
        private readonly Log4NetProxy _logger = new Log4NetProxy();


        /// <summary>
        /// CONSTRUCTOR:
        /// </summary>
        /// <param name="providerFactory"></param>
        /// <param name="providerHelper"></param>
        public PayoutServiceProvider(IPayoutServiceProviderFactory providerFactory, IProviderHelper providerHelper, IPersistenceHelper persistenceHelper, IMapper mappingHelper, IEmailHelper emailHelper)
        {
            //TODO: Is some form of this check needed?
            //if (responseFactory == null)
            //    throw new CoreApiException(TechnicalSubSystem.ComplianceService,
            //      SubSystemError.GeneralRequiredParameterIsUndefined, "responseFactory");

            _providerFactory = providerFactory;
            _providerHelper = providerHelper;
            _persistenceHelper = persistenceHelper;
            _mappingHelper = mappingHelper;
            _emailHelper = emailHelper;
        }

        #endregion



        /// <summary>
        /// Main Method to Process a GetTransactionInfo call for all providers.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetTransactionInfoResponseModel GetTransactionInfo(GetTransactionInfoRequestModel request)
        {
           return GetTransactionInfoCall.S_ProcessGetTransactionInfo(_providerFactory, _providerHelper, _persistenceHelper, _mappingHelper, request);
        }

        /// <summary>
        /// Main Method to Process a PayoutTransaction call for all providers.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PayoutTransactionResponseModel PayoutTransaction(PayoutTransactionRequestModel request)
        {
            //TODO: Review use of transaction and how we can best take advanage of it without causing problems with data in DB and on other provider's servers.
            //only if everything is OK confirm the transaction
            using (var tx = new TransactionScope())
            {
                var response = PayoutTransactionCall.S_ProcessPayoutTransaction(_providerFactory, _providerHelper, _persistenceHelper, _mappingHelper,_emailHelper, request);

                if (response.ReturnInfo.ErrorCode == (int)PayoutMessageCode.PaidSuccessful || response.Commit)
                {
                    _logger.PublishInformation("Commit transaction...");
                    tx.Complete();
                }
                else
                {
                    _logger.PublishInformation("Rollback transaction...");
                }
                
                if (response.ReturnInfo.ErrorCode != (int)PayoutMessageCode.PaidSuccessful)
                {
                    //Persistence (W14)
                    var persistenceEventPayoutTransactionResponse = new PersistenceEventModel(request.PersistenceID, request.ProviderID, PersistenceEventType.PayoutResponse);
                    persistenceEventPayoutTransactionResponse.SetContentObject<PayoutTransactionResponseModel>(response);
                    _persistenceHelper.CreatePersistenceEvent(persistenceEventPayoutTransactionResponse);
                    //End persistence (W14) 
                }
                return response;
            }
            //TODO: I am getting an error that .14 DB cannot process the transaction when it fails midway.

            //return PayoutTransactionCall.S_ProcessPayoutTransaction(_providerFactory, _providerHelper, _persistenceHelper, _mappingHelper, request);
        }


    }
}
