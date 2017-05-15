using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.Model.Tasks;
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
        public List<systblApp_CoreAPI_Task> GetAllTasks()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTask(systblApp_CoreAPI_Task objectEntry)
        {
            this.repo.CreateTask(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateTask(systblApp_CoreAPI_Task objectEntry)
        {
            this.repo.UpdateTask(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveTask(systblApp_CoreAPI_Task objectEntry)
        {
            this.repo.RemoveTask(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
