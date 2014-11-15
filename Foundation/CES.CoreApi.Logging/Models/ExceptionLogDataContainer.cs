using System;
using System.Collections.ObjectModel;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Models
{
    public class ExceptionLogDataContainer : IDataContainer
    {
        #region Core

        private readonly IIocContainer _container;
        private readonly IExceptionLogFormatter _exceptionLogFormatter;

        /// <summary>
        /// Initializes ExceptionLogDataContainer instance
        /// </summary>
        public ExceptionLogDataContainer(IIocContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
           
            _exceptionLogFormatter =  _container.Resolve<IExceptionLogFormatter>();
            Items = new Collection<ExceptionLogItemGroup>();
            Timestamp = _container.Resolve<ICurrentDateTimeProvider>().GetCurrentLocal();
        }

        #endregion //Core

        #region Public properties

        /// <summary>
        /// Gets list of log item groups
        /// </summary>
        public Collection<ExceptionLogItemGroup> Items { get; private set; }

        /// <summary>
        /// Gets or sets an exception instance
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets exception timestamp
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Gets an exception message
        /// </summary>
        public string Message
        {
            get { return Exception == null ? string.Empty : GetExceptionMessage(); }
        }

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

        #endregion //Public methods

        #region Overriding

        /// <summary>
        /// Returns string representation of the log entry
        /// </summary>
        /// <returns>String representation of the log entry</returns>
        public override string ToString()
        {
            return _exceptionLogFormatter.Format(this);
        }

        /// <summary>
        /// Gets log type
        /// </summary>
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