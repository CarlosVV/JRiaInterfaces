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
        public List<systblApp_CoreAPI_Caf> GetAllCafs()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateCaf(systblApp_CoreAPI_Caf objectEntry)
        {
            this.repo.CreateCaf(objectEntry);     
        }
        public void UpdateCaf(systblApp_CoreAPI_Caf objectEntry)
        {
            this.repo.UpdateCaf(objectEntry);
        }
        public void RemoveCaf(systblApp_CoreAPI_Caf objectEntry)
        {
            this.repo.RemoveCaf(objectEntry);
        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
