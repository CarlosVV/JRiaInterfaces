using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ITaskStatusRepository
    {
        TaskStatus find(string id);
        IEnumerable<TaskStatus> find(Expression<Func<TaskStatus, bool>> where);
        void CreateTaskStatus(TaskStatus obj);
        void UpdateTaskStatus(TaskStatus obj);
        void RemoveTaskStatus(TaskStatus obj);
        void SaveChanges();
    }
}
