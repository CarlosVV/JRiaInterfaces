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
    public class TaskTypeService : ITaskTypeService
    {
        private ITaskTypeRepository repo;
        public TaskTypeService(ITaskTypeRepository repository)
        {
            repo = repository;
        }
        public List<TaskType> GetAllTaskTypes()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateTaskType(TaskType objectEntry)
        {
            this.repo.CreateTaskType(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateTaskType(TaskType objectEntry)
        {
            this.repo.UpdateTaskType(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveTaskType(TaskType objectEntry)
        {
            this.repo.RemoveTaskType(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
