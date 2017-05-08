﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model
{
    public partial class FunctionRole
    {
        public Guid Id { get; set; }

        public Guid? RoleId { get; set; }

        public Guid? FunctionalityId { get; set; }

        public Guid? MenuId { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual Functionality Functionality { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual Role Role { get; set; }
        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}