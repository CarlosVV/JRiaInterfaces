﻿using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CES.CoreApi.Receipt_Main.Application.Core
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
           
        }
        public void UpdateTask(systblApp_CoreAPI_Task objectEntry)
        {
            this.repo.UpdateTask(objectEntry);
          
        }
        public void RemoveTask(systblApp_CoreAPI_Task objectEntry)
        {
            this.repo.RemoveTask(objectEntry);
           
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}