using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface ITaskDetailRepository
    {
        systblApp_CoreAPI_Task_Detail find(string id);
        IEnumerable<systblApp_CoreAPI_Task_Detail> find(Expression<Func<systblApp_CoreAPI_Task_Detail, bool>> where);
        void CreateTaskDetail(systblApp_CoreAPI_Task_Detail obj);
        void UpdateTaskDetail(systblApp_CoreAPI_Task_Detail obj);
        void RemoveTaskDetail(systblApp_CoreAPI_Task_Detail obj);
        void SaveChanges();
    }
}
