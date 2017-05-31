using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
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
