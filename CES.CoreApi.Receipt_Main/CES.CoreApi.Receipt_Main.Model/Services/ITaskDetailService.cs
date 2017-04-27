using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaskDetailService
    {
        List<TaskDetail> GetAllTaskDetails();
        void CreateTaskDetail(TaskDetail objectEntry);
        void UpdateTaskDetail(TaskDetail objectEntry);
        void RemoveTaskDetail(TaskDetail objectEntry);
        
    }
}
