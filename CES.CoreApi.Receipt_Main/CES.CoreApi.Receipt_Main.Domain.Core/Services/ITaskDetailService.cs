using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface ITaskDetailService
    {
        List<systblApp_CoreAPI_TaskDetail> GetAllTaskDetails();
        void CreateTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry);
        void UpdateTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry);
        void RemoveTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry);
        void SaveChanges();
    }
}
