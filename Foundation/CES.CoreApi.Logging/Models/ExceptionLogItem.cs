using System.Collections.ObjectModel;

namespace CES.CoreApi.Logging.Models
{
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
        public string ItemName { get; set; }

        /// <summary>
        /// Gets item value
        /// </summary>
        public string ItemValue { get; set; }

        /// <summary>
        /// Gets child item collection
        /// </summary>
        public Collection<ExceptionLogItem> ChildItems { get; private set; }

        #endregion //Public properties
    }
}