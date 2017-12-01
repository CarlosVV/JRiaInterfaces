using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
            : base("name=main")
        {
        }
    }
}
