﻿using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface ICafService
    {
        List<systblApp_CoreAPI_Caf> GetAllCafs();
        void CreateCaf(systblApp_CoreAPI_Caf objectEntry);
        void UpdateCaf(systblApp_CoreAPI_Caf objectEntry);
        void RemoveCaf(systblApp_CoreAPI_Caf objectEntry);
        void SaveChanges();
    }
}