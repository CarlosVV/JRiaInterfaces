using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using CES.CoreApi.Receipt_Main.Model.Services;
using CES.CoreApi.Receipt_Main.Domain;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Repository;
using CES.CoreApi.Receipt_Main.UI.WPF.Security;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using CES.CoreApi.Receipt_Main.UI.WPF.View;

namespace CES.CoreApi.Receipt_Main.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;
        
        private AuthenticationViewModel authviewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            //Create a custom principal with an anonymous identity at startup
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            IocKernel.Initialize(new IocConfiguration());
            base.OnStartup(e);
            //ConfigureContainer();
            //ComposeObjects();
            authviewModel = new AuthenticationViewModel();
            //Show the login view            
            IView loginWindow = new LoginWindow(authviewModel);
            loginWindow.Show();
        }

        private void ConfigureContainer()
        {
            this.container = new StandardKernel();
            container.Bind<IUserService>().To<UserService>().InTransientScope();
            container.Bind<IUserRepository>().To<UserRepository>().InTransientScope();
            container.Bind<IAuthenticationService>().To<AuthenticationService>();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = this.container.Get<MainWindow>();
            Current.MainWindow.Title = "Tax Receipt Management Application";
        }
    }
}
