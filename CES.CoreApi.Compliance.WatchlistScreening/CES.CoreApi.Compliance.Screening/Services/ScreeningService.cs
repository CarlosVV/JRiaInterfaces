using System.Collections.Generic;
using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.Repositories;
using CES.CoreApi.Compliance.Screening.Providers;
using System.Linq;
using System;
using CES.CoreApi.Compliance.Screening.Models.DTO;
using CES.CoreApi.Compliance.Screening.Utilities;
using Newtonsoft.Json;

namespace CES.CoreApi.Compliance.Screening.Services
{
	/// <summary>
	/// 
	/// </summary>
	public class ScreeningService
    {
        private WatchlistRuleRepository _repository = null;
        private WatchlistRuleRepositoryCached _repositoryCached = null;
        private WatchlistScreeningProvider _provider = null;

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="provider"></param>
        public ScreeningService(WatchlistRuleRepository repository = null, WatchlistScreeningProvider provider = null)
        {
            _repository = repository == null ? new WatchlistRuleRepository() : repository;
            _repositoryCached = _repositoryCached == null ? new WatchlistRuleRepositoryCached() : _repositoryCached;
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
            int returnCode = 0;
            List<Rule> rules = null;

            Logging.Log.Info($"Reviewing parties. ({request.Parties.Count()})");
            foreach (var party in request.Parties)
            {
                Logging.Log.Info("Get Rules from Database.");

                //Get EntryTypeID
                var entryTypes = _repositoryCached.GetEntryTypes();
                if(entryTypes.Any())
                {
                    var entryType = entryTypes.FirstOrDefault(e => e.fName == request.EntryType);
                    request.EntryTypeId = (entryType == null ? 0:entryType.fKey2);                   
                }
              
                rules = _repository.GetScreeningRulesForTransaction(request, party.Type, 201).ToList(); //201: Actimize            

                if (rules != null && rules.Any())
                {
                    Logging.Log.Info($"Party {party.Type} has rules. ({rules.Count()})");
                    Logging.Log.Info(JsonConvert.SerializeObject(rules));

                    if (AppSettings.UseDefaultRules)
                    {
                        Logging.Log.Info($"UseDefaultRules (web.config) is true.");
                        rules = GetDefaultRule(request, party.Type);
                    }
                }
                else
                {
                    Logging.Log.Info($"No rules found for {party.Type}");

                    rules = GetDefaultRule(request, party.Type);
                   
                    //Send Email
                    Logging.Log.Info($"Send Email");
                    var sendEMailRequest = GetEmail(request, party.Type);
                    var sendEMailResponse = _repository.SendMail(sendEMailRequest);
                    string json = JsonConvert.SerializeObject(sendEMailRequest);
                    Logging.Log.Info($"{json}. MessageID: { sendEMailResponse.ReturnMessageID }");

                }

                
                foreach (var rule in rules)
                {
                    Logging.Log.Info($"Call Actimize Service for party {party.Type} and rule {rule.fRuleID} .");
                    var result = _provider.AllowTransaction(request, party, rule);
                    status = result.Message;
                    returnCode = result.ReturnCode??0;
                    Logging.Log.Info($"Satus: {status}. ReturnCode: {returnCode}");

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
                                    AlertId = result.AlertId
                                });
                            }
                        }
                    }
                }
            }


            if (partyRuleHits.Count == 0)
            {
                
                var riaCode = 0;
                var riaMessage = "Order has not hit";
                Logging.Log.Info(riaMessage);
                switch (returnCode)
                {
                    case 0:
                        riaCode = 1;
                        break;
                    case 4:
                        riaCode = 60;
                        riaMessage = "Transaction was not successfully processed";
                        break;
                    default:
                        riaCode = 61;
                        riaMessage = "Undefined error";
                        break;

                }
                return new Response(riaCode, riaMessage, null, false, new Rule() { fActionID = 0 }, status);
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
                    ProviderAlertID= partyHit.AlertId??string.Empty,
                    ProviderAlertStatusID = string.Empty,
                };

               
                Logging.Log.Info($"Create ReviewAlert. AlertId: {partyHit.AlertId}");
                var result = _repository.CreateReviewAlert(createReviewIssueRequest);
                if(result.RetVal ==1)
                {
                    Logging.Log.Info($"ReviewAlert created  OK. ReviewIssueID: {result.reviewIssueID}");
                }
                else
                {
                    Logging.Log.Info($"Error. RetVal:{result.RetVal} RetMsg:{result.RetMsg}. ReviewIssueID: {result.reviewIssueID}");
                    return new Response(0, result.RetMsg, null, false, new Rule() { fActionID = 0 }, status);
                }
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

        private static SendEmailRequest GetEmail(Request request, PartyType partyType)
        {
            var sendEmailRequest = new SendEmailRequest()
            {
                Message = $"There were no Actimize rules found for order {request.OrderNo} - party {partyType}",
                MessageType = "Actimize rules not found",
                MessageFormat = "Text/HTML",
                MessageFrom = AppSettings.MailFrom,
                MessageTo = AppSettings.MailTo,
                MessageCc = string.Empty,
                MessageBcc = string.Empty,
                MessageSubject = $"Order {request.OrderNo} - party {partyType} did not match any Actimize screening rules.",
                MessageSendMethod = "Email",
                UserNameID = 1,
                ProviderID = 201,
            };

            return sendEmailRequest;
        }
        private List<Rule> GetDefaultRule(Request request, PartyType partyType)
        {
            string[] defaultRule = new string[3];
            switch (partyType)
            {
                case PartyType.Customer:
                    defaultRule = AppSettings.DefaultRuleCustomer.Split("|".ToCharArray());
                    Logging.Log.Info($"Default Rules for {partyType}: {AppSettings.DefaultRuleCustomer}");
                    break;
                case PartyType.Beneficiary:
                    defaultRule = AppSettings.DefaultRuleBeneficiary.Split("|".ToCharArray());
                    Logging.Log.Info($"Default Rules for {partyType}: {AppSettings.DefaultRuleBeneficiary}");
                    break;
                case PartyType.OnBehalfOf:
                    defaultRule = AppSettings.DefaultRuleOnBehalf.Split("|".ToCharArray());
                    Logging.Log.Info($"Default Rules for {partyType}: {AppSettings.DefaultRuleOnBehalf}");
                    break;
            }


            var rules = new List<Rule>() {
                        new Rule() {
                        ProviderID = 201,
                        ProviderName = "Actimize",
                        fActionID = int.Parse(defaultRule[2]),
                        CountryTo =request.CountryTo,
                        ContryFrom = request.CountryFrom,
                        fNameTypeID = partyType,
                        SearchDef = defaultRule[0],
                        BusinessUnit = defaultRule[1],
                        fIssueItemID = int.Parse(defaultRule[3]),

                    }};

            return rules;

        }
    }
}