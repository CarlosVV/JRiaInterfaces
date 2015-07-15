using System;
using CES.CoreApi.Common.Models.Shared;

namespace CES.CoreApi.Customer.Service.Business.Contract.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string ExternalCustomerId { get; set; }
        public AddressModel Address { get; set; }
        public NameModel Name { get; set; }
        public ContactModel Contact { get; set; }
        public DateTime LastUsed { get; set; }
        public DateTime DateCreated { get; set; }
        public string ReferredBy { get; set; }
        public int AgentId { get; set; }
        public int AgentLocationId { get; set; }
        public bool IsOnHold { get; set; }
        public bool IsDisabled { get; set; }
        public string Note { get; set; }
        public int StatusId { get; set; }
        
        ///// <summary>
        ///// Gets or sets the name of the matched.
        ///// </summary>
        ///// <value>The name of the matched.</value>
        //public string MatchedName { get; set; }

        ///// <summary>
        ///// Gets or sets the matched telephone.
        ///// </summary>
        ///// <value>The matched telephone.</value>
        //public string MatchedTelephone { get; set; }

        ///// <summary>
        ///// Gets or sets the matched address.
        ///// </summary>
        ///// <value>The matched address.</value>
        //public string MatchedAddress { get; set; }

        ///// <summary>
        ///// Gets or sets the name id agent loc.
        ///// </summary>
        ///// <value>The name id agent loc.</value>
        //public int NameIdAgentLoc { get; set; }

        ///// <summary>
        ///// Gets or sets the Rialink Card Number if used.
        ///// </summary>
        ///// <value>The customer Rialink Card Number if used.</value>
        //public String RiaLinkCardUsed { get; set; }
    }
}