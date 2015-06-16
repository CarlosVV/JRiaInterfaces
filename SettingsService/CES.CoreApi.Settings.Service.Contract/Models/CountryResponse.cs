using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class CountryResponse: ExtensibleObject
    {
        /// <summary>
        /// Gets or sets the country ID.
        /// </summary>
        /// <value>The country ID.</value>
        [DataMember(IsRequired = true)]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the country name
        /// </summary>
        /// <value>The country name.</value>
        [DataMember(IsRequired = true)]
        public string Name
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the abbrev.
        /// </summary>
        /// <value>The abbrev.</value>
        [DataMember(IsRequired = true)]
        public string Abbreviation
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [DataMember]
        public string Code
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the ISO code.
        /// </summary>
        /// <value>The ISO code.</value>
        [DataMember]
        public string IsoCode
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>The note.</value>
        [DataMember(EmitDefaultValue = false)]
        public string Note
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        [DataMember(IsRequired = true)]
        public int Order
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating whether [use city list].
        /// </summary>
        /// <value><c>true</c> if [use city list]; otherwise, <c>false</c>.</value>
        [DataMember(IsRequired = true)]
        public bool UseCityList
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the ISO code_ alpha3.
        /// </summary>
        /// <value>The ISO code_ alpha3.</value>
        [DataMember]
        public string IsoCodeAlpha3
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the country currency.
        /// </summary>
        /// <value>The country currency.</value>
        [DataMember]
        public string Currency
        {
            get;
            set;
        }
    }
}
