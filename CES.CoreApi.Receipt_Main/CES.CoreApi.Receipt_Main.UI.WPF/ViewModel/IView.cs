using CES.CoreApi.Receipt_Main.UI.WPF.View;

namespace CES.CoreApi.Receipt_Main.UI.WPF.ViewModel
{
    public interface IView
    {
        IViewModel ViewModel
        {
            get;
            set;
        }

        void Show();
    }
}