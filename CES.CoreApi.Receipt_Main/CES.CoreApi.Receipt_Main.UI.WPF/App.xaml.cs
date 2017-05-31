using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.UI.WPF.ViewModel;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using CES.CoreApi.Receipt_Main.UI.WPF.Config;
using CES.CoreApi.Receipt_Main.Infrastructure.Core.Security;

namespace CES.CoreApi.Receipt_Main.UI.WPF
{
    public partial class App : System.Windows.Application
    {
        public static IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            var customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
            ConfigureDependencies();
            ComposeObjects();

            AppDomain.CurrentDomain.SetData("DataDirectory", AppSettings.DbPath);

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
            Current.MainWindow = App.container.Get<MainWindow>();
            Current.MainWindow.Title = "Ria Financial";
        }
    }
}
