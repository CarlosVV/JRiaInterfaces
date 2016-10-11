using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Model
{
    public class PersistenceModel : BaseModel
    {
        public PersistenceModel()
        {
            PersistenceID = 0;
            PersistenceEvents = new List<PersistenceEventModel>();
        }
        public int PersistenceID { get; set; }
        public long OrderID { get; set; }
        public List<PersistenceEventModel> PersistenceEvents { get; set; }
    }
}
