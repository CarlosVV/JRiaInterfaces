using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ITaskRepository
    {
        Task find(string id);
        IEnumerable<Task> find(Expression<Func<Task, bool>> where);
        void CreateTask(Task obj);
        void UpdateTask(Task obj);
        void RemoveTask(Task obj);
        void SaveChanges();
    }
}
