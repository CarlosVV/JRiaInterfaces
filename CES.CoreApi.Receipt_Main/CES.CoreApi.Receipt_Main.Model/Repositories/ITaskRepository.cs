using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ITaskRepository
    {
        systblApp_CoreAPI_Task find(string id);
        IEnumerable<systblApp_CoreAPI_Task> find(Expression<Func<systblApp_CoreAPI_Task, bool>> where);
        void CreateTask(systblApp_CoreAPI_Task obj);
        void UpdateTask(systblApp_CoreAPI_Task obj);
        void RemoveTask(systblApp_CoreAPI_Task obj);
        void SaveChanges();
    }
}
