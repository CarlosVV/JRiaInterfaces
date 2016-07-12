using System;



namespace CES.CoreApi.Common.Models
{

    public abstract class BaseResponse 
    {
		public BaseResponse()
		{
			ResponseTime = DateTime.UtcNow;
		}
        /// <summary>
        /// Provides information about the success of the operation.
        /// </summary>
      
        public int StatusCode { get; set; }

        /// <summary>
        /// Information about an error that occurred during the geocode dataflow job. This value is provided only for data that was not processed successfully.
        /// </summary>
     
        public string FaultReason { get; set; }

      
        public int ErrorCode { get; set; }

        /// <summary>
        /// Provides data time when response was created in UTC
        /// </summary>
   
        public DateTime ResponseTime { get; private set; }
	
		public string Message  { get; set; }


	}
}
