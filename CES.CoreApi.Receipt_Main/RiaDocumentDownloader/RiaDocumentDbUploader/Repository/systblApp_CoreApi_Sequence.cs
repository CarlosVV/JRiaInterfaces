using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLocalDb.Repository
{
    public class systblApp_CoreApi_Sequence
    {
        [Key]
        public string EntityName { get; set; }
        public int? StartId { get; set; }
        public int? CurrentId { get; set; }
    }
}
