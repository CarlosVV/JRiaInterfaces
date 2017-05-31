
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository
{
    public class MenuRepository : BaseRepository<systblApp_TaxReceipt_Menu>, IMenuRepository
    {
        public MenuRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_TaxReceipt_Menu find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_TaxReceipt_Menu> find(Expression<Func<systblApp_TaxReceipt_Menu, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateMenu(systblApp_TaxReceipt_Menu obj)
        {
            this.Add(obj);
        }
        public void UpdateMenu(systblApp_TaxReceipt_Menu obj)
        {
            this.Update(obj);
        }
        public void RemoveMenu(systblApp_TaxReceipt_Menu obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
