﻿using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();
        void CreateRole(Role objectEntry);
        void UpdateRole(Role objectEntry);
        void RemoveRole(Role objectEntry);
        
    }
}
