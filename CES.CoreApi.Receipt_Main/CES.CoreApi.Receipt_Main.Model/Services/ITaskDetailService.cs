using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaskDetailService
    {
        List<systblApp_CoreAPI_TaskDetail> GetAllTaskDetails();
        void CreateTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry);
        void UpdateTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry);
        void RemoveTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry);
        
    }
}
