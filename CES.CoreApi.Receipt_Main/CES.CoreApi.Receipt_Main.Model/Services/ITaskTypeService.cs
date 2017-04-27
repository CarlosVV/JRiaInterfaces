using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaskTypeService
    {
        List<TaskType> GetAllTaskTypes();
        void CreateTaskType(TaskType objectEntry);
        void UpdateTaskType(TaskType objectEntry);
        void RemoveTaskType(TaskType objectEntry);
        
    }
}
