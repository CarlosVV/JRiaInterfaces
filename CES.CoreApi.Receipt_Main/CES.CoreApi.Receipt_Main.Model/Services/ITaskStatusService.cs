using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaskStatusService
    {
        List<TaskStatus> GetAllTaskStatuss();
        void CreateTaskStatus(TaskStatus objectEntry);
        void UpdateTaskStatus(TaskStatus objectEntry);
        void RemoveTaskStatus(TaskStatus objectEntry);
        
    }
}
