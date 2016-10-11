using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class MoneyModel
    {

        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        /// <summary>
        /// CONSTRUCTOR:
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        public MoneyModel(decimal amt, string cur)
        {
            Amount = amt;
            CurrencyCode = cur;
        }

        public MoneyModel()
        {
            Amount = 0;
            CurrencyCode = string.Empty ;
        }


    }
}
