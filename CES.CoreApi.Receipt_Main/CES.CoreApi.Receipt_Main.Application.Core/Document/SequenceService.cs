using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class SequenceService : ISequenceService
    {
        private ISequenceRepository repo;
        public SequenceService()
        {

        }
        public SequenceService(ISequenceRepository repository)
        {
            repo = repository;
        }
        public int GetSequence()
        {
            return 0;
        }
        /*public List<actblTaxDocument_TableSeq> GetAllSequences()
        {
            var results = repo.find(p => p.fCurrentId == p.fCurrentId).ToList();
            return results;
        }     

        public void CreateSequence(actblTaxDocument_TableSeq objectEntry)
        {
            this.repo.CreateSequence(objectEntry);
        }
        public void UpdateSequence(actblTaxDocument_TableSeq objectEntry)
        {
            this.repo.UpdateSequence(objectEntry);
        }
        public void RemoveSequence(actblTaxDocument_TableSeq objectEntry)
        {
            this.repo.RemoveSequence(objectEntry);
        }*/

        //public void SaveChanges()
        //{
        //    this.repo.SaveChanges();
        //}
    }
}
