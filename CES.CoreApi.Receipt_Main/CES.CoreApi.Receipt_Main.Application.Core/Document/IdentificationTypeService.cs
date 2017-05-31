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
    public class IdentificationTypeService : IIdentificationTypeService
    {
        private IIdentificationTypeRepository repo;
        public IdentificationTypeService(IIdentificationTypeRepository repository)
        {
            repo = repository;
        }
        public List<IdentificationType> GetAllIdentificationTypes()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateIdentificationType(IdentificationType objectEntry)
        {
            this.repo.CreateIdentificationType(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateIdentificationType(IdentificationType objectEntry)
        {
            this.repo.UpdateIdentificationType(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveIdentificationType(IdentificationType objectEntry)
        {
            this.repo.RemoveIdentificationType(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
