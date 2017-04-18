using CES.CoreApi.Receipt_Main.Domain;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.Repository;
using CES.CoreApi.Receipt_Main.UI.WPF.Security;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF
{
    public class MyModule : NinjectModule
    {
        public override void Load()
        {         
            Bind<IUserService>().To<UserService>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IAuthenticationService>().To<AuthenticationService>();
            Bind<AuthenticationViewModel>().ToSelf();
            Bind<LoginWindow>().ToSelf();
            Bind<DbContext>().To<MyDbContext>();
        }
    }
}
