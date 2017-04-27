using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void CreateUser(User objectEntry);
        void UpdateUser(User objectEntry);
        void RemoveUser(User objectEntry);
    }
}
