using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class TaskDetailRepository : BaseRepository<TaskDetail>, ITaskDetailRepository
    {
        public TaskDetailRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public TaskDetail find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<TaskDetail> find(Expression<Func<TaskDetail, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaskDetail(TaskDetail obj)
        {
            this.Add(obj);
        }
        public void UpdateTaskDetail(TaskDetail obj)
        {
            this.Update(obj);
        }
        public void RemoveTaskDetail(TaskDetail obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
