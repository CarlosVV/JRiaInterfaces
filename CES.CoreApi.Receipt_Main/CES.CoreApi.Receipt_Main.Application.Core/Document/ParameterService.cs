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
    public class ParameterService : IParameterService
    {
        private IParameterRepository _repo;
        public ParameterService(IParameterRepository repository)
        {
            _repo = repository;
        }
        public List<systblApp_TaxReceipt_Parameter> GetAllParameters()
        {
            return _repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateParameter(systblApp_TaxReceipt_Parameter objectEntry)
        {
            objectEntry.Id = GetNewId();
            this._repo.CreateParameter(objectEntry);
        }
        public void UpdateParameter(systblApp_TaxReceipt_Parameter objectEntry)
        {
            this._repo.UpdateParameter(objectEntry);
        }
        public void RemoveParameter(systblApp_TaxReceipt_Parameter objectEntry)
        {
            this._repo.RemoveParameter(objectEntry);
        }
        public void SaveChanges()
        {
            this._repo.SaveChanges();
        }
        private int GetNewId()
        {
            var newid = 0;
            var query = GetAllParameters();
            var max = 0;
            if (query != null && query.Count() > 0)
            {
                max = query.Max(p => p.Id);
            }
            newid = max + 1;
            return newid;
        }
    }
}
