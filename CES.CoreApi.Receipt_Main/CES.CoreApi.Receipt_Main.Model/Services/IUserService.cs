using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IUserService
    {
        List<systblApp_TaxReceipt_User> GetAllUsers();
        void CreateUser(systblApp_TaxReceipt_User objectEntry);
        void UpdateUser(systblApp_TaxReceipt_User objectEntry);
        void RemoveUser(systblApp_TaxReceipt_User objectEntry);
    }
}
