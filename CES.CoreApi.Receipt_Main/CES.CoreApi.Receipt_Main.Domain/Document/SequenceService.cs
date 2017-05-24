using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Receipt_Main.Model.Documents;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class SequenceService : ISequenceService
    {
        private ISequenceRepository repo;
        public SequenceService(ISequenceRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_CoreApi_Sequence> GetAllSequences()
        {
            var results = repo.find(p => p.CurrentId == p.CurrentId).ToList();
            return results;
        }     

        public void CreateSequence(systblApp_CoreApi_Sequence objectEntry)
        {
            this.repo.CreateSequence(objectEntry);
        }
        public void UpdateSequence(systblApp_CoreApi_Sequence objectEntry)
        {
            this.repo.UpdateSequence(objectEntry);
        }
        public void RemoveSequence(systblApp_CoreApi_Sequence objectEntry)
        {
            this.repo.RemoveSequence(objectEntry);
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
