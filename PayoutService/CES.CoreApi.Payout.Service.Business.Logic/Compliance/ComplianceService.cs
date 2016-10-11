
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Persistence.Interfaces;
using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Compliance
{
        
    /// <summary>
    /// This class is meant to serve as a placeholder for the Compliance functions that will later be in
    /// the separate Compliance Service.
    /// </summary>
    class ComplianceService
    {
        private static readonly Log4NetProxy _logger = new Log4NetProxy();
        private readonly IRiaRepository _repository;
        private static IPersistenceHelper _persistenceHelper;


        //These method calls are planned for the Compliance service, so use them here as the method set:
        //- CompliancePayout
        //- ComplianceSend
        //- WLFCheck
        //- AMLCheck
        //- ReviewIssueCreate
        //- ReviewIssueClear

        /// <summary>
        /// CONSTRUCTOR:
        /// </summary>
        /// <param name="repository"></param>
        public ComplianceService(IRiaRepository repository, IPersistenceHelper persistenceHelper)
        {
            _repository = repository;
            _persistenceHelper = persistenceHelper;
        }


        public PayoutTransactionResponseModel CompliancePayout(PayoutTransactionRequestModel requestModel, bool isExternalProvider)
        {
           
            //TODO Revied Issues
            var issueReview = ReviewIssues(requestModel);
            PayoutTransactionResponseModel response =null;
            switch (issueReview)
            {
                case 1:
                    response = new PayoutTransactionResponseModel(800, "Unreviewed issues", requestModel.PersistenceID);
                    response.ReviewIssuesStatus = issueReview;
                    return response;
                case 2:
                    response = new PayoutTransactionResponseModel(0, string.Empty, requestModel.PersistenceID);
                    response.ReviewIssuesStatus = issueReview;
                    return response;
                default:
                    response = new PayoutTransactionResponseModel(0, string.Empty, requestModel.PersistenceID);
                    response.ReviewIssuesStatus = issueReview;
                    // - Compliance Instructions SP [determines if compliance is on Ria or Actimize system or both]
                    ComplianceProviderResponseModel compResp = _repository.GetComplianceProvider(requestModel);
                    if (compResp.UseRiaCompliance)
                    {

                        requestModel.Persistence = _persistenceHelper.GetPersistence(requestModel.PersistenceID);
                        _logger.PublishInformation("Compliance on Ria Systems:");

                        string log = string.Empty;
                        // - mt_sp_Payout_Confirm_Compliance_WatchList SP
                        response = _repository.PayoutConfirmComplianceWatchlist(requestModel,ref log);
                        response.PersistenceID = requestModel.PersistenceID;


                        //(W16) 
                        var persistenceEventWLFlRequest = new PersistenceEventModel(requestModel.PersistenceID, requestModel.ProviderID, PersistenceEventType.ComplianceWLFRequest);
                        persistenceEventWLFlRequest.SetContentObject<string>(log);
                        _persistenceHelper.CreatePersistenceEvent(persistenceEventWLFlRequest);


                        //(W17) 
                        var persistenceEventWLFlResponse = new PersistenceEventModel(requestModel.PersistenceID, requestModel.ProviderID, PersistenceEventType.ComplianceWLFResponse);
                        persistenceEventWLFlResponse.SetContentObject<PayoutTransactionResponseModel>(response);
                        _persistenceHelper.CreatePersistenceEvent(persistenceEventWLFlResponse);

                        // - AML SP (anti money laundering)



                    }
                    else if (compResp.UseActimizeCompliance)
                    {
                        //TODO: Add call to actimize:
                        _logger.PublishInformation("Compliance will be run on Actimize:");
                        _logger.PublishInformation("Actimize not currently implemented: No actions will be taken");
                    }
                    else
                    {
                        _logger.PublishInformation("No Compliance set to run on this transaction.");
                    }


                    return response;
                   
            } 

        }

        /// <summary>
        /// Write Compliance Issue:
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="issueDesc"></param>
        private void WriteComplianceIssue(PayoutTransactionRequestModel requestModel, string issueDesc)
        {
            //Currently Not writing any issues:
            //_repository.ComplianceWriteIssue(requestModel, issueDesc);
        }

        private int ReviewIssues(PayoutTransactionRequestModel requestModel)
        {
            var orderIssues = _repository.ComplianceListIssues(requestModel).ToList();

            //20  Closed - No Action
            //40  Closed - Action Taken            
            var statusToRelease = new List<int>() {20, 40};            

            //New 
            if(orderIssues.Count==0)
            {
                return 0;
            }

            //some open isues
            if (orderIssues.Count(i => statusToRelease.Contains(i.StatusID)) < orderIssues.Count)
            {
                return 1;
            }

            //all isues open
            if (orderIssues.Count(i => statusToRelease.Contains(i.StatusID)) == orderIssues.Count)
            {
                return 2;
            }

            return -1; //error
          
        }

        public PlaceOnLegalHoldResponseModel PlacOrderLegalHold(PlaceOnLegalHoldRequestModel requestModel, int perstenceID, int providerID)
        {
            var log = string.Empty;
            var placeLegaHoldResponse = _repository.PlaceLegalHoldPayout(requestModel,ref log);
            //(W20) 
            var persistenceEventPlaceOnLegalHoldRequestModel = new PersistenceEventModel(perstenceID, providerID, PersistenceEventType.PlaceOrderOnHoldRequest);
            persistenceEventPlaceOnLegalHoldRequestModel.SetContentObject<string>(log);
            _persistenceHelper.CreatePersistenceEvent(persistenceEventPlaceOnLegalHoldRequestModel);


            if (placeLegaHoldResponse.ReturnValue == 1)
            {
                placeLegaHoldResponse.ReturnValue = 104;
                placeLegaHoldResponse.ReturnMessage = requestModel.StatusNote;             

            }
           
            //(W21) 
            var persistenceEventPlaceOnLegalHoldResponseModel = new PersistenceEventModel(perstenceID, providerID, PersistenceEventType.PlaceOrderOnHoldResponse);
            persistenceEventPlaceOnLegalHoldResponseModel.SetContentObject<PlaceOnLegalHoldResponseModel>(placeLegaHoldResponse);
            _persistenceHelper.CreatePersistenceEvent(persistenceEventPlaceOnLegalHoldResponseModel);

            return placeLegaHoldResponse;
        }

    }
}
