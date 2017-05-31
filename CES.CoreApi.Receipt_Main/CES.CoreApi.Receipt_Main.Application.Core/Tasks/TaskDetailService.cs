using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class TaskDetailService : ITaskDetailService
    {
        private ITaskDetailRepository repo;
        public TaskDetailService(ITaskDetailRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_CoreAPI_TaskDetail> GetAllTaskDetails()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry)
        {
            this.repo.CreateTaskDetail(objectEntry);       
        }
        public void UpdateTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry)
        {
            this.repo.UpdateTaskDetail(objectEntry);           
        }
        public void RemoveTaskDetail(systblApp_CoreAPI_TaskDetail objectEntry)
        {
            this.repo.RemoveTaskDetail(objectEntry);
           
        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
