using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IUserRepository
    {
        User find(string id);
        IEnumerable<User> find(Func<User, bool> where);        
    }
}
