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
    
        public int fEntityID { get; set; }

        public int fCountryID { get; set; }
       
        public string fTaxID { get; set; }

        public int fEntityTypeID { get; set; }
      
        public string fCompanyLegalName { get; set; }
      
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

        public bool fDisabled { get; set; }

        public bool fDelete { get; set; }

        public bool fChanged { get; set; }

        public DateTime fTime { get; set; }

        public DateTime fModified { get; set; }

        public int fModifiedID { get; set; }
       
        public ICollection<TaxAddress> TaxAddresses { get; set; }
    }
}
