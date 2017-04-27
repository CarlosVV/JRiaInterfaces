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
    public partial class App : Application
    {
        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            var customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
            ConfigureDependencies();
            ComposeObjects();

            base.OnStartup(e);

            IView loginWindow = container.Get<LoginWindow>();
            loginWindow.Show();
        }

        private void ConfigureDependencies()
        {
            container = new StandardKernel(new MyModule());
        }
        private void ComposeObjects()
        {
            Current.MainWindow = this.container.Get<MainWindow>();
            Current.MainWindow.Title = "Ria Financial";
        }
    }
}
