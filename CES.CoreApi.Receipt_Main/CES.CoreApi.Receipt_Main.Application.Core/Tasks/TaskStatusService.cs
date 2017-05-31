using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class TaskStatusService : ITaskStatusService
    {
        private ITaskStatusRepository repo;
        public TaskStatusService(ITaskStatusRepository repository)
        {
            repo = repository;
        }
        public List<TaskStatus> GetAllTaskStatuss()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTaskStatus(TaskStatus objectEntry)
        {
            this.repo.CreateTaskStatus(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateTaskStatus(TaskStatus objectEntry)
        {
            this.repo.UpdateTaskStatus(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveTaskStatus(TaskStatus objectEntry)
        {
            this.repo.RemoveTaskStatus(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
