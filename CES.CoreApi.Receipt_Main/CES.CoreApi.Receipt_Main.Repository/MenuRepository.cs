using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Menu find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Menu> find(Expression<Func<Menu, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateMenu(Menu obj)
        {
            this.Add(obj);
        }
        public void UpdateMenu(Menu obj)
        {
            this.Update(obj);
        }
        public void RemoveMenu(Menu obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
