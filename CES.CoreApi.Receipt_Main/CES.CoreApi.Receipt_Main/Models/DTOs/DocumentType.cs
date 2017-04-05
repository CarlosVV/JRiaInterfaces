using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models.DTOs
{
    public enum DocumentType
    {
        //per conversation with Domingo: 
        //SalesTicketTaxIncluded = BoletaAfecta (tipo39) , NotadeCredito (tipo 61), NotadeDebito (tipo 56), Factura (tipo 33)
        SalesTicketTaxIncluded = 39,
        CreditNote = 61,
        DebitNote = 56,
        Invoice = 33
    }
}