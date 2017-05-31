using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Model
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            Time = DateTime.Now;
            Modified = DateTime.Now;
        }
        public bool Disabled { get; set; }
        public bool Delete { get; set; }
        public bool Changed { get; set; }
        public DateTime Time { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedID { get; set; }

    }
}
