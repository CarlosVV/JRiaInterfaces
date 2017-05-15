using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IUserRepository
    {
        systblApp_TaxReceipt_User find(string id);
        IEnumerable<systblApp_TaxReceipt_User> find(Expression<Func<systblApp_TaxReceipt_User, bool>> where);
        void CreateUser(systblApp_TaxReceipt_User obj);
        void UpdateUser(systblApp_TaxReceipt_User obj);
        void RemoveUser(systblApp_TaxReceipt_User obj);
        void SaveChanges();
    }
}
