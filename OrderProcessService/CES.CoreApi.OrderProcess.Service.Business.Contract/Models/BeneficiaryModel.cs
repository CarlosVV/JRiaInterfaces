using System;
using CES.CoreApi.Common.Models.Shared;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class BeneficiaryModel
    {
        public int Id { get; set; }
        public NameModel Name { get; set; }
        public AddressModel Address { get; set; }
        public ContactModel Contact { get; set; }
        public DateTime DateOfBirth { get; set; }



        public int CustomerIdTo { get; set; }
     



        public string MessageForBeneficiary { get; set; }


        public int DelivMethod { get; set; }

        //public string BankAccountNumber { get; set; }

        //public string BankName { get; set; }

        //public string BankAccountType { get; set; }

        public string BeneficiaryIdNumber { get; set; }

        //public string BankCity { get; set; }

        public string BeneficiaryColonia { get; set; }

        public string BeneficiaryColoniaIdType { get; set; }

        public string BeneficiaryColoniaMunicipio { get; set; }

        //public string RoutingCode { get; set; }

        //public int RoutingType { get; set; }

        public bool OpenPaymentNoLocationSelected { get; set; }

        public string Relationship { get; set; }

     
    }
}


//                        BenAddress = dbReader.GetString("fBenAddress"),
//                        BenCity = dbReader.GetString("fBenCity"),
//                        BenState = dbReader.GetString("fBenState"),
//                        BenZip = dbReader.GetString("fBenZip"),
//                        BenCountry = dbReader.GetString("fBenCountry"),
//                        MessageforBen = dbReader.GetString("fMessageforBen"),
//                        BeneficiaryNameFirst = dbReader.GetString("BeneficiaryNameFirst"),
//                        BeneficiaryNameLast1 = dbReader.GetString("BeneficiaryNameLast1"),
//                        BeneficiaryNameLast2 = dbReader.GetString("BeneficiaryNameLast2"),
//                        DelivMethod = dbReader.GetInt("fDelivMethod"),
//                        BankAccountNo = dbReader.GetString("fBankAccountNo"),
//                        BenColonia = dbReader.GetString("fBenColonia"),
//                        BenMunicipio = dbReader.GetString("fBenMunicipio"),
//                        BankName = dbReader.GetString("fBankName"),
//                        BankAcType = dbReader.GetString("fBankAcType"),
//                        BenIDNo = dbReader.GetString("fBenIDNo"),
//                        BankCity = dbReader.GetString("fBankCity"),
//                        BenIDType = dbReader.GetString("fBenIDType"),
//                        OpenPaymentNoLocationSelected = dbReader.GetBool("fOpenPaymentNoLocationSelected"),
//                        Relationship = dbReader.GetString("fCustBenRelationship"),
//                        BenDateOfBirth = dbReader.GetDateTime("fBenDOB")