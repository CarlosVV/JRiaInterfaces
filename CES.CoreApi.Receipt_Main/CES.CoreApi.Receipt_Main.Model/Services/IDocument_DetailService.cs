using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IDocument_DetailService
    {
        List<Document_Detail> GetAllDocument_Details();
        void CreateDocument_Detail(Document_Detail objectEntry);
        void UpdateDocument_Detail(Document_Detail objectEntry);
        void RemoveDocument_Detail(Document_Detail objectEntry);
        
    }
}
