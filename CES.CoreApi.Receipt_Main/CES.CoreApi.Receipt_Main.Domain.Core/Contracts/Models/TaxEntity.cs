using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxEntity
    {
        public TaxEntity()
        {
            TaxAddresses = new HashSet<TaxAddress>();
        }
             
        public int fTaxEntityId { get; set; }
   
        public string fRUT { get; set; }

        public string fFirstName { get; set; }
        
        public string fMiddleName { get; set; }
       
        public string fLastName1 { get; set; }
     
        public string fLastName2 { get; set; }
    
        public string fFullName { get; set; }
       
        public string fGender { get; set; }
     
        public string fOccupation { get; set; }

        public DateTime? fDateOfBirth { get; set; }
     
        public string fNationality { get; set; }
    
        public string fCountryOfBirth { get; set; }
   
        public string fPhone { get; set; }
 
        public string fCellPhone { get; set; }
      
        public string fEmail { get; set; }
      
        public string fLineOfBusiness { get; set; }

        public int? fEconomicActivity { get; set; }   
      
        public ICollection<TaxAddress> TaxAddresses { get; set; }
    }
}
