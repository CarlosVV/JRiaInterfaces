using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System.Collections.Generic;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class DocumentSearchResult
    {
        public IEnumerable<TaxDocument> Results;

    }
}