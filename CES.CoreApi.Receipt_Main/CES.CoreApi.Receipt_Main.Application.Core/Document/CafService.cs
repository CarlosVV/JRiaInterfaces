using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class CafService : ICafService
    {
        private ICafRepository _repo;
        public CafService(ICafRepository repository)
        {
            _repo = repository;
        }
        public List<systblApp_CoreAPI_Caf> GetAllCafs()
        {
            return _repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateCaf(systblApp_CoreAPI_Caf objectEntry)
        {
            this._repo.CreateCaf(objectEntry);
        }
        public void UpdateCaf(systblApp_CoreAPI_Caf objectEntry)
        {
            this._repo.UpdateCaf(objectEntry);
        }
        public void RemoveCaf(systblApp_CoreAPI_Caf objectEntry)
        {
            this._repo.RemoveCaf(objectEntry);
        }
        public void SaveChanges()
        {
            this._repo.SaveChanges();
        }
        private int GetNewId()
        {
            var newid = 0;
            var query = GetAllCafs();
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
