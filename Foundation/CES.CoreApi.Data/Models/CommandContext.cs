using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CES.CoreApi.Foundation.Data.Models
{
    internal class CommandContext
    {
        public Database Database { get; set; }
        public DbCommand Command { get; set; }
    }
}