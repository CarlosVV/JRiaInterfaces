using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class TaskDetailService : ITaskDetailService
    {
        private ITaskDetailRepository repo;
        public TaskDetailService(ITaskDetailRepository repository)
        {
            repo = repository;
        }
        public List<TaskDetail> GetAllTaskDetails()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTaskDetail(TaskDetail objectEntry)
        {
            this.repo.CreateTaskDetail(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateTaskDetail(TaskDetail objectEntry)
        {
            this.repo.UpdateTaskDetail(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveTaskDetail(TaskDetail objectEntry)
        {
            this.repo.RemoveTaskDetail(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
