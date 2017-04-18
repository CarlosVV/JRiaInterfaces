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
        User find(string id);
        IEnumerable<User> find(Expression<Func<User, bool>> where);        
    }
}
