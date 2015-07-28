using CES.CoreApi.Shared.Business.Contract.Enumerations;

namespace CES.CoreApi.Shared.Business.Contract.Models
{
    public class AgentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int DepartmentId { get; set; }
        public AgentTypeEnum AgentType { get; set; }
        public AgentLocationModel Location { get; set; }
        public string Number { get; set; }
        public bool IsRiaStore { get; set; }
    }
}
