using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxAddress
    {
        public int fAddressID { get; set; }

        public int fEntityID { get; set; }

        public string fAddress { get; set; }

        public string fCounty { get; set; }

        public string fCity { get; set; }

        public string fState { get; set; }

        public int fCountryId { get; set; }
    }
}
