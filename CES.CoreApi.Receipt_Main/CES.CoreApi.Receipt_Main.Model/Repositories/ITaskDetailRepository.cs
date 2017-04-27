using CES.CoreApi.Receipt_Main.Model.Documents;
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
        TaskDetail find(string id);
        IEnumerable<TaskDetail> find(Expression<Func<TaskDetail, bool>> where);
        void CreateTaskDetail(TaskDetail obj);
        void UpdateTaskDetail(TaskDetail obj);
        void RemoveTaskDetail(TaskDetail obj);
        void SaveChanges();
    }
}
