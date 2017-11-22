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
        List<systblApp_CoreAPI_Task_Detail> GetAllTaskDetails();
        void CreateTaskDetail(systblApp_CoreAPI_Task_Detail objectEntry);
        void UpdateTaskDetail(systblApp_CoreAPI_Task_Detail objectEntry);
        void RemoveTaskDetail(systblApp_CoreAPI_Task_Detail objectEntry);
        void SaveChanges();
    }
}
