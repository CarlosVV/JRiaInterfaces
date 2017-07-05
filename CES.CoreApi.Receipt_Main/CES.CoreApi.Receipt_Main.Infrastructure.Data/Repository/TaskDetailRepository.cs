using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository
{
    public class TaskDetailRepository : BaseRepository<systblApp_CoreAPI_TaskDetail>, ITaskDetailRepository
    {
        public TaskDetailRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_CoreAPI_TaskDetail find(string id)
        {
            return this.Get(p => p.fTaskDetailId.ToString() == id);
        }

        public IEnumerable<systblApp_CoreAPI_TaskDetail> find(Expression<Func<systblApp_CoreAPI_TaskDetail, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaskDetail(systblApp_CoreAPI_TaskDetail obj)
        {
            this.Add(obj);
        }
        public void UpdateTaskDetail(systblApp_CoreAPI_TaskDetail obj)
        {
            this.Update(obj);
        }
        public void RemoveTaskDetail(systblApp_CoreAPI_TaskDetail obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
