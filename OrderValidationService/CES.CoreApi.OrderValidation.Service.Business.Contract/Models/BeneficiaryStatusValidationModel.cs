namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class BeneficiaryStatusValidationModel
    {
        public int BeneficiaryId { get; set; }
        public int CorrespondentId { get; set; }
        public bool IsBlocked { get; set; }
    }
}