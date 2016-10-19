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
    public class RulesService
    {
        private WatchlistRuleRepository _repository = null;
        private WatchlistRuleRepositoryCached _repositoryCached = null;
        private WatchlistScreeningProvider _provider = null;

        public RulesService(WatchlistRuleRepository repository = null, WatchlistScreeningProvider provider = null)
        {
            _repository = repository == null ? new WatchlistRuleRepository() : repository;
            _repositoryCached = _repositoryCached == null ? new WatchlistRuleRepositoryCached() : _repositoryCached;
            _provider = provider == null ? new WatchlistScreeningProvider() : provider;
        }


        public virtual RulesResponse GetRules(RulesRequest request)
        {
            //Get EntryTypeID
            var entryTypes = _repositoryCached.GetEntryTypes();
            if (entryTypes.Any())
            {
                var entryType = entryTypes.FirstOrDefault(e => e.fName == request.EntryType);
                request.EntryTypeId = (entryType == null ? 0 : entryType.fKey2);
            }

            var rules = _repository.GetScreeningRulesForTransaction(request);
            throw new NotImplementedException();
        }

        public virtual Rule GetRule(int Id)
        {

            throw new NotImplementedException();
        }
    }
}