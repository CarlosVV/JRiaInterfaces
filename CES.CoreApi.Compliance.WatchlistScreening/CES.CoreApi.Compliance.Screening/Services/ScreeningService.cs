using System.Collections.Generic;
using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.Repositories;
using CES.CoreApi.Compliance.Screening.Providers;
using System.Linq;
using System;
using CES.CoreApi.Compliance.Screening.Models.DTO;

namespace CES.CoreApi.Compliance.Screening.Services
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreeningService
    {
        private WatchlistRuleRepository _repository = null;
        private WatchlistScreeningProvider _provider = null;

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="provider"></param>
        public ScreeningService(WatchlistRuleRepository repository = null, WatchlistScreeningProvider provider = null)
        {
            _repository = repository == null ? new WatchlistRuleRepository() : repository;
            _provider = provider == null ? new WatchlistScreeningProvider() : provider;
        }

		//FOR DEVS NOTE AND TEST ONLY 
		public ActimizeResponse PingActmize(string originatorName, string originatorCountryCd)
		{
			return _provider.GetMsRealTimeWsProviderOnePing(originatorName, originatorCountryCd);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual Response ScreeningTransaction(Request request)
        {
            Logging.Log.Info("Call ScreeningTransaction.");
            var partyRuleHits = new List<PartyRuleHits>();
            var stbMessage = new System.Text.StringBuilder();
            var status = "";

            Logging.Log.Info($"Reviewing parties. ({request.Parties.Count()})");
            foreach (var party in request.Parties)
            {
                Logging.Log.Info("Get Rules from Database.");
                var rules = _repository.GetScreeningRulesForTransaction(request, party.Type, 201); //201: Actimize

                if (rules != null && rules.Any())
                {
                    Logging.Log.Info($"Party {party.Type} has rules. ({rules.Count()})");
                }
                else
                {
                    Logging.Log.Info("No rules.");
                }

                foreach (var rule in rules)
                {
                    Logging.Log.Info("Review Rules");
                    Logging.Log.Info($"Call Actimize Service for party {party.Type} and rule {rule.fRuleID} .");
                    var result = _provider.AllowTransaction(request, party, rule);
                    status = result.Message;
                    Logging.Log.Info($"Satus: {status}.");

                    if (result.IsAlerted)
                    {
                        Logging.Log.Info($"Is Alerted");
                        if (result.HasHits)
                        {
                            Logging.Log.Info($"Has Hits ({result.Hits.Count})");
                            if (result.Hits != null && result.Hits.Count > 0)
                            {
                                partyRuleHits.Add(new PartyRuleHits()
                                {
                                    Code = result.ReturnCode,
                                    Status = result.Message,
                                    Party = party,
                                    Rule = rule,
                                    Hits = result.Hits,
                                    AlertId = "WLF101-8901-547",//TODO: result.AlertId
                                });
                            }
                        }
                    }
                }
            }


            if (partyRuleHits.Count == 0)
            {
                Logging.Log.Info($"Order has not hit");
                return new Response(0, "Order has not hit", null, false, new Rule() { fActionID = 0 }, status);
            }

            var partyRuleHit = partyRuleHits.OrderBy(r => r.Rule.fActionID)
                                            .OrderBy(p => p.Party.Type)
                                            .GroupBy(r => r.Rule.fActionID)
                                            .FirstOrDefault().FirstOrDefault();



            if (partyRuleHit == null)
            {
                Logging.Log.Info($"Order has not hit");
                return new Response(0, "Order has not hit", null, false, new Rule() { fActionID = 0 }, status);
            }

            var partiesHitsGroups = partyRuleHits.OrderBy(r => r.Rule.fActionID)
                                            .OrderBy(p => p.Party.Type)
                                            .GroupBy(r => r.Rule.fActionID)
                                            .FirstOrDefault();

            stbMessage = new System.Text.StringBuilder();
            foreach (var partyHit in partiesHitsGroups)
            {
                foreach (var hit in partyHit.Hits)
                {
                    foreach (var categoryName in hit.categoriesNames)
                    {
                        stbMessage.Append($"{categoryName}; ");
                    }

                }

                var createReviewIssueRequest = new ReviewAlertCreateRequest()
                {
                    DateTime = DateTime.Now,
                    ServiceID = (int)request.ServiceId,
                    TransactionID = request.OrderId,
                    IssueID = partyHit.Rule.fIssueItemID,
                    IssueDescription = $"{partyHit.Party.Type } Hit Actimize {stbMessage.ToString()}",
                    FilterID = null,
                    IssueTypeID = 15,
                    ActionID = partyHit.Rule.fActionID,
                    ProviderID = 201,
                    ProviderAlertID= partyHit.AlertId,
                    ProviderAlertStatusID = string.Empty,
                };

                //TODO: Create Issue
                Logging.Log.Info($"Create ReviewAlert");
               _repository.CreateReviewAlert(createReviewIssueRequest);
            }

            
            stbMessage = new System.Text.StringBuilder();
            stbMessage.Append($"Order has Hit(s) in { partyRuleHit.Party.Type}. ");
      
            foreach (var hit in partyRuleHit.Hits)
            {
                foreach (var categoryName in hit.categoriesNames)
                {
                    stbMessage.Append($"{categoryName}; ");
                }
                
            }

            var message = stbMessage.ToString().Trim().Remove(stbMessage.ToString().Trim().Length - 1, 1);
            Logging.Log.Info(message);

            return  new Response(partyRuleHit.Code ?? 0, message, partyRuleHit.Hits, true, partyRuleHit.Rule, partyRuleHit.Status);

            
        }
    }
}