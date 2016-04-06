namespace CES.CoreApi.Foundation.Contract.Models
{
    public class BehaviorConfiguration
    {
        public bool IsHttpsEnabled { get; set; }
        public bool IsDebugEnabled { get; set; }
        public bool IsHelpEnabled { get; set; }
        public bool IsWsdlEnabled { get; set; }
        public bool IsJsonRequestEnabled { get; set; }
        public bool IsJsonResponseEnabled { get; set; }
        public bool IsAutomaticFormatSelectionEnabled { get; set; }
    }
}
