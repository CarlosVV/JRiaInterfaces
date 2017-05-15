﻿using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ITaskService
    {
        List<systblApp_CoreAPI_Task> GetAllTasks();
        void CreateTask(systblApp_CoreAPI_Task objectEntry);
        void UpdateTask(systblApp_CoreAPI_Task objectEntry);
        void RemoveTask(systblApp_CoreAPI_Task objectEntry);
        
    }
}
