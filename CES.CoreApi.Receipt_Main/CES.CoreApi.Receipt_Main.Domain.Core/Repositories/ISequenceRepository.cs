using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface ISequenceRepository
    {
        systblApp_CoreApi_Sequence find(string id);
        IEnumerable<systblApp_CoreApi_Sequence> find(Expression<Func<systblApp_CoreApi_Sequence, bool>> where);
        void CreateSequence(systblApp_CoreApi_Sequence obj);
        void UpdateSequence(systblApp_CoreApi_Sequence obj);
        void RemoveSequence(systblApp_CoreApi_Sequence obj);
        void SaveChanges();
    }
}
