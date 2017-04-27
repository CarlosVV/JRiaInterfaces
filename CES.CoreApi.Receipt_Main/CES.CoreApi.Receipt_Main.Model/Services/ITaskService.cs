using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaskService
    {
        List<Task> GetAllTasks();
        void CreateTask(Task objectEntry);
        void UpdateTask(Task objectEntry);
        void RemoveTask(Task objectEntry);
        
    }
}
