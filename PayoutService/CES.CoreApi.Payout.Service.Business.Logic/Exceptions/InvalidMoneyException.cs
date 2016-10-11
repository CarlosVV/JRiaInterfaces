using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Exceptions
{
    public class InvalidMoneyException : Exception
    {

        public InvalidMoneyException(string msg)
            : base(msg)
        {
            //NOOP: Just passing the message to normal exception handling,
            //but want to have the exception named with this type.
        }

        public InvalidMoneyException()
            : base()
        {
            //NOOP: Just passing the message to normal exception handling,
            //but want to have the exception named with this type.
        }

    }
}
