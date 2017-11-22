using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public static class DocumentTypeHelper
    {
        public static List<DocumentType> GetDocumenTypes()
        {
            var DocumentTypeList = new List<DocumentType>();
            DocumentTypeList.Add(new DocumentType { Code = "39", Description = "Boleta" });
            DocumentTypeList.Add(new DocumentType { Code = "33", Description = "Factura" });
            DocumentTypeList.Add(new DocumentType { Code = "61", Description = "Nota de Credito" });
            DocumentTypeList.Add(new DocumentType { Code = "56", Description = "Nota de Debito" });
            return DocumentTypeList;

        }
    }
}
