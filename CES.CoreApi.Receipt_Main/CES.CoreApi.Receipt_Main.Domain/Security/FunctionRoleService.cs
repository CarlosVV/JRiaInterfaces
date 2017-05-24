using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Security;
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
        public List<systblApp_TaxReceipt_FunctionRole> GetAllFunctionRoles()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateFunctionRole(systblApp_TaxReceipt_FunctionRole objectEntry)
        {
            this.repo.CreateFunctionRole(objectEntry);
          
        }
        public void UpdateFunctionRole(systblApp_TaxReceipt_FunctionRole objectEntry)
        {
            this.repo.UpdateFunctionRole(objectEntry);
            
        }
        public void RemoveFunctionRole(systblApp_TaxReceipt_FunctionRole objectEntry)
        {
            this.repo.RemoveFunctionRole(objectEntry);
            
        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
