namespace CES.CoreApi.ReferenceData.Service.Business.Contract.Models
{
    public class IdentificationTypeModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public int Id { get; set; }
        public int LocationDepartmentId { get; set; }
        public bool IsExpirationNotRequired { get; set; }
    }
}