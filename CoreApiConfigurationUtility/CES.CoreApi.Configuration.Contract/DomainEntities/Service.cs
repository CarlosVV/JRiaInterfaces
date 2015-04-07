using System.Runtime.Serialization;

namespace CES.CoreApi.Configuration.Model.DomainEntities
{
    [DataContract]
    public class Service
    {
        public Service(int id, string name)
        {
            Name = name;
            Id = id;
        }

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public int Id { get; private set; }
    }
}