using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IDocument_TypeService
    {
        List<Document_Type> GetAllDocument_Types();
        void CreateDocument_Type(Document_Type objectEntry);
        void UpdateDocument_Type(Document_Type objectEntry);
        void RemoveDocument_Type(Document_Type objectEntry);
        
    }
}
