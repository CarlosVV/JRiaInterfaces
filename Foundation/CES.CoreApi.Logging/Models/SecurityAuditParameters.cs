namespace CES.CoreApi.Logging.Models
{
    public class SecurityAuditParameters
    {
        public int ServiceApplicationId { get; set; }
        public int ClientApplicationId { get; set; }
        public int ServerId { get; set; }
        public string Operation { get; set; }
    }
}
