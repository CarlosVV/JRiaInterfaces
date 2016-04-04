using System;

namespace CES.CoreApi.Foundation.Contract.Models
{
    public class ReliableSessionConfiguration
    {
        public ReliableSessionConfiguration()
        {
            Ordered = true;
        }

        public bool Enabled { set; get; }
        public TimeSpan InactivityTimeout { set; get; }
        public bool Ordered { set; get; }
    }
}
