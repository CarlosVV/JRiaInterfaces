using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class FunctionRoleService : IFunctionRoleService
    {
        private IFunctionRoleRepository repo;
        public FunctionRoleService(IFunctionRoleRepository repository)
        {
            repo = repository;
        }
        public List<FunctionRole> GetAllFunctionRoles()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateFunctionRole(FunctionRole objectEntry)
        {
            this.repo.CreateFunctionRole(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateFunctionRole(FunctionRole objectEntry)
        {
            this.repo.UpdateFunctionRole(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveFunctionRole(FunctionRole objectEntry)
        {
            this.repo.RemoveFunctionRole(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
