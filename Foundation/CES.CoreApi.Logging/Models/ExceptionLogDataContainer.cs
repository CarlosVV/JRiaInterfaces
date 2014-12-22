using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
    [DataContract]
    public class ExceptionLogDataContainer : IDataContainer
    {
        #region Core

        private readonly IIocContainer _container;
        private readonly IJsonDataContainerFormatter _formatter;

        /// <summary>
        /// Initializes ExceptionLogDataContainer instance
        /// </summary>
        public ExceptionLogDataContainer(IIocContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
           
            Items = new Collection<ExceptionLogItemGroup>();
            Timestamp = _container.Resolve<ICurrentDateTimeProvider>().GetCurrentLocal();
            _formatter = _container.Resolve<IJsonDataContainerFormatter>();
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        #endregion //Core

        #region Public properties

        /// <summary>
        /// Gets or sets log record creation time
        /// </summary>
        [DataMember]
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Gets list of log item groups
        /// </summary>
        [DataMember]
        public Collection<ExceptionLogItemGroup> Items { get; private set; }

        /// <summary>
        /// Gets an exception instance
        /// </summary>
        [DataMember]
        public CoreApiException Exception { get; private set; }

        /// <summary>
        /// Gets or sets thred identifier
        /// </summary>
        [DataMember]
        public int ThreadId { get; private set; }

        /// <summary>
        /// Gets an exception message
        /// </summary>
        [DataMember]
        [DefaultValue("")]
        public string Message
        {
            get { return Exception == null ? string.Empty : GetExceptionMessage(); }
        }

        [DataMember]
        [DefaultValue("")]
        public string CustomMessage { get; set; }

        #endregion //Public properties

        #region Public methods
        
        /// <summary>
        /// Gets existing item group or creates new one
        /// </summary>
        /// <param name="groupTitle">Group title</param>
        public ExceptionLogItemGroup GetGroupByTitle(string groupTitle)
        {
            if (string.IsNullOrEmpty(groupTitle))
                throw new ArgumentNullException("groupTitle");

            var group = Items.FirstOrDefault(p => p.Title.Equals(groupTitle, StringComparison.OrdinalIgnoreCase));
            if (group == null)
            {
                group = _container.Resolve<ExceptionLogItemGroup>();
                group.Title = groupTitle;
                Items.Add(group);
            }
            return group;
        }

        public void SetException(Exception exception)
        {
            if (exception == null) 
                throw new ArgumentNullException("exception");

            var coreApiException = exception as CoreApiException ?? new CoreApiException(exception);
            Exception = coreApiException;
        }

        #endregion //Public methods

        #region Overriding

        /// <summary>
        /// Returns string representation of the log entry
        /// </summary>
        /// <returns>String representation of the log entry</returns>
        public override string ToString()
        {
            return _formatter.Format(this);
        }

        /// <summary>
        /// Gets log type
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogType LogType
        {
            get { return LogType.ExceptionLog;}
        }

        #endregion //Overriding

        #region Private methods
        
        /// <summary>
        /// Gets the most inner exception message
        /// </summary>
        /// <returns></returns>
        private string GetExceptionMessage()
        {
            var message = Exception.Message;
            var innerException = Exception.InnerException;

            while (innerException != null)
            {
                message = innerException.Message;
                innerException = innerException.InnerException;
            }

            return message;
        }

        #endregion //Private methods
    }
}