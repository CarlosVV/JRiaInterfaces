using CES.CoreApi.Receipt_Main.Domain;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.Repository;
using CES.CoreApi.Receipt_Main.UI.WPF.Security;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            //Bind<IUserService>().To<UserService>().InSingletonScope(); // Reuse same storage every time
            // Bind<UserControlViewModel>().ToSelf().InTransientScope(); // Create new instance every time
            Bind<IUserService>().To<UserService>().InTransientScope(); 
            Bind<IUserService>().To<UserService>().InTransientScope();
            Bind<IUserRepository>().To<UserRepository>().InTransientScope();
            Bind<IAuthenticationService>().To<AuthenticationService>();
        }
    }
}
