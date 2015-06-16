using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Common.Models
{
    [DataContract]
    public abstract class BaseResponse : ExtensibleObject
    {
        protected BaseResponse(ICurrentDateTimeProvider currentDateTimeProvider)
        {
            ResponseTime = currentDateTimeProvider.GetCurrentUtc();
        }

        /// <summary>
        /// Provides information about the success of the operation.
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = true)]
        public ResponseStatus StatusCode { get; set; }

        /// <summary>
        /// Information about an error that occurred during the geocode dataflow job. This value is provided only for data that was not processed successfully.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string FaultReason { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Provides data time when response was created in UTC
        /// </summary>
        [DataMember]
        public DateTime ResponseTime { get; private set; }
    }
}
