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
             
        public int Id { get; set; }
   
        public string RUT { get; set; }

        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
       
        public string LastName1 { get; set; }
     
        public string LastName2 { get; set; }
    
        public string FullName { get; set; }
       
        public string Gender { get; set; }
     
        public string Occupation { get; set; }

        public DateTime? DateOfBirth { get; set; }
     
        public string Nationality { get; set; }
    
        public string CountryOfBirth { get; set; }
   
        public string Phone { get; set; }
 
        public string CellPhone { get; set; }
      
        public string Email { get; set; }
      
        public string LineOfBusiness { get; set; }

        public int? EconomicActivity { get; set; }   
      
        public ICollection<TaxAddress> TaxAddresses { get; set; }
    }
}
