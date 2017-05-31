using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Model
{
    public interface IOService
    {
        string OpenFileDialog(string defaultPath);

        //Other similar untestable IO operations
        Stream OpenFile(string path);
    }
}
