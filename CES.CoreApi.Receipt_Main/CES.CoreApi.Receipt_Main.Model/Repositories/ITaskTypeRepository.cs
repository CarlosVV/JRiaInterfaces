using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ITaskTypeRepository
    {
        TaskType find(string id);
        IEnumerable<TaskType> find(Expression<Func<TaskType, bool>> where);
        void CreateTaskType(TaskType obj);
        void UpdateTaskType(TaskType obj);
        void RemoveTaskType(TaskType obj);
        void SaveChanges();
    }
}
