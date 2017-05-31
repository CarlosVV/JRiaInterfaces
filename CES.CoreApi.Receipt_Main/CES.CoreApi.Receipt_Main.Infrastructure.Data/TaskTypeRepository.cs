using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;


namespace CES.CoreApi.Receipt_Main.Repository
{
    public class TaskTypeRepository : BaseRepository<TaskType>, ITaskTypeRepository
    {
        public TaskTypeRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public TaskType find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<TaskType> find(Expression<Func<TaskType, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaskType(TaskType obj)
        {
            this.Add(obj);
        }
        public void UpdateTaskType(TaskType obj)
        {
            this.Update(obj);
        }
        public void RemoveTaskType(TaskType obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
