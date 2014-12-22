using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace CES.CoreApi.Logging.Models
{
    [DataContract]
    public class ExceptionLogItem
    {
        #region Core 
        
        /// <summary>
        /// Initializes ExceptionLogItem instance
        /// </summary>
        public ExceptionLogItem()
        {
            ChildItems = new Collection<ExceptionLogItem>();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets item name
        /// </summary>
        [DataMember]
        public string ItemName { get; set; }

        /// <summary>
        /// Gets item value
        /// </summary>
        [DataMember]
        public string ItemValue { get; set; }

        /// <summary>
        /// Gets child item collection
        /// </summary>
        [DataMember]
        public Collection<ExceptionLogItem> ChildItems { get; private set; }

        #endregion //Public properties
    }
}