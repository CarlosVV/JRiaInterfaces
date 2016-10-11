using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class ConfirmPayoutResponseModel
    {
        public int PersistenceID { get; set; }
        public DateTime Date { get; set; }
        public string ConfirmationNumber { get; set; }
        public int TransactionStatusCode { get; set; }
        public string TransactionStatusMesage { get; set; }

    }
}
