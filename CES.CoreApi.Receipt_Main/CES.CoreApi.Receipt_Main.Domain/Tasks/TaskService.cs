using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class TaskService : ITaskService
    {
        private ITaskRepository repo;
        public TaskService(ITaskRepository repository)
        {
            repo = repository;
        }
        public List<Task> GetAllTasks()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTask(Task objectEntry)
        {
            this.repo.CreateTask(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateTask(Task objectEntry)
        {
            this.repo.UpdateTask(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveTask(Task objectEntry)
        {
            this.repo.RemoveTask(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
