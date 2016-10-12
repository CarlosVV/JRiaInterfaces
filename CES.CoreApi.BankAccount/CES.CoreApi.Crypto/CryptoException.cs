using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CES.CoreApi.Crypto
{
    /// <summary>
    /// This exception is thrown by the application code and allows to differentiate from the ones thrown from the
    /// .net framework or third party applications/components
    /// </summary>
    public class CryptoException : ApplicationException
    {
        //@@209-05-19 lb SCR#  610011 Created

        public CryptoException() : base() { }

        public CryptoException(string message) : base(message) { }

        public CryptoException(string message, Exception innerException) : base(message, innerException) { }

        public CryptoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
