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
    public class EntityIdentificationService : IEntityIdentificationService
    {
        private IEntityIdentificationRepository repo;
        public EntityIdentificationService(IEntityIdentificationRepository repository)
        {
            repo = repository;
        }
        public List<EntityIdentification> GetAllEntityIdentifications()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateEntityIdentification(EntityIdentification objectEntry)
        {
            this.repo.CreateEntityIdentification(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateEntityIdentification(EntityIdentification objectEntry)
        {
            this.repo.UpdateEntityIdentification(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveEntityIdentification(EntityIdentification objectEntry)
        {
            this.repo.RemoveEntityIdentification(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
