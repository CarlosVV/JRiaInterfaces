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
        public List<actblTaxDocument_AuthCode> GetAllCafs()
        {
            return _repo.find(c => !c.fDisabled && !c.fDelete).ToList();
        }

        public void CreateCaf(actblTaxDocument_AuthCode objectEntry)
        {
            objectEntry.fAuthCodeID = GetNewId();
            this._repo.CreateCaf(objectEntry);
        }
        public void UpdateCaf(actblTaxDocument_AuthCode objectEntry)
        {
            this._repo.UpdateCaf(objectEntry);
        }
        public void RemoveCaf(actblTaxDocument_AuthCode objectEntry)
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
                max = query.Max(p => p.fAuthCodeID);
            }
            newid = max + 1;
            return newid;
        }
    }
}
