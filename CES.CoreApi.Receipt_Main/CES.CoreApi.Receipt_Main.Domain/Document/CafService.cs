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
    public class CafService : ICafService
    {
        private ICafRepository repo;
        public CafService(ICafRepository repository)
        {
            repo = repository;
        }
        public List<Caf> GetAllCafs()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateCaf(Caf objectEntry)
        {
            this.repo.CreateCaf(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateCaf(Caf objectEntry)
        {
            this.repo.UpdateCaf(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveCaf(Caf objectEntry)
        {
            this.repo.RemoveCaf(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
