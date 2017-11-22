using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface ISequenceService
    {
        /*List<actblTaxDocument_TableSeq> GetAllSequences();
        void CreateSequence(actblTaxDocument_TableSeq objectEntry);
        void UpdateSequence(actblTaxDocument_TableSeq objectEntry);
        void RemoveSequence(actblTaxDocument_TableSeq objectEntry);*/
        int GetSequence();
        //void SaveChanges();
    }
}
