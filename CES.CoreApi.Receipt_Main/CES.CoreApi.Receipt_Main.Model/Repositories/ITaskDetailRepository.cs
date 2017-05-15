using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ITaskDetailRepository
    {
        systblApp_CoreAPI_TaskDetail find(string id);
        IEnumerable<systblApp_CoreAPI_TaskDetail> find(Expression<Func<systblApp_CoreAPI_TaskDetail, bool>> where);
        void CreateTaskDetail(systblApp_CoreAPI_TaskDetail obj);
        void UpdateTaskDetail(systblApp_CoreAPI_TaskDetail obj);
        void RemoveTaskDetail(systblApp_CoreAPI_TaskDetail obj);
        void SaveChanges();
    }
}
