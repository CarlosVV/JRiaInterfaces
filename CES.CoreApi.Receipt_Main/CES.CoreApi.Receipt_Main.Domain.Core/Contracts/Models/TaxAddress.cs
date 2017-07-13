using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxAddress
    {
        public int fTaxAddressId { get; set; }

        public int? fTaxEntityId { get; set; }

        public string fAddress { get; set; }

        public string fComuna { get; set; }

        public string fCity { get; set; }

        public string fState { get; set; }

        public string fCountryId { get; set; }
       
    }
}
