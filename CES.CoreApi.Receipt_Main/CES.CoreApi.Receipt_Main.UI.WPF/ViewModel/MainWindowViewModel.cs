using CES.CoreApi.Receipt_Main.UI.WPF.Model;
using CES.CoreApi.Receipt_Main.UI.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            MenuElements = new[]
            {
                new MenuElement {Label = "Actions", Content =  new CafManagement() },
                new MenuElement{Label = "Search Documents" },
                new MenuElement{Label = "Send to EIS" }
            };
        }
        public MenuElement[] MenuElements { get; }
    }   
}
