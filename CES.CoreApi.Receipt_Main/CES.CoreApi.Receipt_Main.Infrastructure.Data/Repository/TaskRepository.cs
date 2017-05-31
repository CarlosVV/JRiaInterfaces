using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;


namespace CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository
{
    public class TaskRepository : BaseRepository<systblApp_CoreAPI_Task>, ITaskRepository
    {
        public TaskRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_CoreAPI_Task find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_CoreAPI_Task> find(Expression<Func<systblApp_CoreAPI_Task, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTask(systblApp_CoreAPI_Task obj)
        {
            this.Add(obj);
        }
        public void UpdateTask(systblApp_CoreAPI_Task obj)
        {
            this.Update(obj);
        }
        public void RemoveTask(systblApp_CoreAPI_Task obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
