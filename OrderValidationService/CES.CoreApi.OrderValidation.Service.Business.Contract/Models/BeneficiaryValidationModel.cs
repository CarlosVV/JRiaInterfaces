namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class BeneficiaryValidationModel
    {
        public int BeneficiaryId { get; set; }
        public int CorrespondentId { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string Country { get; set; }
        public int IdentificationTypeId { get; set; }
        public string IdentificationNumber { get; set; }
    }
}