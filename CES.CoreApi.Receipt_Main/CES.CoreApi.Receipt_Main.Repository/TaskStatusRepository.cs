using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class TaskStatusRepository : BaseRepository<TaskStatus>, ITaskStatusRepository
    {
        public TaskStatusRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public TaskStatus find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<TaskStatus> find(Expression<Func<TaskStatus, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaskStatus(TaskStatus obj)
        {
            this.Add(obj);
        }
        public void UpdateTaskStatus(TaskStatus obj)
        {
            this.Update(obj);
        }
        public void RemoveTaskStatus(TaskStatus obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
