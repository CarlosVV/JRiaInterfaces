using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ISequenceService
    {
        List<systblApp_CoreApi_Sequence> GetAllSequences();
        void CreateSequence(systblApp_CoreApi_Sequence objectEntry);
        void UpdateSequence(systblApp_CoreApi_Sequence objectEntry);
        void RemoveSequence(systblApp_CoreApi_Sequence objectEntry);
        void SaveChanges();
    }
}
