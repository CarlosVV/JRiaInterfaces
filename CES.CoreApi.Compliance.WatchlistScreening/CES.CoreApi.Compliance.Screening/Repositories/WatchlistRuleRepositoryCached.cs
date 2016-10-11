using CES.Caching;
using CES.CoreApi.Compliance.Screening.Models;
using System.Collections.Generic;

namespace CES.CoreApi.Compliance.Screening.Repositories
{
	public class WatchlistRuleRepositoryCached : WatchlistRuleRepository
    {
        public override IEnumerable<Rule> GetAllScreeningRules(string countryFrom, string countryTo)
        {
            // check if data in cache is found
            var rules = Cache.Get("WatchlistRule_Cached", 
                () => {
                    return base.GetAllScreeningRules(countryFrom, countryTo);
                });

            return rules;
        }
    }
}