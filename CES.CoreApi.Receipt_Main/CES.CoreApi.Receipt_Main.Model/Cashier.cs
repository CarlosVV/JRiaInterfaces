using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model
{
    public partial class Cashier
    {
        public Guid Id { get; set; }

        [StringLength(80)]
        public string FirstName { get; set; }

        [StringLength(80)]
        public string MidleName { get; set; }

        [StringLength(80)]
        public string LastName1 { get; set; }

        [StringLength(80)]
        public string LastName2 { get; set; }

        public Guid? UserId { get; set; }

        public Guid? StoreId { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual User User { get; set; }
    }
}
