using System.Runtime.Serialization;
namespace CES.CoreApi.Receipt_Main.Models.DTOs
{
    /// <summary>
    /// Sample business DTO model  you need to remove it or replace it 
    /// </summary>
    [DataContract]
    public class CurrencyCountry
    {
        [DataMember(Name = "Symbol")]
        public string fSymbol { get; set; }
        [DataMember(Name = "Name")]
        public string fName { get; set; }
    }
}