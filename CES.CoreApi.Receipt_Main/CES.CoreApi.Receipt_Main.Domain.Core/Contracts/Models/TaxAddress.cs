using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxAddress
    {
        public int Id { get; set; }

        public int? TaxEntityId { get; set; }

        public string Address { get; set; }

        public string Comuna { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string CountryId { get; set; }
       
    }
}
