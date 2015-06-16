using System.Runtime.Serialization;

namespace CES.CoreApi.Common.Models
{
    [DataContract]
    public abstract class ExtensibleObject : IExtensibleDataObject
    {
        /// <summary>
        /// To implement the IExtensibleDataObject interface, you must implement the ExtensionData property.
        /// The property holds data from future versions of the class for backward compatibility.
        /// </summary>
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
