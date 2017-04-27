﻿using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;


namespace CES.CoreApi.Receipt_Main.Repository
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Task find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Task> find(Expression<Func<Task, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTask(Task obj)
        {
            this.Add(obj);
        }
        public void UpdateTask(Task obj)
        {
            this.Update(obj);
        }
        public void RemoveTask(Task obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
