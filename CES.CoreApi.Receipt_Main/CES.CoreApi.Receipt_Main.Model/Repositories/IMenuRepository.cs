using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IMenuRepository
    {
        Menu find(string id);
        IEnumerable<Menu> find(Expression<Func<Menu, bool>> where);
        void CreateMenu(Menu obj);
        void UpdateMenu(Menu obj);
        void RemoveMenu(Menu obj);
        void SaveChanges();
    }
}
